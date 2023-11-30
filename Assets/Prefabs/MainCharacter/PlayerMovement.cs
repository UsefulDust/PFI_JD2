using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // input system
    PlayerInput playerInput;
    InputAction move;

    void Awake()
    {
        playerInput = new PlayerInput();
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
}
