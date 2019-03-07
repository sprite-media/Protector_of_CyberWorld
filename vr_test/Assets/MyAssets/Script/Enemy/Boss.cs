using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Boss : Enemy
{
    #region basic status

    float closeRangeAttackSpeed = 7.0f;
    float curTime = 0.0f;
    #endregion

    #region movement related variables
    [SerializeField] float maxSpeed         = 10.0f;
    [SerializeField] float minSpeed         = 3.0f;
    [SerializeField] float incrementSpeed   = 0.1f;
    [SerializeField] float slowingDistance  = 3.0f;
    [SerializeField] float stoppingDistance = 1.0f;
    [SerializeField] float curSpeed         = 0.0f;
    [SerializeField] float rotateSpeed      = 10.0f;
    [SerializeField] GameObject aim;
    #endregion

    #region State related variables and Enum
    private enum STATE //planning to add more State
    {
        IDLE,
        MOVE_F,
        HIT,
        ATTACK_C //C stands for close
    }
    [SerializeField] STATE curState = STATE.IDLE;
    [SerializeField] Transform curTarget = null;
    bool isDead = false;
    bool isDeadUsed = false;
    #endregion

    //attack type string
    string[] attackTypeString = { "Projectile Attack 01", "Projectile Attack 02",
                                 "Telekinesis Swing Right", "Telekinesis Swing Left" };

    public List<GameObject> targetList;
    private Animator animator;
    #region Singleton
    private static Boss instance;
    public static Boss Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    }
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();

        gameObject.SetActive(false);

        hp = 50.0f;
        damage = 2.0f;
    }
    private void Update()
    {
        if (isDead) return;
        StateSelector();
        curTime += 0.1f;
    }

    private void StateSelector()
    {
        if (targetList.Count == 0)
        {
            curTarget = null;
            return;
        }
        curTarget = targetList[0].transform;
 
        switch (curState)
        {
            case STATE.IDLE:
                curState = STATE.MOVE_F;
                break;
            case STATE.MOVE_F:
                Move_F();
                break;
            case STATE.HIT:
                GetHit();
                break;
            case STATE.ATTACK_C: //Clost range
                if (curTime >= closeRangeAttackSpeed)
                {
                    curTime = 0.0f;
                    CloseRangeAttack();
                }
                break;
            default:
                break;
        }
    }

    #region Action funtions
    private void Move_F()
    {
        float targetDistance = Vector3.Distance(transform.position, curTarget.position);
        Vector3 temp = new Vector3(curTarget.position.x, -4.5f, curTarget.position.z);
        var quaternion = Quaternion.LookRotation(temp - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, rotateSpeed * Time.deltaTime); // make roation first

        if(targetDistance > slowingDistance) //increase the speed
        {
            animator.SetBool("Move Forward Fast", true);
            if(curSpeed < maxSpeed)         
            {
                if (curSpeed <= 0)
                {
                    curSpeed = minSpeed;
                    return;
                }
                curSpeed += incrementSpeed;
                if (curSpeed > maxSpeed)
                    curSpeed = maxSpeed;
            }         
        }
        else
        {
            if (targetDistance < stoppingDistance) //Stopping Area
            {
                animator.SetBool("Move Forward Fast", false);
                curSpeed = 0;
                curState = STATE.ATTACK_C;
                return;
            }
            if (curSpeed < minSpeed) //slowing Area
                curSpeed = minSpeed;
            else
                curSpeed -= 0.001f;
        }
        transform.Translate(Vector3.forward * curSpeed * Time.deltaTime); //Move forward
    }

    private void CloseRangeAttack()
    {
        int attackIndex = Random.Range(0, 4);
        animator.SetBool(attackTypeString[attackIndex], true);
    }
    public void AttackApply(AnimationEvent animationEvent)
    {
        targetList[0].GetComponent<PlayerBuilding>().TakeDamage(damage);
    }

    private void GetHit()
    {
        animator.SetBool("Take Damage", true);
    }
    public void BackToIdleFromHiState(AnimationEvent animationEvent)
    {
        curState = STATE.IDLE;
    }
    #endregion

    #region Hit, Get Damaged..
    public override void Death()
    {
        if (!isDead && !isDeadUsed)
        {
            isDead = true;
            isDeadUsed = true;
            animator.SetBool("Die", isDead);
            Base.Instance.ReduceNumEnemy();
        }
    }
    public void DeadAnimationHandler_1(AnimationEvent animationEvent)
    {
        animator.SetBool("Die", false);
    }
    public void DeadAnimationEventHandler(AnimationEvent animationEvent)
    {
        Destroy(gameObject, 2.0f);
    }
    public void AimableDelayEventHandler(AnimationEvent animationEvent)
    {
        aim.SetActive(true);
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (Random.Range(0, 100) == 1) 
            curState = STATE.HIT;
    }
    #endregion


    #region targetList related functions
    public void PickTheTarget()
    {
        targetList.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(transform.position, a.transform.position).
                CompareTo(Vector3.Distance(transform.position, b.transform.position));
        });

        for (int i = 0; i < targetList.Count; i++)
            targetList[i].GetComponent<PlayerBuilding>().number = i;
    }

    public void UpdateTargetList(int indexToRemove)
    {
        curState = STATE.IDLE;
        targetList.RemoveAt(indexToRemove);
        PickTheTarget();
    }
    #endregion

}
