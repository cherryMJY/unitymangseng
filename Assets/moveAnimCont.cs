using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class moveAnimCont : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform cylin;
    public Transform Rotate;
    public Animator Anim;
    public Transform character;
   // public Transform cam; 
    public float Transla = 8;
    public Transform head;
    public Transform hips;
    public HingeJoint[] AllHingeJoints;
    public Transform[] allLeg;
    public ConfigurableJoint[] AllConfigurableJoints;
    public BoxCollider[] ColisoresIK;
    public bool UseSpring;
    public Transform Ankle_L1 ;
    public Transform Ankle_R1;
    public bool ifUseSpring=true;
    public bool zhuJue;
    private float zhengdirection;
    private float zhengdirectionX;
    private float zhengdirectionY;
    private float zhengdirectionZ;

    private float zhengdirectionHipX;
    private float zhengdirectionHipY;
    private float zhengdirectionHipZ;
    private Vector3 targetPos;
    public float TempoLevantar;
    public float ForcaInicial = 500;
    private  float TimeReborn;
    private Quaternion RotAnt;
    private Vector3 RotPos;
    private JointDrive drive;
    private int timeUse = -1;
    private float tmpTime = 0;
    private float woodTime = 0;
    private bool woodFlag = false;
    private bool firstWoodFlag = false;
    private bool justHipSpring = false;
    private bool legSpring = false;
    private Quaternion hipsRotation;
    private Quaternion ikRotation;
    private Vector3 startHipRotation;
    private Vector3 startIkRotation;
    private bool ifIsQKey=false;
    private bool noInput = true;
    private Vector3 distanceFootHip;
    private Rigidbody hibRb;
    private bool qingXieFlag=false;

    public isOnGround1 hipsOther; 
    public isOnGround1 Spine_01Other;
    public isOnGround1 UpperLeg_R_YOther;
    public isOnGround1 LowerLeg_R_ZOther;
    public isOnGround2 UpperLeg_L_YOther;
    public isOnGround2 LowerLeg_L_ZOther;
    public isOnGround1 Ankle_ROther;
    public isOnGround2 Ankle_LOther;


    public isHitwood[] allWood;

    //public bool ifIsGrab;
    public grab Grab;
    public float hp=5f;
    public bool hit =false;


    void Start()
    {
        //这里的重新赋值一下就行
        //上面是ik的选装正方向
        recordData();


        //print("ikRotat" + ikRotation.x);
        //在一边用力往上提的时候hip渐渐的达到这个角度
        distanceFootHip = hips.position - Ankle_L1.position;
        hibRb = GetComponent<Rigidbody>();
        
        Vector3 tmpPosition = transform.position;
        character.position = transform.position;
        transform.position = tmpPosition;
        

        if (ifUseSpring == true)
            hp = 5f;
        else
            hp = 0f;

    }
    void recordData()
    {
        zhengdirection = Rotate.eulerAngles.y;
        zhengdirectionX = Rotate.eulerAngles.x;
        zhengdirectionY = Rotate.eulerAngles.y;
        zhengdirectionZ = Rotate.eulerAngles.z;

        zhengdirectionHipX = hips.eulerAngles.x;
        zhengdirectionHipY = hips.eulerAngles.y;
        zhengdirectionHipZ = hips.eulerAngles.z;

        //这个hip也可以这么玩
        targetPos = hips.position;
        hipsRotation = hips.rotation;

        ikRotation = Rotate.rotation; //记录开始旋转的位置
    }
    Quaternion getQuaternionNi(Quaternion q)
    {
        float norm = q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
        Quaternion tmp=q;
        tmp.Set(-q.x, -q.y, -q.z, q.w);
        tmp.Set(-q.x / norm / norm, -q.y / norm / norm, -q.z / norm / norm, q.w / norm / norm);
        //tmp = tmp /norm /norm;
        return tmp;
    }
    bool judgeSim(Quaternion q, Quaternion p)
    {
        Quaternion now = getQuaternionNi(q);
        Quaternion tmp = now * p;

        tmp.Set(tmp.x, tmp.y, tmp.z, tmp.w - 1);
        float norm = tmp.x * tmp.x + tmp.y * tmp.y + tmp.z * tmp.z + tmp.w * tmp.w;
        if (norm <= 1f)
            return true;
        return false;
    }
    bool judgeDis(Vector3 firstPoint,Vector3 secondPoint)
    {
        //判断两个点的距离
        float dis = (firstPoint.x - secondPoint.x) * (firstPoint.x - secondPoint.x) + (firstPoint.y - secondPoint.y) * (firstPoint.y - secondPoint.y) + (firstPoint.z - secondPoint.z) * (firstPoint.z - secondPoint.z);
        if (Mathf.Sqrt(dis) < 10f)
            return true;
        return false;
    }
    bool judgeOneAnkleHip(Vector3 foot, Vector3 hipPosition)
    {
        Vector3 tmp = hipPosition - foot;
        Vector3 tmp1 = new Vector3(foot.x,foot.y+1,foot.z);
        float angle = Vector3.Angle(tmp,tmp1);
        //print("angle" + angle);
        // 50   130  
        //40   140
        if(angle > 40 && angle < 140)
            return true;
        return false;

    }

    float judgeOneAnkleHip1(Vector3 foot, Vector3 hipPosition)
    {
        Vector3 tmp = hipPosition - foot;
        Vector3 tmp1 = new Vector3(foot.x, foot.y + 1, foot.z);
        float angle = Vector3.Angle(tmp, tmp1);
        //print("angle" + angle);
        return angle;
    }

    bool judgeAnkleHip(Vector3 leftFoot,Vector3 rightFoot,Vector3 hipPosition)
    {
       // Debug.Log("anglea" + judgeOneAnkleHip1(leftFoot, hipPosition) +" "+judgeOneAnkleHip1(rightFoot, hipPosition));
        //Debug.Log("anglea" + judgeOneAnkleHip(leftFoot, hipPosition) + " " + judgeOneAnkleHip(rightFoot, hipPosition));
       if (judgeOneAnkleHip(leftFoot, hipPosition) && judgeOneAnkleHip(rightFoot, hipPosition))
            return true;
        return false;
    }
    // Update is called once per frame
    void Update()
    {

        Vector3 tmpPosition = transform.position;
        character.position = transform.position;
        transform.position = tmpPosition;
       
        noInput = true;
        if(zhuJue==true)
            characterRotation();

        setAllSpring();

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

        //很显然这个给压力的时候其实就是和这个是一样的
        //可以判定，那个方向有速度 或者速度比较大 那么给他播放动画？
        if (zhuJue == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Dir = Mathf.LerpAngle(Dir, Frente, Transla * Time.deltaTime);
                Anim.SetInteger("idleTo", 1);
                noInput = false;
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
                    noInput = false;
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
                        noInput = false;
                        Dir = Mathf.LerpAngle(Dir, Direita, Transla * Time.deltaTime);
                        Anim.SetInteger("idleTo", 1);

                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.A))
                        {
                            noInput = false;
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
        }

        //是否是被拖动状态
        //是否是受力状态
        //拖动不受力 那么脚不用播放动画
        //拖动受力  那么脚要播放动画

        //判断是否被拖动 

        //Anim.SetInteger("idleTo", 1);
        //感觉这里只要是站立状态就可以来一个调整因为身体不是直的。
        if (ifIsQKey == false)
        {

            //问题就是在这里 
            //这个角度需要自己挑一下
            //这个是对的
            //欧拉角度的绕y轴旋转
            Rotate.eulerAngles = new Vector3(Rotate.eulerAngles.x, Dir, Rotate.eulerAngles.z);

            if (noInput == true)
            {
                legSpring = true;
                Vector3 targetPosition = Ankle_L1.position + distanceFootHip;

                float magnitude = hibRb.velocity.magnitude;
                Vector3 velocityVal = hibRb.velocity;

                if (magnitude >= 0.4f && magnitude <= 2f)
                {
                    //其实只要算出正方向和速度的角度就ok ?
                    // Dir = ;
                    //确定旋转的角度
                    //原来是w  a s d 
                    //
                    Anim.speed = 1f;
                    float valY = velocityVal.y;
                    float valZ = velocityVal.z;
                    float valYAbs = Mathf.Abs(valY);
                    float valZAbs = Mathf.Abs(valZ);
                    if (valZAbs > valYAbs)
                    {

                        if (valZ > 0)
                        {
                          //  print("val1");
                            Dir = Mathf.LerpAngle(Dir, Frente, Transla * Time.deltaTime);
                            Anim.SetInteger("idleTo", 1);
                        }
                        else
                        {
                           // print("val2");
                            Dir = Mathf.LerpAngle(Dir, Frente, Transla * Time.deltaTime);
                            Anim.SetInteger("idleTo", -1);
                        }
                    }
                    else
                    {
                        if (valY > 0)
                        {
                            //print("val3");
                            Dir = Mathf.LerpAngle(Dir, Direita, Transla * Time.deltaTime);
                            Anim.SetInteger("idleTo", 1);
                        }
                        else
                        {
                            //print("val4");
                            Dir = Mathf.LerpAngle(Dir, Esquerda, Transla * Time.deltaTime);
                            Anim.SetInteger("idleTo", 1);
                        }
                    }
                    Rotate.eulerAngles = new Vector3(Rotate.eulerAngles.x, Dir, Rotate.eulerAngles.z);
 
                    legSpring = false;

                    //这里来一个判断 就是脚和身体的斜度超过了一定值就会倒地
                    if(!judgeAnkleHip(Ankle_L1.position, Ankle_R1.position, hips.position))
                    {
                        qingXieFlag = true;

                    }

                    if(Grab.hold == true)
                    {
                        if(ifUseSpring == false)
                        {
                            Anim.SetInteger("idleTo", 0);
                        }
                    }
                }
                //  transform.position = Vector3.MoveTowards(transform.position, targetPosition, 100 * Time.deltaTime);
            }
            else
                legSpring = false;

            //就是这个重新到这里就出现问题了
            // 3.7 7.3 12.6 
            //3.8 7  12.8
            //transform.position = new Vector3(3.7f, 7.3f, 12.6f);
            print("hipPos1" + transform.position.x +" "+ transform.position.y +" "+transform.position.z);
        }
        else
        {
            legSpring = true;
            //
           // print("hipPos2" + transform.position.x + " " + transform.position.y + " " + transform.position.z);

        }
    }
    //2分39s

    void characterRotation()
    {
        //这里要获取到父亲节点
        //会旋转到正方向
        //但是还有问题的是这个旋转的时候好像是绕一另外一个轴旋转
        float speed = 10000f;
        float time1 = 1f;
        //这里的每一个旋转都要重新记录

        //在掉下来之后的一瞬间再转向就被改掉了
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Anim.SetInteger("idleTo", 0);
            Quaternion q = Quaternion.LookRotation(new Vector3(0.01f, 0, 0), Vector3.up);
            character.rotation = Quaternion.Slerp(character.rotation, q, speed * Time.deltaTime);
            // character.Rotate(Vector3.up * speed);
            Vector3 tmp = new Vector3(0, 90f, 0);
            recordData();

        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            //0  -90 0
            Anim.SetInteger("idleTo", 0);
            Quaternion q = Quaternion.LookRotation(new Vector3(-0.01f, 0, 0), Vector3.up);
            character.rotation = Quaternion.Slerp(character.rotation, q, speed * Time.deltaTime);
            Vector3 tmp = new Vector3(0, -90f, 0);
            recordData();

        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            //0 0 0
            Anim.SetInteger("idleTo", 0);
            Quaternion q = Quaternion.LookRotation(new Vector3(0, 0, 0.01f), Vector3.up);
            character.rotation = Quaternion.Slerp(character.rotation, q, speed * Time.deltaTime);
            Vector3 tmp = new Vector3(0, 0f, 0);
            recordData();

        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            //0 -180 0
            Anim.SetInteger("idleTo", 0);
            Quaternion q = Quaternion.LookRotation(new Vector3(0, 0, -0.01f), Vector3.up);
            character.rotation = Quaternion.Slerp(character.rotation, q, speed * Time.deltaTime);
            Vector3 tmp = new Vector3(0, -180f, 0);
            recordData();
        }
    }

    void setAllSpring()
    {
        //   dian                hip
        // 3.5 6.6 13.0                   3.5 7.3 13.0 

        // 10.4 0.5 20.0     2.6 2.1 16.9 
        // 2.6  2.3  16.5
        // 3.1  7.2  13.9    3.0 6.9 14.2
        //这里还要加一个 如果一只脚卡主的情况

        //这个判定2个脚着地的时候有可能会有bug 就是虽然是脚着地
        //但是身体不是直的
        // if (useSpringFlag == true)
        //     UseSpring = true;

        //有时候会倒下去
        //这个的话站立状态，给一个判定也行
        bool ZhanLi=true;

        //这里有重复的会出问题 

        bool Ankle_R_flag1 = Ankle_ROther.IsGrounded1;
        bool Ankle_L_flag1 = Ankle_LOther.IsGrounded2;
        bool Hips_flag1 = hipsOther.IsGrounded1;
        bool UpperLeg_R_flag1 = UpperLeg_R_YOther.IsGrounded1;
        bool LowerLeg_R_flag1 = LowerLeg_R_ZOther.IsGrounded1;

        bool UpperLeg_L_flag1 = UpperLeg_L_YOther.IsGrounded2;
        bool LowerLeg_L_flag1 = LowerLeg_L_ZOther.IsGrounded2;
        if (Hips_flag1 == true || UpperLeg_R_flag1 ==true || LowerLeg_R_flag1==true || UpperLeg_L_flag1==true || LowerLeg_L_flag1==true)
        {
            ZhanLi = false;
        }
        else
        {
            if (Ankle_L_flag1 == false && Ankle_R_flag1 == false)
                ZhanLi = false;
        }

        if(ZhanLi == true)
        {
            //这里来一个调整，使得身体变直
            //感觉还是和pos有关系
            float DirHipY = hips.eulerAngles.y;
            float FrenteHipY = zhengdirectionHipY;
            float DirHipX = hips.eulerAngles.x;
            float FrenteHipX = zhengdirectionHipX;
            float DirHipZ = hips.eulerAngles.z;
            float FrenteHipZ = zhengdirectionHipZ;
            DirHipX = Mathf.LerpAngle(DirHipX, FrenteHipX, Transla * Time.deltaTime);
            DirHipY = Mathf.LerpAngle(DirHipY, FrenteHipY, Transla * Time.deltaTime);
            DirHipZ = Mathf.LerpAngle(DirHipZ, FrenteHipZ, Transla * Time.deltaTime);
            //Rotate.eulerAngles = new Vector3(Rotate.eulerAngles.x, Dir, Rotate.eulerAngles.z);
            //hips.eulerAngles = new Vector3(DirHipX, DirHipY, DirHipZ);
            Vector3 tp = new Vector3(DirHipX, DirHipY, DirHipZ);
            Quaternion q2 = Quaternion.Euler(tp);
            transform.rotation = Quaternion.Slerp(transform.rotation, q2, Time.deltaTime * 1);
        }

        if (Input.GetKey(KeyCode.Q) )
        {
            hp = 5;
            qingXieFlag = false;

            ifIsQKey = true;
            bool useSpringFlag = false;
            if (timeUse == -1 || Time.time - tmpTime < 1f)
            {
                float sped = 50;
                Vector3 movement = new Vector3(0f, 1f, 0f);

                //要给头加
                //这里要判断脚没有离地 脚离地了就不能加了只有旋转
                //if()
                head.GetComponent<Rigidbody>().AddForce(movement * sped);
                useSpringFlag = false;
            }

            if (timeUse == -1)
            {
                tmpTime = Time.time;
                timeUse = 0;
            }
            if (timeUse == 0 && Time.time - tmpTime >= 1f)
                useSpringFlag = true;

            if(timeUse == 0 && Time.time - tmpTime >= 0.1f)
            {
                //腿 用力
                legSpring = true;
                //这个玩意让他变成0 不让他变成其他的就ok ?

            }
            // 会有一个身体直立的判定
            //就是回到了原来的角度
            //感觉这里还要加上位置的变化
            //很明显这里的判断写的还不好加上位置信息就好了
            //我猜是这个判断的问题
            //judgeSim(transform.rotation, hipsRotation)&&
            Vector3 targetPosition = Ankle_L1.position + distanceFootHip;
            if (judgeDis(transform.position,targetPosition))
            {
                print("111Dis");
                timeUse = 0;
                //transform.localEulerAngles = startHipRotation;
                //transform.rotation.Set(hipsRotation.x, hipsRotation.y, hipsRotation.z, hipsRotation.w);
                //Rotate.localEulerAngles = startIkRotation;
                // Rotate.rotation = Quaternion.Lerp(Rotate.rotation, Quaternion.Euler(106f, 0f, 0f), 0.05f);
                //Rotate.rotation = Quaternion.Euler(106f, 0.0f, 0.0f);
                //Rotate.rotation = Quaternion.Lerp(Rotate.rotation, Quaternion.Euler(0, 106f, 0f), 0.05f);
                //Rotate.rotation = Quaternion.Lerp(Rotate.rotation, ikRotation, 0.05f);
                //现在这个旋转的角度已经对上了，然后要修改的是hip的角度和位置
                float DirY = Rotate.eulerAngles.y;
                float FrenteY = zhengdirectionY;
                float DirX = Rotate.eulerAngles.x;
                float FrenteX = zhengdirectionX;
                float DirZ = Rotate.eulerAngles.z;
                float FrenteZ = zhengdirectionZ;
                DirX = Mathf.LerpAngle(DirX, FrenteX, Transla * Time.deltaTime);
                DirY = Mathf.LerpAngle(DirY, FrenteY, Transla * Time.deltaTime);
                DirZ = Mathf.LerpAngle(DirZ, FrenteZ, Transla * Time.deltaTime);
                //Rotate.eulerAngles = new Vector3(Rotate.eulerAngles.x, Dir, Rotate.eulerAngles.z);
                Rotate.eulerAngles = new Vector3(DirX, DirY, DirZ);

                float DirHipY = hips.eulerAngles.y;
                float FrenteHipY = zhengdirectionHipY;
                float DirHipX = hips.eulerAngles.x;
                float FrenteHipX = zhengdirectionHipX;
                float DirHipZ = hips.eulerAngles.z;
                float FrenteHipZ = zhengdirectionHipZ;
                DirHipX = Mathf.LerpAngle(DirHipX, FrenteHipX, Transla * Time.deltaTime);
                DirHipY = Mathf.LerpAngle(DirHipY, FrenteHipY, Transla * Time.deltaTime);
                DirHipZ = Mathf.LerpAngle(DirHipZ, FrenteHipZ, Transla * Time.deltaTime);
                //Rotate.eulerAngles = new Vector3(Rotate.eulerAngles.x, Dir, Rotate.eulerAngles.z);
                //hips.eulerAngles = new Vector3(DirHipX, DirHipY, DirHipZ);
                Vector3 tp = new Vector3(DirHipX, DirHipY, DirHipZ);
                Quaternion q2 = Quaternion.Euler(tp);
                transform.rotation = Quaternion.Slerp(transform.rotation, q2, Time.deltaTime * 100);

                useSpringFlag = true;

            }
            else
            {
                //现在是角度不太对，基本上已经可以了哈哈哈哈 真难。
                //角度还是不对问题很大
                //先换一题， 整理一下思路。
                print("222Dis");
                //错的是这里

                float DirY = Rotate.eulerAngles.y;
                float FrenteY = zhengdirectionY;
                float DirX = Rotate.eulerAngles.x;
                float FrenteX = zhengdirectionX;
                float DirZ = Rotate.eulerAngles.z;
                float FrenteZ = zhengdirectionZ;
                DirX = Mathf.LerpAngle(DirX, FrenteX, Transla * Time.deltaTime);
                DirY = Mathf.LerpAngle(DirY, FrenteY, Transla * Time.deltaTime);
                DirZ = Mathf.LerpAngle(DirZ, FrenteZ, Transla * Time.deltaTime);
                //Rotate.eulerAngles = new Vector3(Rotate.eulerAngles.x, Dir, Rotate.eulerAngles.z);
                //这个直接赋值角度有时候也会有问题
                //会有各种各样的问题 感觉很奇怪 
                //平衡太差了
                Rotate.eulerAngles = new Vector3(DirX, DirY, DirZ);

                //


                float DirHipY = hips.eulerAngles.y;
                float FrenteHipY = zhengdirectionHipY;
                float DirHipX = hips.eulerAngles.x;
                float FrenteHipX = zhengdirectionHipX;
                float DirHipZ = hips.eulerAngles.z;
                float FrenteHipZ = zhengdirectionHipZ;
                //print("Y11" + DirHipY);
                // 到这里好像有点问题
                DirHipX = Mathf.LerpAngle(DirHipX, FrenteHipX, Transla * Time.deltaTime);
                DirHipY = Mathf.LerpAngle(DirHipY, FrenteHipY, Transla * Time.deltaTime);
                DirHipZ = Mathf.LerpAngle(DirHipZ, FrenteHipZ, Transla * Time.deltaTime);



                Vector3 tp = new Vector3(DirHipX, DirHipY, DirHipZ);
                Quaternion q2 = Quaternion.Euler(tp);
                transform.rotation = Quaternion.Slerp(transform.rotation, q2, Time.deltaTime * 10);

                //
                 //这个的坐标要是相对坐标
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, 100 * Time.deltaTime);


            }


            if (useSpringFlag == true)
                UseSpring = true;
            else
                UseSpring = false;

        }
        else
        {
            ifIsQKey = false;
            //legSpring = false;
            for (int i = 0; i < allLeg.Length; i++)
            {
                allLeg[i].GetComponent<SetSpring>().enabled = true;
            }
            //这里把脚本的setspring加回来 

            //然后从上面状态转移过来 这里要有一个站立的判断 最好是双脚以已经着地


            bool Hips_flag = hipsOther.IsGrounded1;
            bool Spine_01_flag = Spine_01Other.IsGrounded1;

            bool UpperLeg_R_flag = UpperLeg_R_YOther.IsGrounded1;
            bool LowerLeg_R_flag = LowerLeg_R_ZOther.IsGrounded1;

            bool UpperLeg_L_flag = UpperLeg_L_YOther.IsGrounded2;
            bool LowerLeg_L_flag = LowerLeg_L_ZOther.IsGrounded2;

            bool Ankle_R_flag = Ankle_ROther.IsGrounded1;
            bool Ankle_L_flag = Ankle_LOther.IsGrounded2;

            //这里还要加东西，其他部位碰到地板的时候也会算是false


            //var centerMess =  ;
            //bool judgeIfIn =  ;
            //这个地方还是有bug的因为还有有可能有脚会卡主
            //还是要回来修的
            //Debug.Log("ankle11" + Ankle_L_flag);
            //Debug.Log("ankle12" + Ankle_R_flag);
            if (Ankle_L_flag == false && Ankle_R_flag == false)
            {

            }
            else
            {
                UseSpring = true;
                //两脚着地 以及其他地方着地，那么就还是不用力
                if (Hips_flag == true || Spine_01_flag == true   )
                    UseSpring = false;

            }
           // Debug.Log("useSpring111" + UseSpring);
            Vector3 down = transform.TransformDirection(Vector3.right);
            RaycastHit hit;
            List<string> ListOfDiMian = new List<string>();
            ListOfDiMian.Add("Cube(7)");
            ListOfDiMian.Add("Cube (2)");
            ListOfDiMian.Add("Cube");
            ListOfDiMian.Add("Plane (1)");
            string frontSt = "";
            string nowSt = "";
            int mask = 1 << 9;
            if (Physics.Raycast(transform.position, down, out hit, 100, mask))
            {
                //print("shexian123");
                Debug.DrawLine(transform.position, hit.point, Color.red);
                //把两个点转化成世界坐标
                //然后和上面的直的向量计算夹角
                //print("hipPos" + transform.position);
                //print("dianPos" + hit.point);
                GameObject gameObj = hit.collider.gameObject;
                //print("front物体名字" + frontSt);
                //print("物体名字" + gameObj.name);

                //这里计算斜度吧 
                //就是这个点和碰到的点然后计算他们的角度
                
                Vector3 tmpVec = hit.point - transform.position;
                Vector3 tmpDown = new Vector3(0, -1, 0);
                float angle = Vector3.Angle(tmpVec, tmpDown);
                print("angle" + angle);
                if (angle >= 60 || angle <= -60)
                    UseSpring = false;
                
                //这个是射过来的物体切换
                if (ListOfDiMian.Contains(gameObj.name))
                {
                    if (frontSt == "")
                        frontSt = gameObj.name;
                    else
                    {
                        if (frontSt != gameObj.name)
                        {
                          //  int a = 1;
                            UseSpring = false;
                        }
                        frontSt = gameObj.name;
                    }
                }

            }
            else
                UseSpring = false;
         //   Debug.Log("useSpring2222" + UseSpring);
            //这里加一个判断速度
            float magnitude = hibRb.velocity.magnitude;
            print("magnitude222" + magnitude);
            
            if (magnitude >= 2 )
            {
                UseSpring = false;
            }
            
            
        }

        // 这里多加一个判断
        //如果脚和身体的角度大于一定值，就要给他倾倒
        if (qingXieFlag == true)
            UseSpring = false;
        if (ifUseSpring == false)
        {
            UseSpring = false;
            legSpring = false;
            Anim.SetInteger("idleTo", 0);
        }

        if(Ankle_ROther.cubeSixteen || Ankle_LOther.cubeSixteen)
        {
            //这里给hip一个向中间的力

            float sped = 500;
            Vector3 movement = cylin.position -  hips.position;

 
            hips.GetComponent<Rigidbody>().AddForce(movement * sped);

            UseSpring = false;
            legSpring = false;
        }
        //这里判断如果脚碰到了这个板子就给他usespring=false;

        //print("ifUseSpring" + ifUseSpring);
        print("UseSpring1199" + UseSpring);
        bool hitFlag = false;
        for(int i=0;i<allWood.Length;i++)
        {
            if(allWood[i].hitWood==true)
            {
                hitFlag = true;
            }
        }
       // print("hitFlag" + hitFlag);
            //private float woodTime = 0;
    //private int woodFlag = false;
        //这里逻辑不太好搞
        //这里搞完 就剩下这个旋转 明年弄完。
        if(hitFlag==true)
        {
            if(woodFlag == false)
            {
                firstWoodFlag = true;
            }
            woodFlag = true;
        }
        else
        {
            firstWoodFlag = false;
            woodFlag = false;
        }
        //这里要加上时间间隔
        if(zhuJue == false&&woodFlag==true)
        {
            if (firstWoodFlag == true&&(woodTime == 0f ||Time.time - woodTime>=1f  ))
            {
                hp = hp - 1;
                if (hp <= 0f)
                    hp = 0;
               // firstWoodFlag = false;
               // woodFlag = false;
                woodTime = Time.time;
            }
        }
        if(UseSpring == false)
        {
            hp = 0;
        }
        if(hp == 0)
        {
            UseSpring = false;
        }
        print("UsingHp" + hp);
        dealAllJointWithSpring();
    }

    void dealAllJointWithSpring()
    {
        if (AllHingeJoints.Length > 0 || AllConfigurableJoints.Length > 0)
        {
            //if (AllHingeJoints[0].useSpring != UseSpring)
            //{
            for (int i = 0; i < AllHingeJoints.Length; i++)
            {
                AllHingeJoints[i].useSpring = UseSpring;
            }

            float val = 0f;
            if (UseSpring == true)
                val = 1000f;
            for (int i = 0; i < AllConfigurableJoints.Length; i++)
            {
                //   AllConfigurableJoints[i].
                drive.positionSpring = val;
                drive.positionDamper = 0f;
                drive.maximumForce = 3.402823e+38f;
                AllConfigurableJoints[i].angularXDrive = drive;
                AllConfigurableJoints[i].angularYZDrive = drive;
            }

            if(justHipSpring == true)
            {
                for (int i = 0; i < AllConfigurableJoints.Length; i++)
                {
                    //   AllConfigurableJoints[i].
                    drive.positionSpring = 10000f;
                    drive.positionDamper = 0f;
                    drive.maximumForce = 3.402823e+38f;
                    AllConfigurableJoints[i].angularXDrive = drive;
                    AllConfigurableJoints[i].angularYZDrive = drive;
                }
            }

            if(legSpring == true)
            {
                //2 -  6 
                //8 - 12

                //这个的话 这里加动画也行 
                for(int i=0;i<allLeg.Length;i++)
                {
                    allLeg[i].GetComponent<SetSpring>().enabled = false;
                }

                for (int i = 2; i <=6; i++)
                {
                    var tmpSpring = AllHingeJoints[i].spring;
                    tmpSpring.targetPosition = 0f;
                    //tmpSpring.spring = 3500f;
                    AllHingeJoints[i].spring = tmpSpring;
                    AllHingeJoints[i].useSpring = true;

                    tmpSpring = AllHingeJoints[i+6].spring;
                    tmpSpring.targetPosition = 0f;
                    //tmpSpring.spring = 100f;
                    AllHingeJoints[i+6].spring = tmpSpring;
                    AllHingeJoints[i+6].useSpring = true;
                }
            }

            if (UseSpring == true)
                TimeReborn = 0;
            //}
            //Debug.Log("AllHingeJoints[0]useSpring" + AllHingeJoints[0].useSpring);
            //Debug.Log("useSpring" + UseSpring);
            //Debug.Log("timeReborn" + TimeReborn);

            int first = 0;
            int second = 0;
            print("timeReborn" + TimeReborn);
            if (TimeReborn <= TempoLevantar)
            {
                TimeReborn += 1 * Time.deltaTime;
                if (UseSpring == true)
                {

                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;


                }
                else
                {
                    
                   RotAnt = transform.rotation;
                    TimeReborn = 500;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    for (int i = 0; i < ColisoresIK.Length; i++)
                    {
                        ColisoresIK[i].enabled = UseSpring;
                    }
                }
            }
            else
            {
                //这里是不会过来的
                //感觉下面这个并没有用
                //这个只会进来一次就是调整方向。
                if (TimeReborn != 500)
                {
                    //11 
                    //这个角度不对
                    //这个只会进来一次就是调整方向。
                    //var AnguloY = Mathf.LerpAngle(transform.eulerAngles.y, CameraRot.eularAngles.y, 2.5f*Time.deltaTime);
                    var AnguloY = Mathf.LerpAngle(transform.eulerAngles.y, zhengdirection, 2.5f * Time.deltaTime);
                    transform.eulerAngles = new Vector3(0, AnguloY, 0);
                    for (int i = 0; i < ColisoresIK.Length; i++)
                    {
                        ColisoresIK[i].enabled = UseSpring;
                    }
                    TimeReborn = 500;
                }
            }
        }
    }
}
