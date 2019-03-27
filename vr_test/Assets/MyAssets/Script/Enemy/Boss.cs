using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Boss : Enemy
{
    #region basic status

    float closeRangeAttackSpeed = 7.0f;
    float curTime = 0.0f;

    float jumpTimer = 20f;
    float curJumpTimer = 0.0f;
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

    [SerializeField] GameObject FlyingParticle;
    [SerializeField] GameObject LandingParticle;

    #region State related variables and Enum
    private enum STATE //planning to add more State
    {
        IDLE,
        MOVE_F,
        HIT,
        ATTACK_C, //C stands for close
        JUMP
    }
    [SerializeField] STATE curState = STATE.IDLE;
    [SerializeField] Transform curTarget = null;
    bool isDead = false;
    bool isDeadUsed = false;
    #endregion

    bool isJumping = false;
    float maxHp = 150.0f;

    //attack type string
    string[] attackTypeString = { "Projectile Attack 01", "Projectile Attack 02",
                                 "Telekinesis Swing Right", "Telekinesis Swing Left" };

    bool isAimOn = false;

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
        StartCoroutine("StartActionOn");
    }

    IEnumerator StartActionOn()
    {
        yield return new WaitForSeconds(1.0f);
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

        hp = 150.0f;
        maxHp = hp;
        damage = 2.0f;
    }

    private void Update()
    {
        if (isDead) return;
        if(!isAimOn)
        {
            isAimOn = true;
            StartCoroutine("SetAimOn");
        }
        StateSelector();

        curTime += 0.1f;
        curJumpTimer += 0.1f;
    }

    private void StateSelector()
    {
        if (targetList.Count == 0)
        {
            curTarget = null;
            return;
        }
        curTarget = targetList[0].transform;

        if (curJumpTimer >= jumpTimer && targetList.Count > 1 && !isJumping)
        {
            isJumping = true;
            animator.SetTrigger("Fly Away");
            curState = STATE.JUMP;
        }

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

    public void LandingReset()
    {
        curJumpTimer = 0.0f;
        isJumping = false;
        gameObject.SetActive(true);

        GameObject temp = Instantiate(LandingParticle);
        temp.transform.position = new Vector3(transform.position.x, transform.position.y + 1.2f,
                                    transform.position.z);
        Destroy(temp, 3.0f);

    }

    IEnumerator SetAimOn()
    {
        yield return new WaitForSeconds(2.0f);
        Debug.Log("aim on");
        aim.SetActive(true);
    }

    public void JumpAnimationHandler()
    {
        aim.SetActive(false);
        StartCoroutine("PlayFlyingEffect");
    }
    IEnumerator PlayFlyingEffect()
    {
        yield return new WaitForSeconds(0.02f);
        GameObject temp = Instantiate(FlyingParticle);
        temp.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f,
                                    transform.position.z);
        Destroy(temp, 3.0f);

    }

    public void JumpAnimationHandler2()
    {
        GameObject curTarget;
        do
        {
            curTarget = targetList[Random.Range(0, targetList.Count)];
        } while (curTarget.tag == "Base");
        transform.position = new Vector3(curTarget.transform.position.x,
                                             transform.position.y,
                                             curTarget.transform.position.z + 3.5f);
        curState = STATE.IDLE;
        PickTheTarget();
        Base.Instance.SetBossOnAgain();
        isAimOn = false;
        gameObject.SetActive(false);
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
