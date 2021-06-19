using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//如果交不了拆直接用这个东西就好了 

public class Move_1 : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    Vector3 old_pos = Vector3.zero;      //移动前的位置
    Vector3 step = Vector3.zero;        //位移向量
    Vector3 aimPoint = Vector3.zero;    //目标位置
    Quaternion ratation;                //要转向的目标方向
    private Animator animThis;  //状态机

    //CharacterController controller;
    public float speed=3;

    void Start()
    {
        animThis = GetComponent<Animator>(); 
    }

    //void Awake()
    //{
     //   controller = GetComponent<CharacterController>();
    //}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //if (hit.collider.gameObject.CompareTag("plane"))
                //{
                    old_pos = transform.position;
                    aimPoint = hit.point;
                    aimPoint.y = 0;
                    step = aimPoint - transform.position;
                    ratation = Quaternion.LookRotation(step);
                //}
            }
        }
        DoMove();
        isToAim();

        //这里接着其他的东西
    }

    void DoMove()
    {
        if (step != Vector3.zero)
        {
            animThis.SetBool("run", true);
            transform.rotation = Quaternion.Lerp(transform.rotation, ratation, 0.1f);
            transform.position += step.normalized * 3 * Time.deltaTime;
            //controller.Move(Vector3.ClampMagnitude(step, 0.1f) * speed);
        }
    }

    void isToAim()
    {
        if ((transform.position - old_pos).magnitude >= step.magnitude)
        {
            step = Vector3.zero;
            animThis.SetBool("run", false);
        }
    }
}
