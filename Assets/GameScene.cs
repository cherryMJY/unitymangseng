using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamerTool.GameObjectPool;

public class GameScene : MonoBehaviour
{
    public UnitController controlUnit;

    // Start is called before the first frame update
    void Awake()
    {
        GameStaticMethod.ChangeCursor(CursorType.Idle);
        GameObjectPoolManager.InitByScriptableObject();
    }
    void Start()
    {
        InputHandler.Instance.SelectControlUnit(controlUnit.unit);
    }

    // Update is called once per frame
    void Update()
    {
        var command = InputHandler.Instance.HandleInput();
        if(command !=null)
        {
            command.Execute();
        }
        controlUnit.unit.Update();
    }
}
