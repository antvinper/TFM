using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using CompanyStats;
using UnityEngine;

public class MovePlayer : MonoBehaviour//SingletonMonoBehaviour<MovePlayer>
{
    private float horizontalMove;
    private float verticalMove;
    private Vector3 playerInput;
    private CharacterController player;
    private Animator anim;
    public float gravity = 9.8f;
    public float targetRotation = 9.8f;
    public float RotationSmoothTime = 0.12f;
    public float player_health = 100f;
    private GameObject _mainCamera;

    [SerializeField] PlayerController playerController;
    [SerializeField] private float playerSpeed;

    [SerializeField] private float fallVelocity;
    [SerializeField] private float rotationSpeed;

    private Vector3 movePlayer;
    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    void Start()
    {
        player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        playerSpeed = playerController.GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0.0f, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        movePlayer = playerInput * playerSpeed;

        player.transform.position = player.transform.position + movePlayer;

        anim.SetFloat("isWalking", playerInput.magnitude * playerSpeed);

        SetGravity();

        player.Move(movePlayer * Time.deltaTime);

        //Rotacion del personaje segun hacia donde mira
        if (movePlayer != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(playerInput, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        }

        /*
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isAttack", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isAttack", false);
        }
        */
    }

    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }

    public void TakeDamage(float amount)
    {
        player_health = Math.Max(player_health - amount, 0);
    }

}
