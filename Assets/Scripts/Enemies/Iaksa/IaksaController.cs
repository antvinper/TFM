using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class IaksaController : EnemyController
{
    private  IaksaModel iaksaModel;
    public IaksaModel IaksaModel { get => iaksaModel; }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] List<SkillDefinition> skills = new List<SkillDefinition>();


    private float latestChangeTime;
    private readonly float changeTime = 3f;
    private float walkSpeed;
    private float runSpeed; 
    private Vector3 movementDir;
    private Vector3 speedDir;
    private float timeBetweenAttack = 2f;
    private float actualTimeBetweenAttack = 0;
    private bool canAttack = true;

    void Start()
    {
        iaksaModel = new IaksaModel();
        this.SetModel(iaksaModel);


        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        runSpeed = walkSpeed * 2.5f;

        latestChangeTime = 0f;

        calculateRandomVector();
    }

    void calculateRandomVector()
    {
        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        movementDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        transform.rotation = Quaternion.LookRotation(movementDir);
        speedDir = movementDir * walkSpeed;
    }

    void calculateObjectiveVector(Vector3 objectivePos)
    {
        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        runSpeed = walkSpeed * 2.5f;
        movementDir = new Vector3(objectivePos.x - transform.position.x, 0, objectivePos.z - transform.position.z).normalized;
        transform.rotation = Quaternion.LookRotation(movementDir);
        speedDir = movementDir * runSpeed;
    }

    void Update()
    {
        if(Time.time - latestChangeTime > changeTime)
        {
            latestChangeTime = Time.time;
            calculateRandomVector(); 
            animator.Play("Armature|Walk");
        }

        rb.velocity = speedDir;
    }
    public async Task ResetAttack()
    {
        while (actualTimeBetweenAttack < timeBetweenAttack)
        {
            canAttack = false;
            await new WaitForSeconds(Time.deltaTime);
            actualTimeBetweenAttack += Time.deltaTime;
        }

        canAttack = true;
    }


    public async Task ApplySkill(CompanyCharacterController target)
    {
        foreach (SkillDefinition skill in skills)
        {
            skill.ProcessSkill(this, target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Si el jugador/enemigo entra en contacto con "Sphere Collider", ira a por el
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            //Moverse hacia el jugador
            transform.LookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z));
            calculateObjectiveVector(other.transform.position);
            latestChangeTime = Time.time;
            animator.Play("Armature|Run");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si el jugador/enemigo entra en contacto con "Box Collider", activara la animacion "Action" para pegar un saltito y aplicar el buff/debuff
        if ((collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy")) && canAttack)
        {
            animator.Play("Armature|Action");
            PlayerController playerController = collision.collider.GetComponent<PlayerController>();
            ApplySkill(playerController);
            ResetAttack();

        }
    }
}
