using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using CompanyStats;
using UnityEngine;
using System.Threading.Tasks;

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
    private bool canMove;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 2f;
    private float dashingTime = 0.35f;
    private float dashingCoolDown = 1f;

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
        canMove = true;
    }

    private void FixedUpdate()
    {
        playerSpeed = playerController.GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        anim.speed = playerSpeed * 0.1f;
    }

    public void StopMovement()
    {
        canMove = false;
    }
    public void ContinueMovement()
    {
        canMove = true;
    }

    void Update()
    {
        playerInput = Vector3.zero;
        if (!isDashing && canMove)
        {
            SetPlayerInputConverted();
        }
        
        movePlayer = playerInput * playerSpeed;

        //player.transform.position = player.transform.position + movePlayer;

        anim.SetFloat("isWalking", playerInput.magnitude);

        SetGravity();

        if (isDashing)
        {
            //Debug.Log("#DASH forward: " + player.transform.forward);
            movePlayer = player.transform.forward * playerSpeed;
            player.Move(movePlayer * dashingPower * Time.deltaTime);
        }
        else
        {
            player.Move(movePlayer * Time.deltaTime);
        }

        //Rotacion del personaje segun hacia donde mira
        if (movePlayer != Vector3.zero && playerInput != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(playerInput, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        }
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

    public async Task DoDash(float dashingTime, float dashingPower)
    {
        Debug.Log("#DASH Doing dash");
        canDash = false;
        isDashing = true;
        GetComponent<PlayerController>().SetDashing(isDashing);
        this.dashingPower = dashingPower;

        await new WaitForSeconds(dashingTime);
        isDashing = false;
        GetComponent<PlayerController>().SetDashing(isDashing);
        await new WaitForSeconds(1.0f);
        canDash = true;
    }

    private Vector3 SetPlayerInputConverted()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0.0f, verticalMove);
        
        
        return Vector3.ClampMagnitude(playerInput, 1);
    }

}
