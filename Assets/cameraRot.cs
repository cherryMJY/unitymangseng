using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRot : MonoBehaviour
{
    public Transform realCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, realCam.eulerAngles.y, 0);
    }
}
