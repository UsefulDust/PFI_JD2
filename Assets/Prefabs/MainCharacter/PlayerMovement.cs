using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // input system
    PlayerInput playerInput;
    InputAction move;
    InputAction sprint;

    // Movement and animation
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    [SerializeField] float acceleration = 2.0f;
    [SerializeField] float deceleration = 2.0f;
    [SerializeField] float maximumWalkVelocity = 1f;
    [SerializeField] float maximumRunVelocity = 2.0f;


    // Physics
    Rigidbody rb;
    void Awake()
    {
        playerInput = new PlayerInput();
        
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        // activate player input
        move = playerInput.Player.Move;
        move.Enable();
        sprint = playerInput.Player.Sprint;
        sprint.Enable();
    }
    void OnDisable()
    {
        // deactivate player input
        move.Disable();
        sprint.Disable();
    }
    void Update()
    {
        Move();

        Vector3 direction = new Vector3(velocityX, 0, velocityZ);
        if (sprint.ReadValue<float>() > 0.1f && direction.z > 1.1f)
        {
            rb.velocity = new Vector3(direction.x * maximumRunVelocity, 0, direction.z);
        } else
        {
            rb.velocity = direction;
        }
        
    }

    void Move()
    {
        Vector2 movementInput = move.ReadValue<Vector2>();
        
        bool isRunning = sprint.ReadValue<float>() > 0.1f;
        bool fowardPressed = movementInput.y > 0.1f;
        bool backwardPressed = movementInput.y < -0.1f;
        bool rightPressed = movementInput.x > 0.1f;
        bool leftPressed = movementInput.x < -0.1f;
        float currentMaxVelocity = isRunning ? maximumRunVelocity : maximumWalkVelocity;
        // y+ = W
        // y- = S
        // x+ = D
        // x- = A

        // foward
        if (fowardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        } else if (velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        // backward
        if (backwardPressed && velocityZ > -maximumWalkVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        } else if (velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
        // right
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        } else if (velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
        // left
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        } else if (velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        // reset velocity Z
        if (!fowardPressed && !backwardPressed && velocityX != 0.0f && velocityZ > -0.05f && velocityZ < 0.05f)
        {
            velocityZ = 0.0f;
        }
        // reset velocity X
        if (!leftPressed && !rightPressed && velocityX != 0.0f && velocityX > -0.05f && velocityX < 0.05f)
        {
            velocityX = 0.0f;
        }

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);
    }
}
