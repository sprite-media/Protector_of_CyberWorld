using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    public enum Axis
    {
        X,
        Y,
        Z
    }
    public Axis AxisToRotate = Axis.X;
    public float speed;


    public void Update()
    {
        switch ((int)AxisToRotate)
        {
            case (int)Axis.X:
                transform.Rotate(speed * Time.deltaTime, 0, 0);
                break;
            case (int)Axis.Y:
                transform.Rotate(0, speed * Time.deltaTime, 0);
                break;
            case (int)Axis.Z:
                transform.Rotate(0, 0, speed * Time.deltaTime);
                break;
        }
    }
}
