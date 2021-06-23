using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vision : MonoBehaviour
{
    public Transform border1, border2, pointer;
    public Transform lookAtTarget;
    public bool canSeeTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointer.LookAt(lookAtTarget);

        if(pointer.localRotation.y > border1.localRotation.y && pointer.localRotation.y < border2.localRotation.y)
        {

            RaycastHit hit;
            Vector3 fromPosition = pointer.position;
            Vector3 toPosition = lookAtTarget.transform.position;
            Vector3 direction = toPosition - fromPosition;
            if (Physics.Raycast(fromPosition, direction, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    canSeeTarget = true;
                }
            }
        }
        else
        {
            canSeeTarget = false;
        }
    }
}
