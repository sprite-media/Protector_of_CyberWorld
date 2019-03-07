using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Boss : MonoBehaviour
{
    private static Boss instance;
    public static Boss Instance
    {
        get { return instance; }
    }

    public List<GameObject> targetList;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void PickTheTarget()
    {
        targetList.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(transform.position, a.transform.position).
                CompareTo(Vector3.Distance(transform.position, b.transform.position));
        });


        for (int i = 0; i < targetList.Count; i++)
        {
            targetList[i].GetComponent<PlayerBuilding>().number = i;

            //Debug.Log("name:" + targetList[i].name);

        }
    }

    public void UpdateTargetList(int indexToRemove)
    {
        targetList.RemoveAt(indexToRemove);
        //Debug.Log("targetList length: " + targetList.Count);
        PickTheTarget();
    }

}
