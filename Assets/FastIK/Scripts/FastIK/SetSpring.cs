using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpring : MonoBehaviour
{
    public HingeJoint Joint;
    public Transform Target;
    //[Space(20)]
    // Start is called before the first frame update
    public float Compensador;
    public bool Inverter;
    public enum Eixo { X, Y, Z };
    public Eixo Sentido = Eixo.Z;

    void Start()
    {
        //Joint = gameObject.GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        JointSpring Sp = Joint.spring;
        var Angle = 0f;
        if (Sentido == Eixo.X)
            Angle = Target.localEulerAngles.x;
        if (Sentido == Eixo.Y)
            Angle = Target.localEulerAngles.y;
        if (Sentido == Eixo.Z)
            Angle = Target.localEulerAngles.z;
        //Debug.Log("angle " + Angle);
        //Angle += Time.time;
        if (Inverter)
            Angle *= -1;
        if (Angle < 180)
            Angle = Angle + 360;
        if (Angle > 180)
            Angle = Angle - 360;
        Angle += Compensador;
        //Sp.spring = 500f+Time.time;
        Sp.targetPosition = Angle;
        Debug.Log("Angle" + Angle);

        Joint.spring = Sp;
        Debug.Log("target position" + Joint.spring.targetPosition);
    }
}
