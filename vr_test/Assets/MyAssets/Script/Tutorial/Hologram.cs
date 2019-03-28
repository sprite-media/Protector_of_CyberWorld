using UnityEngine;

public class Hologram : MonoBehaviour
{
    [SerializeField] GameObject[] viruses;
    int curIdx = 0;


    float changeTime =  48.0f;
    float curChangeTime = 0.0f;

    private void Start()
    {
        ChangeDisplay();
    }

    private void Update()
    {
        curChangeTime += 0.1f;
        if(curChangeTime >= changeTime)
        {
            curChangeTime = 0.0f;

            if (curIdx + 1 < viruses.Length)
                curIdx += 1;
            else
                curIdx = 0;

            ChangeDisplay();
        }
    }


    private void ChangeDisplay()
    {
        for (int i = 0; i < viruses.Length; i++)
        {
            viruses[i].SetActive(false);
        }
        viruses[curIdx].SetActive(true);
    }

}
