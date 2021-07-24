using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class moveAnimCont : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform Rotate;
    public Animator Anim;
    public Transform cam; 
    public float Transla = 8;

    public HingeJoint[] AllHingeJoints;
    public ConfigurableJoint[] AllConfigurableJoints;
    public BoxCollider[] ColisoresIK;
    public bool UseSpring;
    public float zhengdirection;
    public float TempoLevantar;
    public float ForcaInicial = 500;
    private  float TimeReborn;
    private Quaternion RotAnt;
    private Vector3 RotPos;
    private JointDrive drive;


    void Start()
    {
        zhengdirection = Rotate.eulerAngles.y;
    }




    // Update is called once per frame
    void Update()
    {

        /*
        if(!Input.GetKey(KeyCode.LeftAlt))
        {
            //物体进行旋转  旋转到相机的y 
            var Angulo = Mathf.LerpAngle(transform.eulerAngles.y, cam.eulerAngles.y, 5 * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, Angulo, 0);
        }
        */
        bool useSpringFlag = false;

        if(Input.GetKey(KeyCode.Q))
        {
            useSpringFlag = true;
        }

        setAllSpring(useSpringFlag);

        float Dir = Rotate.eulerAngles.y;
        float Frente = zhengdirection;
        //float Frente = transform.eulerAngles.y; //这个 是正方向 
        var Direita = Frente + 90;       // 斜90
        var Esquerda = Frente - 90;    // 90  

        //加速跑
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Anim.speed = 1.5f;
        }
        else
        {
            Anim.speed = 1f;
        }

 
        
        if (Input.GetKey(KeyCode.W))
        {
            Dir = Mathf.LerpAngle(Dir, Frente, Transla * Time.deltaTime);
            Anim.SetInteger("idleTo", 1);


            if (Input.GetKey(KeyCode.D))
            {
                Dir = Mathf.LerpAngle(Dir, Direita, Transla * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.A))
            {
                Dir = Mathf.LerpAngle(Dir, Esquerda, Transla * Time.deltaTime);

            }
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                Dir = Mathf.LerpAngle(Dir, Frente, Transla * Time.deltaTime);


                Anim.SetInteger("idleTo", -1);
                if (Input.GetKey(KeyCode.D))
                {
                    Dir = Mathf.LerpAngle(Dir, Esquerda, Transla * Time.deltaTime);
    
                }

                if (Input.GetKey(KeyCode.A))
                {
                    Dir = Mathf.LerpAngle(Dir, Direita, Transla * Time.deltaTime);

                }
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    Dir = Mathf.LerpAngle(Dir, Direita, Transla * Time.deltaTime);
                    Anim.SetInteger("idleTo", 1);
            
                }
                else
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        Dir = Mathf.LerpAngle(Dir, Esquerda, Transla * Time.deltaTime);
                        Anim.SetInteger("idleTo", 1);
                    }
                    else
                    {
                        Dir = Mathf.LerpAngle(Dir, Frente, Transla * Time.deltaTime);
                        Anim.SetInteger("idleTo", 0);
                    }
                }

            }

        }
        Rotate.eulerAngles = new Vector3(Rotate.eulerAngles.x,Dir, Rotate.eulerAngles.z);

    }
    //2分39s

    void setAllSpring(bool useSpringFlag)
    {

        bool Hips_flag = GameObject.Find("Hips").GetComponent<isOnGround1>().IsGrounded1;
        bool Spine_01_flag = GameObject.Find("Spine_01").GetComponent<isOnGround1>().IsGrounded1;

        bool UpperLeg_R_flag = GameObject.Find("UpperLeg_R Y").GetComponent<isOnGround1>().IsGrounded1;
        bool LowerLeg_R_flag = GameObject.Find("LowerLeg_R Z").GetComponent<isOnGround1>().IsGrounded1;

        bool UpperLeg_L_flag = GameObject.Find("UpperLeg_L Y").GetComponent<isOnGround2>().IsGrounded2;
        bool LowerLeg_L_flag = GameObject.Find("LowerLeg_L Z").GetComponent<isOnGround2>().IsGrounded2;

        bool Ankle_R_flag = GameObject.Find("Ankle_R").GetComponent<isOnGround1>().IsGrounded1;
        bool Ankle_L_flag = GameObject.Find("Ankle_L").GetComponent<isOnGround2>().IsGrounded2;

        //这里还要加东西，其他部位碰到地板的时候也会算是false


        //var centerMess =  ;
        //bool judgeIfIn =  ;


        if (Ankle_L_flag == false && Ankle_L_flag == false)
        {
            UseSpring = false;
        }
        else
        {
            UseSpring = true;
            if (Hips_flag == true || Spine_01_flag == true )
                UseSpring = false;
        }

        //这里还要加一个 如果一只脚卡主的情况

        if (useSpringFlag == true)
            UseSpring = true;
        
        //这里输入什么的时候就给他来一个就ok 

        if (AllHingeJoints.Length > 0  || AllConfigurableJoints.Length > 0)
        {
            //if (AllHingeJoints[0].useSpring != UseSpring)
            //{
            for(int i=0;i<AllHingeJoints.Length;i++)
            {
                AllHingeJoints[i].useSpring = UseSpring;
            }

            float val = 0f;
            if (UseSpring == true)
                val = 1000f;
            for(int i=0;i<AllConfigurableJoints.Length;i++)
            {
             //   AllConfigurableJoints[i].
                drive.positionSpring = val;
                drive.positionDamper = 0f;
                drive.maximumForce = 3.402823e+38f;
                AllConfigurableJoints[i].angularXDrive = drive;
                AllConfigurableJoints[i].angularYZDrive = drive;
            }
            if (UseSpring == true)
                TimeReborn = 0;
            //}
            Debug.Log("AllHingeJoints[0]useSpring" + AllHingeJoints[0].useSpring);
            Debug.Log("useSpring" + UseSpring);
            Debug.Log("timeReborn" + TimeReborn);

            int first = 0;
            int second = 0;
            if (TimeReborn  <= TempoLevantar)
            {
                TimeReborn += 1 * Time.deltaTime;
                if (UseSpring == true)
                {
                    //问题就在于这个RotAnt变了
                    /*
                    if (first != 0)
                    {
                        transform.rotation = Quaternion.Lerp(transform.rotation, RotAnt, 100 * Time.deltaTime);
                        transform.position = RotPos;

                    }
                    */
                    //transform.rotation = Quaternion.Lerp(transform.rotation, RotAnt, 1.4f * Time.deltaTime);
                    //if(GetComponent<Rigidbody>().constraints != RigidbodyConstraints.FreezeRotation)
                    GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
                    transform.rotation = Quaternion.Lerp(transform.rotation, RotAnt, 1f * Time.deltaTime);
                    Debug.Log("RigidbodyConstraints.FreezeRotation" + TimeReborn);
                    //讲道理这里会弹回去的？
                    //下面这一句自己加的
                    Debug.Log("totation1:" + RotAnt.x + " " + RotAnt.y + " " + RotAnt.z);
                    //Debug.Log("totation2:" + transform.rotation.x + " " + transform.rotation.y + " " + transform.rotation.z);
                    //if (second == 0)
                    //{
                    //    RotAnt = transform.rotation;
                    //    RotPos = transform.position;
                    //}
                    //first = 1;

                }
                else
                {
                    //second = 1;
                    //这个没变
                    RotAnt = transform.rotation;
                    Debug.Log("totation3:" + RotAnt.x + " " + RotAnt.y + " " + RotAnt.z);
                    //这个变了
                    //Debug.Log("totation4:" + transform.rotation.x + " " + transform.rotation.y + " " + transform.rotation.z);
                    //RotAnt = transform.rotation;
                    TimeReborn = 500;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    for(int i=0;i< ColisoresIK.Length;i++)
                    {
                        ColisoresIK[i].enabled = UseSpring;
                    }
                }
            }
            else
            {
                //感觉下面这个并没有用
                if(TimeReborn != 500)
                {
                    //11 
                    //这个角度不对

                    //var AnguloY = Mathf.LerpAngle(transform.eulerAngles.y, CameraRot.eularAngles.y, 2.5f*Time.deltaTime);
                    var AnguloY = Mathf.LerpAngle(transform.eulerAngles.y, zhengdirection, 2.5f*Time.deltaTime);
                    transform.eulerAngles = new Vector3(0, AnguloY, 0);
                    for(int i = 0; i< ColisoresIK.Length;i++)
                    {
                        ColisoresIK[i].enabled = UseSpring;
                    }
                    TimeReborn = 500;
                }
            }
        }
    }
}
