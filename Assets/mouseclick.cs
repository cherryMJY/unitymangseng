using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseclick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Animator anim = this.GetComponent<Animator>();
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("moustclick", true);
            Debug.Log("111");
           
        }
   
       
    }
}
