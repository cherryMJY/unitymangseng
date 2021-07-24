




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isOnGround2 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsGrounded2 = false;

    void Start()
    {

    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        print("collision" + collision.transform.tag);
        if (collision.transform.tag == "Ground")
        {
            IsGrounded2 = true;
            Debug.Log("Grounded1");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            IsGrounded2 = false;
            Debug.Log("Grounded1");
        }

    }
    */
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Grounded2" + IsGrounded2);

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
            IsGrounded2 = false;
            Debug.Log("Grounded2");
        }
    }

    // Update is called once per frame
    void Update()
    {



    }
}
