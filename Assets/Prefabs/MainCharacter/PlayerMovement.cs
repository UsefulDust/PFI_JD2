using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // input system
    PlayerInput playerInput;
    InputAction move;

    Animator animator;

    void Awake()
    {
        playerInput = new PlayerInput();
        animator = GetComponentInChildren<Animator>();
    }
    void OnEnable()
    {
        // activate player input
        move = playerInput.Player.Move;
        move.Enable();
    }
    void OnDisable()
    {
        // deactivate player input
        move.Disable();
    }
    void Update()
    {
        Move();
    }

    void Move()
    {

    }
}
