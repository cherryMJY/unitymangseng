using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isOnGround : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsGrounded=false;

    void Start()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Grounded1"+ IsGrounded);

        if (other.transform.tag == "Ground")
        {
            IsGrounded = true;
            Debug.Log("Grounded1");
        }
        else
        {
            IsGrounded = false;
            Debug.Log("Not Grounded2!");
        }
    }

    // Update is called once per frame
    void Update()
    {



    }
}
