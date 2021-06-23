using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour
{

    public HingeJoint upLegL, downLegL, upLegR, downLegR, footL, footR, downArmL, downArmR, head;
    public arms upArmL, upArmR;
    public ConfigurableJoint balancer;
    public walk Walk;
    public Animator anim;
    public playerGun ps;
    public bool player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            die();
        }
    }

    public void die()
    {
        if (player)
        {
            Destroy(Walk);
            Destroy(ps);
            upArmL.canUse = false; upArmR.canUse = false;
        }
        Destroy(balancer);
        anim.Play("idle");
        upLegL.useSpring = false; downLegL.useSpring = false; upLegR.useSpring = false; downLegR.useSpring = false; footL.useSpring = false; footR.useSpring = false; downArmL.useSpring = false; downArmR.useSpring = false; head.useSpring = false;
        
    }
}
