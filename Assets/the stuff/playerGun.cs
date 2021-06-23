using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGun : MonoBehaviour
{
    private float distanceToClosestGun;
    public string targTag;
    private GameObject ClosestGun;
    public Transform body;
    public bool hasGun;
    private GameObject myGun;
    public grab leftHand, rightHand;
    public arms leftArm;
    public camera cam;

    private void Update()
    {
        findClosestGun();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hasGun)
            {
                throwGun();
            }
            else
            {
                if(distanceToClosestGun < 5.5) { pickUpGun(); }
            }
        }

        if (hasGun)
        {
            leftHand.canGrab = false; rightHand.canGrab = false;
            leftArm.canUse = false;
            if (Input.GetMouseButton(1))
            {
                cam.aiming = true;
            }
            else
            {
                cam.aiming = false;
            }
        }
        else
        {
            leftHand.canGrab = true; rightHand.canGrab = true;
            leftArm.canUse = true;
            cam.aiming = false;
        }

        if (cam.aiming)
        {
            gun myGunsScript = myGun.GetComponent<gun>();
            if (myGunsScript.oneShotAtATime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    myGunsScript.shoot(null);
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    myGunsScript.shoot(null);
                }
            }
        }
    }

    void findClosestGun()
    {
        distanceToClosestGun = Mathf.Infinity;
        GameObject[] Guns = GameObject.FindGameObjectsWithTag(targTag);
        float distanceToGun;

        foreach (GameObject Gun in Guns)
        {
            distanceToGun = Vector3.Distance(Gun.transform.position, body.position);
            if (distanceToGun < distanceToClosestGun)
            {
                distanceToClosestGun = distanceToGun;
                ClosestGun = Gun;
            }
        }
    }

    void pickUpGun()
    {
        myGun = ClosestGun;
        hasGun = true;
        myGun.GetComponent<gun>().Activate(rightHand.transform);
    }

    void throwGun()
    {
        myGun.GetComponent<gun>().DeActivate();
        myGun = null;
        hasGun = false;
    }
}
