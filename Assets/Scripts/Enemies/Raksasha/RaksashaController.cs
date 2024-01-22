using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RaksashaController : EnemyController
{
    private RaksashaModel raksashaModel;
    public RaksashaModel RaksashaModel { get => raksashaModel; }

    [SerializeField] private Rigidbody rb;

    [SerializeField] private Animator animator;
    [SerializeField] SkillDefinition skillSmash;
    [SerializeField] SkillDefinition skillBlow;

    [SerializeField] private float walkSpeed;

    private float timeToChangeDirection = 3.0f;
    private float actualTimeToChangeDirection = 0.0f;

    //private float latestChangeTime;
    //private readonly float changeTime = 3f;
    private Vector3 movementDir;
    private Vector3 speedDir;
    private float timeBetweenAttack = 2f;
    private float actualTimeBetweenAttack = 0;
    private bool canAttack = true;
    private bool isAttacking = false;

    private Vector3 rotateTo;

    private bool isPatrolling = true;
    private bool isCatchingPlayer = false;
    private bool isInAttackRange = false;
    private Quaternion rotation;

    private PlayerController playerController;

    private string[] attackAnims = { "Aplastar", "Zarpazo" };

    void Start()
    {
        raksashaModel = new RaksashaModel();
        this.SetModel(raksashaModel);

        actualTimeToChangeDirection = 0f;

        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
    }

    private void ChangePatrolDirection()
    {
        actualTimeToChangeDirection = 0.0f;
        rotateTo = ChangeDirection(this.playerController);
        rotation = Quaternion.LookRotation(rotateTo);
        animator.Play("Armature|Andar");
    }

    private void CatchPlayer()
    {
        rotateTo = ChangeDirection(this.playerController);
        rotateTo.y = 0.0f;

        Quaternion rotationY = Quaternion.LookRotation(rotateTo);
        rotation.eulerAngles = new Vector3(0, rotationY.eulerAngles.y, 0);
    }

    private void Attack()
    {
        Quaternion rotationY = Quaternion.LookRotation(rotateTo);
        rotation.eulerAngles = new Vector3(0, rotationY.eulerAngles.y + 20, 0);
        int index = Random.Range(0, attackAnims.Length);
        animator.SetTrigger(attackAnims[index]);
        //Debug.Log("#RAKSASHA ANIM: " + attackAnims[index]);
        ResetAttack();
    }

    void Update()
    {
        if (actualTimeToChangeDirection > timeToChangeDirection && isPatrolling && !isCatchingPlayer)
        {
            //Debug.Log("#RAKSASHA UPDATE: Change patrol direction");
            ChangePatrolDirection();
        }
        else if ((isCatchingPlayer || isInAttackRange) && !isAttacking)
        {
            //Debug.Log("#RAKSASHA UPDATE: Catching player");
            CatchPlayer();
        }
        if (isInAttackRange && canAttack)
        {
            //Debug.Log("#RAKSASHA UPDATE: Attack");
            Attack();
        }
        actualTimeToChangeDirection += Time.deltaTime;
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);

        if ((isCatchingPlayer || isPatrolling) && !isAttacking)
        {
            speedDir = transform.forward * walkSpeed;
            rb.velocity = speedDir;
        }
        
    }

    Vector3 ChangeDirection(PlayerController playerController)
    {
        //Debug.Log("#RAKSASHA playerController is: " + playerController);
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        if (playerController != null)
        {
            direction = playerController.transform.position - this.transform.position;

        }
        //Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        return direction.normalized;
    }

    void calculateRandomVector()
    {
        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        //Debug.Log("WalkSpeed = " + walkSpeed);
        movementDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

        Quaternion rotation = Quaternion.LookRotation(movementDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 10);

        speedDir = movementDir * walkSpeed;
    }

    void calculateObjectiveVector(Vector3 objectivePos)
    {
        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        //Debug.Log("WalkSpeed = " + walkSpeed);
        movementDir = new Vector3(objectivePos.x - transform.position.x, 0, objectivePos.z - transform.position.z).normalized;
        transform.rotation = Quaternion.LookRotation(movementDir);
        speedDir = movementDir * walkSpeed;
    }

    public async Task ApplySkillSmash(CompanyCharacterController target)
    {
        skillSmash.ProcessSkill(this, target);
    }

    public async Task ApplySkillBlow(CompanyCharacterController target)
    {
        skillBlow.ProcessSkill(this, target);
    }

    public async Task WaitUntilFinishAttack()
    {
        //Debug.Log("#RAKSASHA UPDATE: Start waiting until finish attack");
        isAttacking = true;
        //Debug.Log("#RAKSASHA UPDATE animation clip: " + animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
        await new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        isAttacking = false;
        //Debug.Log("#RAKSASHA UPDATE: End waiting until finish attack");
    }

    public async Task ResetAttack()
    {
        WaitUntilFinishAttack();
        canAttack = false;
        while (actualTimeBetweenAttack < timeBetweenAttack)
        {
            await new WaitForSeconds(Time.deltaTime);
            actualTimeBetweenAttack += Time.deltaTime;
        }
        actualTimeBetweenAttack = 0.0f;
        canAttack = true;
    }

    public void OnPlayerInPursueRange(bool isInRange, PlayerController playerController)
    {
        if (isInRange)
        {
            this.playerController = playerController;
            Debug.Log("#RAKSASHA Change direction to get player");
            isPatrolling = false;
            isCatchingPlayer = true;
            //rotateTo = ChangeDirection(this.playerController);
        }
        else
        {
            this.playerController = null;
            Debug.Log("#RAKSASHA Change to a random direction");
            isPatrolling = true;
            isCatchingPlayer = false;
            //rotateTo = ChangeDirection(this.playerController);
            actualTimeToChangeDirection = 0;
        }
    }

    internal void OnPlayerInAttackRange(bool isInRange, PlayerController playerController)
    {
        if (isInRange)
        {
            isInAttackRange = true;
            isCatchingPlayer = false;
        }
        else
        {
            isCatchingPlayer = true;
            isInAttackRange = false;
            canAttack = false;
        }
    }
    
}
