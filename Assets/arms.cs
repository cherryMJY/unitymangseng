using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arms : MonoBehaviour
{
    public bool invert;

    public float torqueForce;
    public float angularDamping;
    public float maxForce;
    public float springForce;
    public float springDamping;

    public Vector3 targetVel;

    public Transform target;
    private GameObject limb;
    private JointDrive drive;
    private SoftJointLimitSpring spring;
    private ConfigurableJoint joint;
    private Quaternion startingRotation;

    public Transform thingy;

    //这个东西输入的应该是直接输入 而不是这个样子拖进来了
    public KeyCode grabKey;

    public Transform cam;
    public Transform camRot;
    public Transform com;
    public HingeJoint lowerArm;
    public bool TargetMode;
    public bool canUse;

    void Start()
    {
        invert = false;

        torqueForce = 500f;
        angularDamping = 0.0f;
        maxForce = 500f;

        springForce = 0f;
        springDamping = 0f;

        targetVel = new Vector3(0f, 0f, 0f);

        drive.positionSpring = torqueForce;
        drive.positionDamper = angularDamping;
        drive.maximumForce = maxForce;

        spring.spring = springForce;
        spring.damper = springDamping;

        joint = gameObject.GetComponent<ConfigurableJoint>();

        joint.slerpDrive = drive;

        joint.linearLimitSpring = spring;
        joint.rotationDriveMode = RotationDriveMode.Slerp;
        joint.projectionMode = JointProjectionMode.None;
        joint.targetAngularVelocity = targetVel;
        joint.configuredInWorldSpace = false;
        joint.swapBodies = true;

        startingRotation = Quaternion.Inverse(target.localRotation);
    }

    private void Update()
    {

        if (Input.GetKey(grabKey) && canUse)
        {
            if (TargetMode)
            {
                target.localEulerAngles = new Vector3(-cam.eulerAngles.x, 0, 0);
                lowerArm.useSpring = true;
                if (com) { com.rotation = Quaternion.LookRotation(camRot.forward); }
            }
            else
            {
                target.LookAt(thingy);
                lowerArm.useSpring = true;
            }
        }
        else
        {
            target.localEulerAngles = new Vector3(90, 0, 0);
            lowerArm.useSpring = false;
        }
    }

    void LateUpdate()
    {
        if (invert)
            joint.targetRotation = Quaternion.Inverse(target.localRotation * startingRotation);
        else
            joint.targetRotation = target.localRotation * startingRotation;
    }
}
