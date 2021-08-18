using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{
    public bool hold;
    public KeyCode grabKey;
    public bool canGrab;
    public Rigidbody[] allRb;
    private int val = 0;

    void Update()
    {
        if (canGrab)
        {
            if (Input.GetKey(grabKey))
            {
                hold = true;
            }
            if(Input.GetKey(KeyCode.Z))
            {
                val = 0;
                hold = false;
                Destroy(GetComponent<FixedJoint>());
            }
        }
    }
    //这边就是举起来 先不管他  
    //先考虑前向还是后向旋转
    private void OnCollisionEnter(Collision col)
    {
        if (hold)
        {
            Rigidbody rb = col.transform.GetComponent<Rigidbody>();
            //这里只能举起来14或者15
            //这里要是碰到另外一个物体的
            if (rb != null&&(col.transform.gameObject.name== "Cube (14)" || col.transform.gameObject.name == "Cube (15)" || col.transform.gameObject.name == "Spine_01"))
            {
                Debug.Log("hand" + 123);
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                //改变物体的position
                fj.connectedBody = rb;
                if (val == 0)
                {
                    val = 1;
                    //col.transform.position = transform.position;
                }

                //感觉在这里的时候要把他的质量都变成很小
                //然后有一个地方 又要变回去
                for (int i=0;i < allRb.Length; i++)
                {
                    float tmpQuality = allRb[i].mass;
                   // allRb[i].mass = tmpQuality / 3;
                }
            }
            else
            {
                Debug.Log("hand" + 456);
               // FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
            }
        }
    }

    /*
    private void OnCollisionExit(Collision col)
    {
        if (hold)
        {
            Rigidbody rb = col.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("hand" + 123);
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                fj.connectedBody = rb;

                //感觉在这里的时候要把他的质量都变成很小
                //然后有一个地方 又要变回去
                for (int i = 0; i < allRb.Length; i++)
                {
                    float tmpQuality = allRb[i].mass;
                    allRb[i].mass = tmpQuality * 3;
                }
            }
            else
            {
                Debug.Log("hand" + 456);
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
            }
        }
    }
    */
}
