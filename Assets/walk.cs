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

        if (Input.GetKey(KeyCode.Q))
        {
            //把力都加上 站起来

            //考虑这样 在按q键的时候 手会着地   然后起来之后
            /*
            if (PressJumpKeyTime == -1)
            {
                PressJumpKeyTime = Time.time;
            }

            if (Time.time - PressJumpKeyTime >= 1.2)
            {
                Debug.LogFormat("按下跳跃时间" + (Time.time - PressJumpKeyTime));

                Elbow_L.useSpring = true;
                Elbow_R.useSpring = true; //手肘 

                Ankle_L.useSpring = true;
                Ankle_R.useSpring = true;//脚

                Hand_L.useSpring = true;
                Hand_R.useSpring = true;   //手

                leftUpLeg.useSpring = true;
                rightUpLeg.useSpring = true; //腿

                drive.positionSpring = 100000f;
                drive.positionDamper = 0f;
                drive.maximumForce = 3.402823e+38f;

                Spine_01.angularXDrive = drive;
                Spine_01.angularYZDrive = drive;
                leftLeg.useSpring = true; rightLeg.useSpring = true;
            }
            else if (Time.time - PressJumpKeyTime >= 0.8)
            {
                //这边要调成他能站立 然后进入idle状态  

                //腿部用力
                //进入站立状态
                Debug.LogFormat("按下跳跃时间" + (Time.time - PressJumpKeyTime));

                Elbow_L.useSpring = true;
                Elbow_R.useSpring = true; //手肘 

                Ankle_L.useSpring = true;
                Ankle_R.useSpring = true;//脚

                Hand_L.useSpring = true;
                Hand_R.useSpring = true;   //手

                leftUpLeg.useSpring = true;
                rightUpLeg.useSpring = true; //腿

                drive.positionSpring = 10000f;
                drive.positionDamper = 0f;
                drive.maximumForce = 3.402823e+38f;
               */
        }
        else if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
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
