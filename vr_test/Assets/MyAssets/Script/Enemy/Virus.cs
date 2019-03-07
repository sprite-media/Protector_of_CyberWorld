using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Virus : Enemy
{
    public enum State
    {
        Idle,
        Path,
        InRange,
        Attack
    }
    protected State currentState = State.Path;

    //All enemies share the same targets
    private static PlayerBuilding[][] TargetList = null;    // array0 : Base   array1 : Towers    array2 : Traps
    public static PlayerBuilding[][] Targets { get { return TargetList; } }

    protected int pathType;
    public int PathType { get { return pathType; } set { pathType = value; } }
    private int pathIndex = 0;
    private Vector3 targetPath = Vector3.zero;

    private PlayerBuilding targetBuilding = null;
    public PlayerBuilding Target { get { return targetBuilding; } }
    public bool HasTarget { get { if (currentState == State.Attack) return true; else return false; } }
    private Vector3 targetBuildingPos = Vector3.zero;
    private float attackTimer = 0.0f;
    protected float attackCoolTime = 0.0f;
    protected int[] priority = null; // if priority starts from -1, it attacks all buildings
    private int priorityIndex = 0;

    private GameObject particle = null;

    public float Power { get { return damage; } }
    protected float detectRange = 1.5f;
    protected float attackRange = 1.0f;
    protected float speed = 3.0f;

    protected void Awake()
    {
        pathIndex = 0;
        pathType = transform.position.x <= PathFinding.Path[0][0].x ? 0 : 1;
        priority = new int[2];
        priorityIndex = 0;

        particle = transform.Find("Particle_EnemyDeath").gameObject;
        particle.SetActive(false);

        if (TargetList == null)
        {
            TargetList = new PlayerBuilding[2][];

            //Second array is tower
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Tower");
            TargetList[0] = new PlayerBuilding[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                TargetList[0][i] = temp[i].GetComponent<Tower>();
            }

            //Third array is Trap
            temp = GameObject.FindGameObjectsWithTag("Trap");
            TargetList[1] = new PlayerBuilding[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                TargetList[1][i] = temp[i].GetComponent<Trap>();
            }
        }
    }

    protected void Start()
    {
        UpdateTargetPath();
    }

    public virtual void Update()
    {
        switch ((int)currentState)
        {
            case (int)State.Idle:
                DetectTarget();
                //Animation control
                break;
            case (int)State.Path:
                MoveToPath();
                break;
            case (int)State.InRange:
                MoveToTarget();
                break;
            case (int)State.Attack:
                Attack();
                break;
            default:
                Debug.LogError(gameObject.name + " state error. \n Destroying the gameObejct");
                Base.Instance.ReduceNumEnemy();
                Destroy(gameObject);
                break;
        }
    }

    private void UpdateTargetPath()
    {
        Vector3 temp = PathFinding.Path[pathType][pathIndex];
        targetPath = PathFinding.Path[pathType][pathIndex]; //new Vector3(temp.x, transform.localScale.y / 2.0f, temp.z);
    }
    private void UpdateTargetBuilding(PlayerBuilding target)
    {
        targetBuilding = target;
        Vector3 temp = targetBuilding.transform.position;
        targetBuildingPos = targetBuilding.transform.position; //new Vector3(temp.x, transform.localScale.y / 2.0f, temp.z);
        currentState = State.InRange;
    }
    public void BackToPath()
    {
        for (int i = pathIndex; i < PathFinding.Path[pathType].Length; i++)
        {
            Vector3 temp = PathFinding.Path[pathType][i];
            if (temp.z <= transform.position.z)
            {
                pathIndex = i;
                UpdateTargetPath();
                currentState = State.Path;
                return;
            }
        }
        Debug.LogWarning("Cannot Find Path.");
        currentState = State.Idle;
    }

    public virtual void DetectTarget()
    {
        if (priority[0] == -1)// No priority. Attack any closest building
        {
            for (int j = 0; j < priority.Length; j++)
            {
                for (int i = 0; i < TargetList[j].Length; i++)
                {
                    if (TargetList[j][i] != null)
                    {
                        if (Vector3.Distance(transform.position, TargetList[j][i].transform.position) <= detectRange)
                        {
                            UpdateTargetBuilding(TargetList[j][i]);
                            return;
                        }
                    }
                    else
                        continue;
                }
            }
        }
        else
        {
            // if all high priority buildings are destroyed, detect next priority
            for (int i = priorityIndex; i < priority.Length; i++)
            {
                if (TargetList[priority[priorityIndex]] == null)
                {
                    priorityIndex++;
                }
            }

            //if  there is target in range
            for (int i = 0; i < TargetList[priority[priorityIndex]].Length; i++)
            {
                if (TargetList[priority[priorityIndex]][i] != null)
                {
                    if (Vector3.Distance(transform.position, TargetList[priority[priorityIndex]][i].transform.position) <= detectRange)
                    {
                        UpdateTargetBuilding(TargetList[priority[priorityIndex]][i]);
                        return;
                    }
                }
                else
                    continue;
            }
        }
    }
    public virtual void MoveToTarget()
    {
        if (targetBuilding != null)
        {
            Vector3 temp = new Vector3(targetBuildingPos.x, transform.position.y, targetBuildingPos.z);
            transform.LookAt(temp);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, temp) < attackRange)
            {
                currentState = State.Attack;
            }
        }
        else
        {
            currentState = State.Path;
        }
    }
    public virtual void MoveToPath()
    {
        DetectTarget();
        transform.LookAt(targetPath);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPath) < 0.1f)
        {
            pathIndex++;
            if (pathIndex >= PathFinding.Path[pathType].Length)
            {
                UpdateTargetBuilding(Base.Instance.BASE);
            }
            else
            {
                UpdateTargetPath();
            }
        }
        // animation control
    }

    public virtual void Attack()
    {
        Vector3 temp = new Vector3(targetBuildingPos.x, transform.position.y, targetBuildingPos.z);
        transform.LookAt(temp);
        if (targetBuilding == null)
        {
            BackToPath();
        }
    }

    public override void Death()
    {
        particle.SetActive(true);
        particle.transform.parent = null;
        Destroy(particle, 3);
        base.Death();
    }
}
