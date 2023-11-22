using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaksaController : EnemyController
{
    //private new IaksaModel model;
    //public IaksaModel Model { get => model; }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    private float latestChangeTime;
    private readonly float changeTime = 3f;
    private float walkSpeed = 2f;
    private float runSpeed = 5f;
    private Vector3 movementDir;
    private Vector3 speedDir;

    [SerializeField] private float alertRange;
    [SerializeField] private LayerMask maskPlayer;
    [SerializeField] private Transform player;

    // TO DO: anyadir los efectos de buff/debuff cuando salta al lado de otro personaje
    //public float max_health;
    //public float cur_health = 0f;
    void Start()
    {
        model = new IaksaModel();
        this.SetModel(model);

        latestChangeTime = 0f;

        calculateRandomVector();
    }

    void calculateRandomVector()
    {
        movementDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        transform.rotation = Quaternion.LookRotation(movementDir);
        speedDir = movementDir * walkSpeed;
    }

    void calculateObjectiveVector(Vector3 objectivePos)
    {
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

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

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
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy"))
        {
            animator.Play("Armature|Action");
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
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy"))
        {
            animator.Play("Armature|Action");
        }
    }
}
