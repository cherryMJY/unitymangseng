using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamerTool.GameObjectPool;

public class UnitController : MonoBehaviour
{
    //InputHandler inutHandler;
    public Unit unit { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
       // GameObjectPoolManager.InitByScriptableObject();
        unit = new HeroUnit(transform, GetComponentInChildren<Animator>(),GetComponent<Rigidbody>());
        //InputHandler.Instance.SelectUnit(unit);
    }

    private void OnMouseEnter()
    {
        InputHandler.Instance.SelectEnemyUnit(unit);
        GameStaticMethod.ChangeCursor(CursorType.Fight);
    }
    private void OnMouseExit()
    {
        InputHandler.Instance.UnSelectEnemyUnit();
        GameStaticMethod.ChangeCursor(CursorType.Idle);
    }
    // Update is called once per frame
    void Update()
    {

        
    }
}
