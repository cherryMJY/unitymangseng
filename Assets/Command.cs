using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using DreamerTool.Singleton;
using DreamerTool.GameObjectPool;

public enum CursorType
{
    Idle,
    Fight
}
public enum AnimParamType
{
    Bool,
    Trigger
}

//状态模式
public enum HeroState
{
    Idle,
    Run,
    Attack
}


//至少这个命令模式已经打通了
//这样子就有了整个大体的框架


//感觉对于一段时间的移动 好像有点问题 

public class HeroUnit:Unit
{
    public HeroState heroState = HeroState.Idle;
    protected Animator anim;
    protected Rigidbody rigi;
    HeroUnit enemyUnit;
    Vector3 aimPos;


    public HeroUnit(Transform transform,Animator anim=null,Rigidbody rigi = null):base(transform)
    {
        Debug.Log("999");
        this.rigi = rigi;
        this.anim = anim;
    }
    public override void PlayAnim(AnimParamType apt,params object[] param)
    {
        if(anim == null)
        {
            Debug.LogError("Anim is Null");
            return ;
        }

        if (apt == AnimParamType.Bool)
        {
            anim.SetBool((String)param[0], (bool)param[1]);
        }
        else if(apt == AnimParamType.Trigger)
        {
            anim.SetTrigger((String)param[0]);
        }
    }
    public override void MoveTo(Vector3 aimPos)
    {
        this.aimPos = aimPos;
        heroState = HeroState.Run;
        GameObjectPoolManager.GetPool("click_move").Get(aimPos, Quaternion.identity, 2);

    }
    public void SetHeroState(HeroState state,params object[] param)
    {
        switch(state)
        {
            case HeroState.Idle:
                break;
            case HeroState.Run:
                break;
            case HeroState.Attack:
                enemyUnit = (param[0] as HeroUnit);
                break;
            default:
                break;
        }
        heroState = state;
    }
    public override void SetIdle()
    {
        //变成Idle状态
        heroState = HeroState.Idle;
    }
    public override void SetPos(Vector3 aimPos)
    {
        //var FlashEffet = Resources.Load<GameObject>("FlashEffet");
        //var FlashEffet2 = Resources.Load<GameObject>("FlashEffet_1");
        //UnityEngine.Object.Instantiate(FlashEffet2, transform.position, Quaternion.identity);
        GameObjectPoolManager.GetPool("flash_effect").Get(transform.position,Quaternion.identity,2);
        transform.forward = (aimPos - transform.position).normalized;
        transform.position = aimPos;
        AudioManager.Instance.PlayOneShot("flash");
        GameObjectPoolManager.GetPool("flash_effect1").Get(transform.position, Quaternion.identity, 2);
        //UnityEngine.Object.Instantiate(FlashEffet, transform.position, Quaternion.identity);
    }
    float timer=0;
    public override void Update()
    {

        if(heroState == HeroState.Idle)
        {
            PlayAnim(AnimParamType.Bool, "run", false);
        }
        else if(heroState == HeroState.Run)
        {
            if (Vector3.Distance(transform.position, aimPos) < 0.1f)
            {
                SetIdle();
                //heroState = HeroState.Idle;
            }
            else
            {
                transform.forward = Vector3.Slerp(transform.forward, (aimPos - transform.position).normalized, Time.deltaTime * 5);
                transform.position = Vector3.MoveTowards(transform.position, aimPos, Time.deltaTime * 5);
                PlayAnim(AnimParamType.Bool, "run", true);
            }
        }
        else if(heroState == HeroState.Attack)
        {
            transform.forward = Vector3.Slerp(transform.forward, (enemyUnit.GetPosNoY() - transform.position).normalized, Time.deltaTime * 5);

            if (Vector3.Distance(transform.position, enemyUnit.GetPos()) < 1.5f)
            {
                PlayAnim(AnimParamType.Bool, "run", false);
                //PlayAnim(AnimParamType.Trigger, "attack");
                timer += Time.deltaTime;
                if(timer >=1)
                {
                    PlayAnim(AnimParamType.Trigger, "attack");
                    timer = 0;
                }
                return;
                //heroState = HeroState.Idle;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, enemyUnit.GetPosNoY(), Time.deltaTime * 5);
                PlayAnim(AnimParamType.Bool, "run", true);
            }
        }
        
    }
}


public class SetPosCommand:Command
{
    protected Vector3 aimPos;
    public SetPosCommand(Unit unit, Vector3 pos)
    {
        this.unit = unit;
        this.aimPos = pos;
    }
    public override void Execute()
    {
        unit.SetPos(aimPos);
    }
}
public class FlashCommand:SetPosCommand
{
    public FlashCommand(Unit unit, Vector3 pos) : base(unit, pos)
    {
    }
    public override void Execute()
    {
        //unit.PlayAnim(AnimParamType.Bool, "run", false);
        //
        
        //unit.PlayAnim(AnimParamType.Bool, "run", true);
        if (Vector3.Distance(unit.GetPos(),aimPos) >=3)
        {
            var dir = (aimPos - unit.GetPos()).normalized;
            aimPos = unit.GetPos() + dir * 3;
            unit.SetPos(aimPos);
        }
        else
        {
            unit.SetPos(aimPos);
        }
        unit.SetIdle();
        //unit.heroState = HeroState.Idle;
    }

}
public abstract class Command
{
    protected Unit unit;
    public abstract void Execute();
}

public class InputHandler:Singleton<InputHandler>
{
    public Unit enemyUnit { get; private set; }
    public Unit controlUnit { get; private set; } 
   public void SelectControlUnit(Unit unit)
    {
        controlUnit = unit;
    }
    public void UnSelectEnemyUnit()
    {
        enemyUnit = null;
    }
    public void SelectEnemyUnit(Unit unit)
    {
        enemyUnit = unit;
    }
    public Command HandleInput()
    {
        //处理输入 命令分发
        //命令模式 这里是 输入了什么 然后 返回一个什么命令
        
        if (Input.GetKeyDown(KeyCode.Mouse1))
        //这里是一个命令  然后我需要
        {
            //return new MoveToCommand(unit, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (enemyUnit != null)
            {
                return new AttackCommand(controlUnit,enemyUnit);
            }
            else
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    return new MoveToCommand(controlUnit, hit.point);
                }
                else
                {
                    return null;
                }
            }
            
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return new FlashCommand(controlUnit, hit.point);
            }
            else
            {
                return null;
            }
        }
        //这里可能会有Q技能 所以还要改动

        return null;
    }
}

public class AttackCommand:Command
{
    Unit attackUnit;
    public AttackCommand(Unit unit,Unit attackUnit )
    {
        this.unit = unit;
        this.attackUnit = attackUnit;
    }
    public override void Execute()
    {
        (unit as HeroUnit).SetHeroState(HeroState.Attack, attackUnit);
    }
}

public class MoveToCommand:Command
{

    Vector3 aimPos;
    public MoveToCommand(Unit unit,Vector3 pos)
    {
        this.unit = unit;
        this.aimPos = pos;
    }
    public override void Execute()
    {
        unit.MoveTo(aimPos);
    }
}