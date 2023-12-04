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

    private float walkSpeed;

    private float latestChangeTime;
    private readonly float changeTime = 3f;
    private Vector3 movementDir;
    private Vector3 speedDir;
    private float timeBetweenAttack = 2f;
    private float actualTimeBetweenAttack = 0;
    private bool canAttack = true;

    void Start()
    {
        
        raksashaModel = new RaksashaModel();
        this.SetModel(raksashaModel);

        latestChangeTime = 0f;

        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
    }

    void Update()
    {
        if (Time.time - latestChangeTime > changeTime)
        {
            latestChangeTime = Time.time;
            calculateRandomVector();
            animator.Play("Armature|Andar");
        }

        rb.velocity = speedDir;
    }

    void calculateRandomVector()
    {
        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        Debug.Log("WalkSpeed = " + walkSpeed);
        movementDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        transform.rotation = Quaternion.LookRotation(movementDir);
        speedDir = movementDir * walkSpeed;
    }

    void calculateObjectiveVector(Vector3 objectivePos)
    {
        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        Debug.Log("WalkSpeed = " + walkSpeed);
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

    public async Task WaitAnSecods()
    {
       await new WaitForSeconds(5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Si el jugador entra en contacto con "Sphere Collider"
        if (other.CompareTag("Player"))
        {
            //Realiza ataque en area "Aplastar" (Smash)
            animator.Play("Armature|Aplastar");
            PlayerController playerController = other.GetComponent<PlayerController>();
            ApplySkillSmash(playerController); //TODO -> Mejorar, ya que debe hacer daño si esta dentro del area, si sale no deberia hacerle caso
            WaitAnSecods();

            //Moverse hacia el jugador
            transform.LookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z));
            calculateObjectiveVector(other.transform.position);
            latestChangeTime = Time.time;
            animator.Play("Armature|Andar");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si el jugador entra en contacto con "Box Collider"
        if (collision.collider.CompareTag("Player") && canAttack)
        {
            //Realiza un "zarpazo" (Blow)
            animator.Play("Armature|Zarpazo");
            PlayerController playerController = collision.collider.GetComponent<PlayerController>();
            ApplySkillBlow(playerController);
            ResetAttack();

        }
    }
}
