using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteout : MonoBehaviour
{
    private MeshRenderer block = null;

    private float speed = 0.5f;
    private float multiplier = 1;
    private bool start = false;
    private float alpha = 0;

    private static whiteout _instance;
    public static whiteout instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
        block = gameObject.GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (start)
        {
            alpha += speed * multiplier * Time.unscaledDeltaTime;

            if (alpha >= 1.0f)
            {
                alpha = 1.0f;
                multiplier *= -1;
            }
            else if (alpha < 0)
            {
                alpha = 0.0f;
                start = false;
            }

            block.material.SetColor("_Color", new Color(1, 1, 1, alpha));
        }
    }

    public void StartWhiteOut()
    {
        Debug.Log("whiteout");
        alpha = 0;
        start = true;
        multiplier = 1;
    }
}
