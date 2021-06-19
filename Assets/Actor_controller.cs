using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Actor_controller 

public class Actor_controller : MonoBehaviour
{
    public UnitController controlUnit;
    HeroUnit heroUnit;

   
    // Start is called before the first frame update
    void Start()
    {
        heroUnit = new HeroUnit(transform,GetComponentInChildren<Animator>());
       // inputHandler = new InputHandler(heroUnit);
    }

    // Update is called once per frame
    void Update()
    {
        var command1 = InputHandler.Instance.HandleInput();
        if (command1!=null)
        {
            command1.Execute();
        }
        heroUnit.Update();
    }
}
