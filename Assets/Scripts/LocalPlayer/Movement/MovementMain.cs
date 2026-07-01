using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class MovementMain : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputActionMap playerMovementInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction airdiveAction;
    private InputAction dashAction;

    private BoxCollider2D collider;
    private Rigidbody2D rigidBody;

    private int readDirectionX = 0;
    private float actualDirectionX = 0;
    public float moveSpeed = 175f;

    private int remainingDashes = 1;
    private int dashDirection = 1;
    public float dashForce = 7f;

    private int remainingJumps = 1;
    public float jumpForce = 8.5f;

    private bool canAirdive = false;
    public float airDiveForce = -20f;

    private void Awake()
    {
        playerMovementInput = inputActions.FindActionMap("PlayerMovement");

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        airdiveAction = InputSystem.actions.FindAction("AirDive");
        dashAction = InputSystem.actions.FindAction("Dash");

        collider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerMovementInput.Enable();
    }
    private void OnDisable()
    {
        playerMovementInput.Disable();
    }


    // Update is called once per frame
    private void Update()
    {
        // move
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        if (math.abs(moveVector.x) < 0.05)
        {
            readDirectionX = 0;
        } else if (moveVector.x > 0)
        {
            readDirectionX = 1;
            dashDirection = 1;
        } else
        {
            readDirectionX = -1;
            dashDirection = -1;
        }



        // dash
        if (remainingDashes > 0 && dashAction.WasPressedThisFrame())
        {
            Dash();
        }

        // jump
        if (remainingJumps > 0 && jumpAction.WasPressedThisFrame())
        {
            Jump();
        }

        // airdive
        if (canAirdive && airdiveAction.WasPressedThisFrame())
        {
            AirDive();
        }
    }



    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        actualDirectionX = math.lerp(actualDirectionX, readDirectionX, 5f * Time.deltaTime);

        rigidBody.linearVelocityX = actualDirectionX * moveSpeed * Time.deltaTime * 100;
        //print(actualDirectionX);
        //print(rigidBody.linearVelocityX);
        //print(rigidBody.linearVelocityY);
        //print("------------------------------");
    }

    private void Jump()
    {
        //print("jump");
        rigidBody.linearVelocityY = jumpForce;
    }

    private void AirDive()
    {
        rigidBody.linearVelocityY = airDiveForce;
    }

    private void Dash()
    {
        print(dashDirection);
        actualDirectionX = dashDirection * dashForce;
    }
}
