using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 startPosition;
    public Vector3 targetPostion1;
    public Vector3 targetPostion2;
    private int flag = 0;
    public Transform  jueSe;
    public isOnGround1 left;
    public isOnGround2 right;
    public int ok = 0;
    public float tmpTime = 0;

    void Start()
    {
        startPosition = transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {

        //判断有个人踩上来了
        //print("leftright11" + left.IsGrounded1 + right.IsGrounded2);
        print("leftright22" + left.cubeSix + right.cubeSix);


        if (true == left.cubeSix && true == right.cubeSix&&ok!=2)
        {
            jueSe.SetParent(transform);
            if(ok == 0 )
            {
                ok = 1;
                tmpTime = Time.time;
            }
            if(Time.time- tmpTime >=2f)
            {
                if (0 == flag)
                {
                    Vector3 tmp = targetPostion1 - transform.position;
                    transform.position += tmp * Time.deltaTime * 0.1f;
                    float dis = (transform.position - targetPostion1).sqrMagnitude;
                    if ( dis < 1f)
                    {
                        flag = 1;
                    }
                }
                else if(1 == flag)
                {
                    Vector3 tmp = targetPostion2 - transform.position;
                    transform.position += tmp * Time.deltaTime * 0.1f;
                    float dis = (transform.position - targetPostion2).sqrMagnitude;
                    if (dis < 1f)
                    {
                        flag = 2;
                       // jueSe.parent = null;
                        ok = 2;
                        jueSe.SetParent(null);
                    }
                }
            }
            
        }

        if (true == left.cubeTwo && true == right.cubeTwo)
        {
            //判断下去了
            //回去

            if (2 == flag)
            {

                Vector3 tmp = targetPostion1 - transform.position;
                transform.position += tmp * Time.deltaTime * 0.1f;
                float dis = (transform.position - targetPostion1).sqrMagnitude;
                if (dis < 1f)
                {
                    flag = 1;
                }
            }
            else if (1 == flag)
            {

                Vector3 tmp = startPosition - transform.position;
                transform.position += tmp * Time.deltaTime * 0.1f;
                float dis = (transform.position - startPosition).sqrMagnitude;
                if (dis < 0.001f)
                {
                    flag = 0;
                    ok = 0;
                    left.cubeSix = false;
                    right.cubeSix = false;
                    left.cubeTwo = false;
                    right.cubeTwo = false;
                }
            }
        }
        

    }
}
