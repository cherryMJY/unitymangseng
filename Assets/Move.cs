using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//这个是物体移动脚本

public class Move : MonoBehaviour
{
    private bool finish = true;
    private Vector3 pos;
    private Animator animThis;
    //private float rotateSpeed = 2; 
    private Transform transformThis;
    private float moveSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        //这边碰到一个坑
        //animThis = this.getComponment<Animator>();
        animThis = GetComponent<Animator>();
        transformThis = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            //判断是否要移动
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //print("" + Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            { 
                pos = hit.point;
                finish = false;

            }
            Debug.Log("111");
            Debug.Log(finish);

        }

        if (!finish)
        {

            Vector3 offset = pos - transform.position;

            //transform.position += offset.normalized * 3 * Time.deltaTime;
            transformThis.Translate(offset.normalized * Time.deltaTime * moveSpeed);
            //transform.rotation += offset.normalized * 2 * Time.deltaTime;
           // float h = offset.normalized.z;
            //print(h)
           // transformThis.Rotate(new Vector3(0,1,0),  5f);
           // transformThis.Rotate(new Vector3(0, -offset.x, 0));

            animThis.SetBool("run", true);

            if (Vector3.Distance(pos, transform.position) < 0.01f)
            {
                transform.position = pos;
                finish = true;
                animThis.SetBool("run", false);
            }

        }

    }
}
