using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : SingletonMonoBehaviour<MovePlayer>
{
    private float horizontalMove;
    private float verticalMove;
    private Vector3 playerInput;
    private CharacterController player;

    [SerializeField] private float playerSpeed = 10;

    private Vector3 movePlayer;

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0.0f, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        movePlayer = playerInput * playerSpeed;

        player.transform.position = player.transform.position + movePlayer;

        player.Move(movePlayer * Time.deltaTime);
    }
}
