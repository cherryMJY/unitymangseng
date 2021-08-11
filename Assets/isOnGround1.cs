


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isOnGround1 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsGrounded1 = false;
    public bool cubeSix=false;
    public bool cubeTwo = false;
    public bool cubeSixteen = false;

    void Start()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Grounded1" + IsGrounded1);
        GameObject gameObj = other.gameObject;
        print("gameObject" + gameObj.name);
        if(gameObj.name == "Cube (6)")
        {
            cubeSix = true;
        }
        if (gameObj.name == "Cube (16)")
        {
            cubeSixteen = true;
        }
        if (gameObj.name == "Cube (2)")
        {
            cubeTwo = true;
        }
        if (other.transform.tag == "Ground")
        {
            IsGrounded1 = true;
            Debug.Log("Grounded1");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Grounded1" + IsGrounded1);
        
        if (other.transform.tag == "Ground")
        {

            //cubeSix = false;
            //cylinder0 = false;
            IsGrounded1 = false;
            Debug.Log("Grounded1");
        }
    }
    /*
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Grounded1" + IsGrounded1);

        if (other.transform.tag == "Ground")
        {
            IsGrounded1 = true;
            Debug.Log("Grounded1");
        }
        else
        {
            IsGrounded1 = false;
            Debug.Log("Not Grounded1!");
        }
    }
       */
    // Update is called once per frame
    void Update()
    {



    }
}
