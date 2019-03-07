using UnityEngine;

public class Virus2 : Enemy
{
	private static GameObject virus3 = null;
	[SerializeField]
	private Transform target = null;
	private float virusDetectRange;

	private new void Awake()
	{
		base.Awake();
		hp = 3.0f;
		power = 1.0f;
		speed = 5.0f;
		virusDetectRange = 6.0f;
		if (virus3 == null)
		{
			virus3 = Resources.Load("V3", typeof(GameObject)) as GameObject;
		}
	}
	protected new void Start()
	{
		base.Start();
		priority = new int[] { 1, 0 };
	}
	public override void Update()
	{
		base.Update();
		if(target == null)
			HasVirus();
	}
	public override void DetectTarget()
	{
		
		if(target == null)
			base.DetectTarget();
	}
	public override void MoveToTarget()
	{
		if (target == null)
			base.MoveToTarget();
		else
		{
			transform.LookAt(target.position);
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
			if (Vector3.Distance(transform.position, target.position) < 1.0f)
			{
                Base.Instance.DereaseTheEnemyNum();
                GameObject temp = (GameObject)Instantiate(virus3, transform.position, transform.rotation);
				temp.GetComponent<Virus3>().PathType = this.pathType;
				temp.GetComponent<Virus3>().BackToPath();
				Destroy(target.gameObject);
				Destroy(gameObject);
			}
		}
	}
	private void HasVirus()
	{
		foreach(GameObject g in EnemyContainer.Enemies)
		{
			if (g != null)
			{
				Enemy e = g.GetComponent<Enemy>();
				if (e is Virus1)
				{
					if (Vector3.Distance(transform.position, e.transform.position) < virusDetectRange)
					{
						target = e.transform;
						currentState = State.InRange;
					}
				}
			}
		}
	}
}
