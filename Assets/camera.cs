using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public bool aiming;
    private Transform targetTransform;
    public Transform aimingTarget;
    public Transform notAimingTarget;
    public GameObject aimer;
    private Camera _Cam;
    public Camera Cam
    {
        get
        {
            if (_Cam == null)
            {
                _Cam = GetComponent<Camera>();
            }
            return _Cam;
        }
    }
    public Vector3 CamOffset = Vector3.zero;
    public Vector3 ZoomOffset = Vector3.zero;
    public float senstivity = 50;
    public float minY = 30;
    public float maxY = 50;
    public bool isZooming;
    private float currentX = 0;
    private float currentY = 1;
    void Update()
    {
        currentX += Input.GetAxis("Mouse X") * senstivity;
        currentY += Input.GetAxis("Mouse Y") * senstivity;
        currentX = Mathf.Repeat(currentX, 360);
        currentY = Mathf.Clamp(currentY, minY, maxY);

        if (aiming)
        {
            aimer.SetActive(true);
            targetTransform = aimingTarget;
            CamOffset = new Vector3(0, 0, -8);
        }
        else
        {
            aimer.SetActive(false);
            targetTransform = notAimingTarget;
            CamOffset = new Vector3(0, 0, -10);
        }
    }

    void LateUpdate()
    {
        Vector3 dist = isZooming ? ZoomOffset : CamOffset;
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = targetTransform.position + rotation * dist;
        transform.LookAt(targetTransform.position);
        CheckWall();
    }
    public LayerMask wallLayer;
    void CheckWall()
    {
        RaycastHit hit;
        Vector3 start = targetTransform.position;
        Vector3 dir = transform.position - targetTransform.position;
        float dist = CamOffset.z * -1;
        if (Physics.Raycast(targetTransform.position, dir, out hit, dist, wallLayer))
        {
            float hitDist = hit.distance;
            Vector3 sphereCastCenter = targetTransform.position + (dir.normalized * hitDist);
            transform.position = sphereCastCenter;
        }
    }

}
