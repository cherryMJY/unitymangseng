using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour
{
    public Animator anim;
    public GameObject com;
    public Transform cam;
    public HingeJoint leftLeg, rightLeg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            com.transform.rotation = Quaternion.LookRotation(cam.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            com.transform.rotation = Quaternion.LookRotation(-cam.right);
        }
        if (Input.GetKey(KeyCode.S))
        {
            com.transform.rotation = Quaternion.LookRotation(-cam.forward);
        }
        if (Input.GetKey(KeyCode.D))
        {
            com.transform.rotation = Quaternion.LookRotation(cam.right);
        }

        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            anim.Play("idle");
            leftLeg.useSpring = true; rightLeg.useSpring = true;
        }
        else
        {
            anim.Play("walk");
            leftLeg.useSpring = false; rightLeg.useSpring = false;
        }
    }
}
