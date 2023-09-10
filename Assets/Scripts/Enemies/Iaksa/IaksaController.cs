using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaksaController : MonoBehaviour
{
    private new IaksaModel model;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    private float latestChangeTime;
    private readonly float changeTime = 3f;
    private float walkSpeed = 2f;
    private float runSpeed = 5f;
    private Vector3 movementDir;
    private Vector3 speedDir;

    private bool hasObjective = false;
    private GameObject objective;

    public IaksaModel Model { get => model; }

    // TO DO: añadir los efectos de buff/debuff cuando salta al lado de otro personaje
    void Start()
    {
        model = new IaksaModel();

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
        if(Time.time - latestChangeTime > changeTime)
        {
            latestChangeTime = Time.time;
            calculateRandomVector(); 
            animator.Play("Armature|Walk");
        }

        rb.velocity = speedDir;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasObjective)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                objective = other.gameObject;
                calculateObjectiveVector(other.transform.position);
                latestChangeTime = Time.time;
                animator.Play("Armature|Run");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasObjective && (objective.gameObject == other.gameObject))
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                objective = null;
                hasObjective = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasObjective && (objective.gameObject == collision.collider.gameObject))
        {
            if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy"))
            {
                animator.Play("Armature|Action");
            }
        }
    }
}
