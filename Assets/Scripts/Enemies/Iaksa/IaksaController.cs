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
    private float walkSpeed; //TODO -> Usar la velocidad de los stats.
    private float runSpeed; // TODO -> Velocidad de los stats * x
    private Vector3 movementDir;
    private Vector3 speedDir;
    private float timeBetweenAttack = 2f;
    private float actualTimeBetweenAttack = 0;
    private bool canAttack = true;

    [SerializeField] private float alertRange;
    [SerializeField] private LayerMask maskPlayer;
    //[SerializeField] private Transform player;

    // TO DO: anyadir los efectos de buff/debuff cuando salta al lado de otro personaje
    //public float max_health;
    //public float cur_health = 0f;
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
        walkSpeed = 0;
        Debug.Log("WalkSpeed = " + walkSpeed);
        movementDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        transform.rotation = Quaternion.LookRotation(movementDir);
        speedDir = movementDir * walkSpeed;
    }

    void calculateObjectiveVector(Vector3 objectivePos)
    {
        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        walkSpeed = 0;
        runSpeed = walkSpeed * 2.5f;
        Debug.Log("WalkSpeed = " + walkSpeed);
        movementDir = new Vector3(objectivePos.x - transform.position.x, 0, objectivePos.z - transform.position.z).normalized;
        transform.rotation = Quaternion.LookRotation(movementDir);
        speedDir = movementDir * runSpeed;
    }

    void Update()
    {
        if(Time.time - latestChangeTime > changeTime)
        {
            latestChangeTime = Time.time;
            //calculateRandomVector(); 
            animator.Play("Armature|Walk");
        }

        rb.velocity = speedDir;
    }

    /*public void TakeDamage(float amount)
    {
        if (cur_health > 0)
        {
            cur_health -= amount;
        }
        else
        {
            DestroyEnemy();
        }

    }*/
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

    /*public void DestroyEnemy()
    {
        Destroy(gameObject);
    }*/

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            calculateObjectiveVector(other.transform.position);
            latestChangeTime = Time.time;
            animator.Play("Armature|Run");
        }
    }
    */

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
