using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class VetalaController : EnemyController
{
    private VetalaModel vetalaModel;
    public VetalaModel VetalaModel { get => vetalaModel; }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] SkillDefinition skillPoison;

    private float walkSpeed;

    private Vector3 movementDir;
    private Vector3 speedDir;
    private float timeBetweenAttack = 2f;
    private float actualTimeBetweenAttack = 0;

    void Start()
    {

        vetalaModel = new VetalaModel();
        this.SetModel(vetalaModel);

        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
    }

    void Update()
    {
        rb.velocity = speedDir;
    }

    void calculateObjectiveVector(Vector3 objectivePos)
    {
        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        movementDir = new Vector3(objectivePos.x - transform.position.x, 0, objectivePos.z - transform.position.z).normalized;
        transform.rotation = Quaternion.LookRotation(movementDir);
        speedDir = movementDir * walkSpeed;
    }

    public async Task ApplySkill(CompanyCharacterController target)
    {
        skillPoison.ProcessSkill(this, target);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Si el jugador entra en contacto con "Sphere Collider"
        if (other.CompareTag("Player"))
        {
            //Realiza ataque en area "Aplastar" (Smash)
            animator.Play("Armature|Attack");

            //Moverse hacia el jugador
            transform.LookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z));
            calculateObjectiveVector(other.transform.position);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si el jugador entra en contacto con "Box Collider"
        if (collision.collider.CompareTag("Player"))
        {
            PlayerController playerController = collision.collider.GetComponent<PlayerController>();
            ApplySkill(playerController);
        }
        /*
        if (collision.collider.CompareTag("Wall"))
        {

        }*/
    }
}
