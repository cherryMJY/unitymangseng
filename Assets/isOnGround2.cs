
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isOnGround2 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsGrounded2 = false;
    public bool cubeSix = false;
    public bool cubeTwo = false;
    public bool  cubeSixteen = false;

    void Start()
    {

    }
 
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Grounded2" + IsGrounded2);
        GameObject gameObj = other.gameObject;
        print("gameObject" + gameObj.name);
        if (gameObj.name == "Cube (6)")
        {
            cubeSix = true;
        }
        if (gameObj.name == "Cube (16)")
        {
            cubeSixteen= true;
        }
        if (gameObj.name == "Cube (2)")
        {
            cubeTwo = true;
        }
        if (other.transform.tag == "Ground")
        {
            IsGrounded2 = true;
            Debug.Log("Grounded2");
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Grounded2" + IsGrounded2);

        if (other.transform.tag == "Ground")
        {
            // cubeSix = false;
           // cylinder0 = false;
            IsGrounded2 = false;
            Debug.Log("Grounded2");
        }
    }

    // Update is called once per frame
    void Update()
    {



    }
}
