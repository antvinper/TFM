using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer : MonoBehaviour//SingletonMonoBehaviour<MovePlayer>
{
    private float horizontalMove;
    private float verticalMove;
    private Vector3 playerInput;
    private CharacterController player;
    private Animator anim;
    public float gravity = 9.8f;

    bool imAttacking;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float fallVelocity;

    private Vector3 movePlayer;


    void Start()
    {
        player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0.0f, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        anim.SetFloat("isWalking", playerInput.magnitude * playerSpeed);

        movePlayer = playerInput * playerSpeed;

        player.transform.position = player.transform.position + movePlayer;

        SetGravity();

        player.Move(movePlayer * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isAttack", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isAttack", false);
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
}
