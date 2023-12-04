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
        vetalaModel = new VetalaModel();
        this.SetModel(vetalaModel);

        latestChangeTime = 0f;

        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
    }

    void Update()
    {
        if (Time.time - latestChangeTime > changeTime)
        {
            latestChangeTime = Time.time;
            calculateRandomVector();
            animator.Play("Armature|Walk");
        }

        rb.velocity = speedDir;
    }

    void calculateRandomVector()
    {
        walkSpeed = GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        movementDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        transform.rotation = Quaternion.LookRotation(movementDir);
        speedDir = movementDir * walkSpeed;
    }
}
