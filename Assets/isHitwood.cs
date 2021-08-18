using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isHitwood : MonoBehaviour
{
    public bool hitWood = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //在脚本中添加这个函数，如果发生碰撞则执行此函数
        // Debug.Log("Grounded1" + IsGrounded1);
        GameObject gameObj = collision.gameObject;
        // print("gameObject" + gameObj.name);
        if (gameObj.name == "Cube (14)"|| gameObj.name == "Cube (15)")
        {
            hitWood = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        //在脚本中添加这个函数，如果发生碰撞则执行此函数
        // Debug.Log("Grounded1" + IsGrounded1);
        GameObject gameObj = collision.gameObject;
        // print("gameObject" + gameObj.name);
        if (gameObj.name == "Cube (14)"|| gameObj.name == "Cube (15)")
        {
            hitWood = false;
        }
    }
    /*
    void OnTriggerEnter(Collider other)
    {

      
        // Debug.Log("Grounded1" + IsGrounded1);
        GameObject gameObj = other.gameObject;
        // print("gameObject" + gameObj.name);
        if (gameObj.name == "Cube (14)")
        {
            hitWood = true;
        }

    }
    
    void OnTriggerExit(Collider other)
    {

        GameObject gameObj = other.gameObject;
        // print("gameObject" + gameObj.name);

        if (gameObj.name == "Cube (14)")
        {
            hitWood = false;
        }
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }
}
