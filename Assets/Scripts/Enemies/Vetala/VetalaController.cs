using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VetalaController : MonoBehaviour
{
    private new VetalaModel model;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    private bool inRange = false;
    private GameObject target;

    private float flySpeed = 3.5f;
    private float diveSpeed = 5f;
    private Vector3 movementDir;
    private Vector3 speedDir;

    public VetalaModel Model { get => model; }

    // TO DO: aplicar los efectos de daño cuando hace el ataque
    void Start()
    {
        model = new VetalaModel();
        target = GameObject.FindGameObjectWithTag("Player");
        animator.Play("Armature|Idle");
    }
    
    void Update()
    {
        if (!inRange)
        {
            movementDir = new Vector3(target.transform.position.x - transform.position.x, 0, target.transform.position.z - transform.position.z).normalized;
            transform.rotation = Quaternion.LookRotation(movementDir);
            speedDir = movementDir * flySpeed;
        }
        else
        {
            speedDir = movementDir * diveSpeed;
        }

        rb.velocity = speedDir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            animator.Play("Armature|Attack");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            animator.Play("Armature|Idle");
        }
    }
}
