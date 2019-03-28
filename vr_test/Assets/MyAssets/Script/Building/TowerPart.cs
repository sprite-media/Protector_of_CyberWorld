using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPart : MonoBehaviour
{
	public enum Type
	{
		None,
		Tower1,
		Tower2
	}

	public Type partType = Type.None;
	private Type tempType;
	private float tempTime;
    private bool used = false;

	public void TurnOnTypeTimer(Type type, float time)
	{
		tempType = type;
		tempTime = time;
		StartCoroutine("TypeTimer");
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (!used)
        {
            GameObject temp = collision.gameObject;
            if (temp.tag == "Destroyed" || temp.tag == "Tower")
            {
                if ((Type)temp.GetComponent<Tower>().towerType == this.partType)
                {
                    if (temp.GetComponent<Tower>().Repair())
                    {
                        used = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
	}

	IEnumerator TypeTimer()
	{
		yield return new WaitForSeconds(tempTime);
		this.partType = tempType;
	}
}