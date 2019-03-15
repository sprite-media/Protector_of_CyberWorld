using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CubeMomentum : MonoBehaviour
{
    [SerializeField] bool isMovingX = false;
    [SerializeField] bool isMovingY = false;
    [SerializeField] bool isMovingZ = false;

    float curXPos = 0.0f;
    float curZPos = 0.0f;
    float curYPos = 0.0f;

    [SerializeField]  float speed = 3.0f;
    [SerializeField]  int xDir = 1;
    [SerializeField]  int yDir = 1;
    [SerializeField]  int zDir = 1;

    bool xRotOnce = false;
    bool yRotOnce = false;
    bool zRotOnce = false;

    private void Update()
    {
        if (isMovingX)
            MoveOnXAxis();
        if (isMovingY)
            MoveOnYAxis();
        if (isMovingZ)
            MoveOnZAxis();
    }

    public void MoveOnXAxis()
    {
        transform.Translate(Vector3.right * xDir * speed * Time.deltaTime);
        StartCoroutine(ChangeXDir(15));
    }

    public void MoveOnYAxis()
    {
        transform.Translate(Vector3.up * yDir * speed * Time.deltaTime);
        StartCoroutine(ChangeYDir(10));
    }

    public void MoveOnZAxis()
    {
        transform.Translate(Vector3.forward * zDir * speed * Time.deltaTime);
        StartCoroutine(ChangeZDir(15));
    }

    IEnumerator ChangeXDir(float sec)
    {
        if (!xRotOnce)
        {
            xRotOnce = true;
            yield return new WaitForSeconds(sec);
            xDir *= -1;
            xRotOnce = false;
        }
    }

    IEnumerator ChangeYDir(float sec)
    {
        if (!yRotOnce)
        {
            yRotOnce = true;
            yield return new WaitForSeconds(sec);
            yDir *= -1;
            yRotOnce = false;
        }
    }

    IEnumerator ChangeZDir(float sec)
    {
        if (!zRotOnce)
        {
            zRotOnce = true;
            yield return new WaitForSeconds(sec);
            zDir *= -1;
            zRotOnce = false;
        }
    }
}
