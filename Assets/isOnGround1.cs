


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isOnGround1 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsGrounded1 = false;

    void Start()
    {

    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            IsGrounded1 = true;
            Debug.Log("Grounded1");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            IsGrounded1 = false;
            Debug.Log("Grounded1");
        }

    }
    */

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Grounded1" + IsGrounded1);

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
