using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        playerMovement.ListenMousePosition();
        playerMovement.ListenMoveInput();
        playerMovement.ListenDashInput();
        playerMovement.ListenJumpInput();
    }

    void FixedUpdate()
    {
        playerMovement.Move();
        playerMovement.StartDash();
        playerMovement.Jump();
    }
}
