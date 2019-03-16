using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float dir;
    private float rotSpeed;
    private float speed;

    private bool isRotatableY;
    private bool isMoveForward;
    private bool isAnyKeyPress;
    Vector3 angle;
    // Start is called before the first frame update
    void Start()
    {
        dir = 0.0f;
        rotSpeed = 20.0f;
        speed = 0.0f;

        isRotatableY = false;
        isMoveForward = false;
        isAnyKeyPress = false;
        angle.y = 180.0f;
        angle.x = 0;
        angle.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.W))
        {
            isMoveForward = isMoveForward ? false : true;

            if (!isMoveForward)
                speed = 0.0f;
            else
                speed = 5.0f;
        }
    }

    void CameraRotation()
    {

        transform.rotation = Quaternion.Euler(angle.x, angle.y, angle.z);

        if (Input.GetKeyUp(KeyCode.A))
        {
            isAnyKeyPress = true;
            isRotatableY = isRotatableY ? false : true;
            if (!isRotatableY)
                dir = 0;
            else
                dir = -1;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            isAnyKeyPress = true;
            isRotatableY = isRotatableY ? false : true;
            if (!isRotatableY)
                dir = 0;
            else
                dir = 1;
        }

        if(isAnyKeyPress)
            angle.y += (rotSpeed * dir) * Time.deltaTime;
    }
}
