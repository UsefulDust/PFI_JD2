using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Camera cam;
    // input system
    PlayerInput playerInput;
    InputAction move;
    InputAction sprint;
    InputAction jump;
    InputAction camMovement;

    // Movement and animation
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    [SerializeField] float acceleration = 2.0f;
    [SerializeField] float deceleration = 2.0f;
    [SerializeField] float maximumWalkVelocity = 1f;
    [SerializeField] float maximumRunVelocity = 2.0f;
    [SerializeField] float speed = 1.5f;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float gravity = 15;
    [SerializeField] float mouseMutiplier = 6;

    Vector3 jumpMovement = Vector3.zero;
    Vector3 cameraRotation = Vector3.zero;

    // Physics
    CharacterController cc;
    void Awake()
    {
        playerInput = new PlayerInput();

        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        cc = GetComponent<CharacterController>();
        cam = Camera.main;
    }
    void OnEnable()
    {
        // activate player input
        move = playerInput.Player.Move;
        move.Enable();
        sprint = playerInput.Player.Sprint;
        sprint.Enable();
        jump = playerInput.Player.Jump;
        jump.Enable();
        camMovement = playerInput.Player.Look;
        camMovement.Enable();
    }
    void OnDisable()
    {
        // deactivate player input
        move.Disable();
        sprint.Disable();
        jump.Disable();
        camMovement.Disable();
    }
    void Update()
    {
        Move();
        Jump();
        CameraRotation();
        Vector3 direction = new Vector3(velocityX, 0, velocityZ);
        if (sprint.ReadValue<float>() > 0.1f && direction.z > 1.1f)
        {
            cc.Move(new Vector3(direction.x * maximumRunVelocity, 0, direction.z) * Time.deltaTime * speed);
        } else
        {
            cc.Move((direction * speed + jumpMovement) * Time.deltaTime);
        }
        
    }
    void Jump()
    {
        if (cc.isGrounded)
        {
            animator.SetBool("IsGrounded", true);
            if (jump.ReadValue<float>() > 0.1f)
            {
                jumpMovement = Vector3.up * jumpForce;
                animator.SetBool("Jump", true);
                
            } else
            {
                animator.SetBool("Jump", false);
            }
            jumpMovement.y = Mathf.Max(-0.5f, jumpMovement.y);
        }
        else
        {
            jumpMovement -= Vector3.up * Time.deltaTime * gravity;
            animator.SetBool("IsGrounded", false);
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
    void CameraRotation()
    {
        Vector2 action = camMovement.ReadValue<Vector2>();
        cameraRotation += new Vector3(-action.y * mouseMutiplier * Time.deltaTime, action.x * mouseMutiplier * Time.deltaTime, 0);


        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -60, 60);
        transform.rotation = Quaternion.Euler(new Vector3(0, cameraRotation.y, 0));
        cam.transform.rotation = Quaternion.Euler(cameraRotation);
    }
}
