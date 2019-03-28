using System.Collections;
using UnityEngine;
using MoenenGames.VoxelRobot;

public class Tower : PlayerBuilding
{
	public enum Type
	{
		None,
		Tower1,
		Tower2
	}
	public Type towerType;
    [SerializeField]
	private bool lookUp;
	private Weapon[] weapons = null;
	[SerializeField]
	private GameObject part;
	private int dropIndex = 0;
	private float partialDestructionDamage;
	private const float MAX_HP = 20.0f;

	[SerializeField]
	private AudioClip deathClip;
	public AudioSource aud { get { return audio; } }
    
    protected Transform target;

	public bool HasTarget {
		get
		{
			if (target != null)
				return true;
			else
				return false;
		}
	}

    public float range = 5.0f;

    protected Transform partToRatate;

    public float turnSpeed = 10f;


    // Start is called before the first frame update
    protected void Start()
    {
		hp = MAX_HP;
		InvokeRepeating("UpdateTarget", 0f, 0.5f); // Inorder to not to call UpdateTarget function in every frame.
        partToRatate = transform.Find("Base");
		weapons = transform.Find("Base").Find("Turret").GetComponentsInChildren<Weapon>();

		partialDestructionDamage = MAX_HP / (float)weapons.Length;
    }

    public void Update()
    {
		if (Base.Instance.GetTotalNumEnemy() <= 0)
		{
			target = null;
		}

		if (target == null)
				return;

			Vector3 dir = target.position - transform.position;
			Quaternion lookRotation = Quaternion.LookRotation(dir);
			Vector3 rotation = Quaternion.Lerp(partToRatate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        if(lookUp)//tower1
        {
            if(Base.Instance.GetTotalNumEnemy() == 1)
                partToRatate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
            else
                partToRatate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }		
        else //tower2
            partToRatate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
	public virtual void Shoot()
	{
		foreach (Weapon w in weapons)
		{
			if (w.ReadyToShoot)
			{
				w.Attack();
			}
		}
	}
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
	public void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in EnemyContainer.Enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (Base.Instance.GetTotalNumEnemy() == 1)
        {
            range = 15;
        }


        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
	public bool Repair()
	{
		if (dropIndex > 0)
		{
			dropIndex--;
			hp = Mathf.Clamp(hp + partialDestructionDamage, 0, MAX_HP);
			weapons[dropIndex].gameObject.SetActive(true);
			gameObject.tag = "Tower";
            Boss.Instance.AddToTargetList(gameObject);
            //Debug.Log("Added " + gameObject.name);
            return true;
		}
		else
		{
			return false;
		}
	}
	public override void TakeDamage(float amt)
	{
        hp = Mathf.Clamp(hp - amt, 0, hp);

        int numIteration = (int)((MAX_HP - hp) / partialDestructionDamage);
		bool once = false;
		while(dropIndex < numIteration)
		{
			/*/
			if (!once)
			{
				GameObject particle = (GameObject)Instantiate(DeathEffect, DeathEffect.transform.position, DeathEffect.transform.rotation);
				particle.SetActive(true);
				Destroy(particle, 1.5f);
				once = true;
			}
			//*/
			GameObject temp = (GameObject)Instantiate(part, weapons[dropIndex].transform.position, weapons[dropIndex].transform.rotation);
			temp.GetComponent<TowerPart>().TurnOnTypeTimer((TowerPart.Type)this.towerType, 1.5f);
			weapons[dropIndex].gameObject.SetActive(false);
			dropIndex++;
		}

        if (hp <= 0 && gameObject.tag != "Destroyed")
        {
            Death();
        }
    }
	public override void Death()
	{
        base.Death();
		/*/
        if (DeathEffect)
        {
            DeathEffect.SetActive(true);
            DeathEffect.transform.parent = null;
            Destroy(DeathEffect, 1.5f);
        }
		//Destroy(gameObject);
		//*/
		gameObject.tag = "Destroyed";
    }

}
