using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//今天应该做的是完全按照单例模式 
//先按这个写一遍 

public class Unit
{
    public  Transform transform;
    public Unit(Transform transform)
    {
        Debug.Log("998");
        this.transform = transform;
    }
    public Vector3 GetPosNoY()
    {
        return new Vector3(transform.position.x,0, transform.position.z);
    }
    public Vector3 GetPos()
    {
        return transform.position;
    }
    public virtual void PlayAnim(AnimParamType apt, params object[] param)
    { }
    public virtual void MoveTo(Vector3 aimPos)
    { }
    public virtual void SetPos(Vector3 aimPos)
    {}
    public virtual  void Update()
    { }
    public virtual void SetIdle()
    { }
}
