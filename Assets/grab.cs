using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{
    private bool hold;
    public KeyCode grabKey;
    public bool canGrab;

    void Update()
    {
        if (canGrab)
        {
            if (Input.GetKey(grabKey))
            {
                hold = true;
            }
            else
            {
                hold = false;
                Destroy(GetComponent<FixedJoint>());
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (hold && col.transform.tag != "Player")
        {
            Rigidbody rb = col.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("" + 123);
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                fj.connectedBody = rb;
                
            }
            else
            {
                Debug.Log("" + 456);
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
            }
        }
    }
}
