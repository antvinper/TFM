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
    //private CharacterController player;
    //private Animator anim;
    public float gravity = 9.8f;
    public float targetRotation = 9.8f;
    public float RotationSmoothTime = 0.12f;
    public float player_health = 100f;

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

    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator anim;

    private Vector3 movePlayer;

    private bool areControlActives = false;

    internal CharacterController CharacterController { get => characterController; }

    internal void SetInputControls(bool isActive)
    {
        areControlActives = isActive;
    }

    internal void ActivateControls()
    {
        characterController.enabled = true;
        anim.enabled = true;
    }

    internal void DeActivateControls()
    {
        characterController.enabled = false;
        anim.enabled = false;
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

        if (!isDashing && canMove && areControlActives)
        {
            SetPlayerInputConverted();
        }
        
        movePlayer = playerInput * playerSpeed;


        anim.SetFloat("isWalking", playerInput.magnitude);

        if (canMove && !isDashing)
        {
            SetGravity();
        }
        

        if (isDashing)
        {
            movePlayer = transform.forward * playerSpeed;
            characterController.Move(movePlayer * dashingPower * Time.deltaTime);
        }
        else
        {
            characterController.Move(movePlayer * Time.deltaTime);
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
        Debug.Log("#GRAVITY -> isGrounded: " + characterController.isGrounded);
        if (characterController.isGrounded)
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
