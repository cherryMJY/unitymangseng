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

        setAllSpring();

        float Dir = Rotate.eulerAngles.y;
        float Frente = zhengdirection;
        //float Frente = transform.eulerAngles.y; //这个 是正方向 
        var Direita = Frente + 45;       // 斜90
        var Esquerda = Frente - 45;    // 90  

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

    void setAllSpring()
    {
        if(AllHingeJoints.Length > 0  || AllConfigurableJoints.Length > 0)
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
            if(TimeReborn  <= TempoLevantar)
            {
                TimeReborn += 1 * Time.deltaTime;
                if (UseSpring == true)
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    transform.rotation = Quaternion.Lerp(transform.rotation, RotAnt, 1.4f * Time.deltaTime);
                }
                else
                {
                    RotAnt = transform.rotation;
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
                if(TimeReborn != 500)
                {
                    //11 
                    //这个角度不对

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
