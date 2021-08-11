using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shexian : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 down = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, down, out hit, 10))
        {
            print("shexian123");
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }

    }
}
