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
    private bool inAlert;
   
 

    // TO DO: anyadir los efectos de buff/debuff cuando salta al lado de otro personaje
    //public float max_health;
    //public float cur_health = 0f;
    void Start()
    {
        model = new IaksaModel();
        this.SetModel(model);

        latestChangeTime = 0f;

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
        inAlert = Physics.CheckSphere(transform.position, alertRange, maskPlayer);

        if (inAlert == true)
        {
            //calculateObjectiveVector(player.transform.position);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), runSpeed * Time.deltaTime);
            //latestChangeTime = Time.time;
            animator.Play("Armature|Run");
        }

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

    private void OnDrawGizmos()
    {
        //Dibujamos una esfera que indique el rango en el que se detecta al jugador
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }

    IEnumerator TimeToRun()
    {
        yield return new WaitForSeconds(20f);
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
        animator.Play("Armature|Action");
    }
}
