using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class MovementMain : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputActionMap playerMovementInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction airdiveAction;
    private InputAction dashAction;

    private CapsuleCollider2D collider;
    private Rigidbody2D rigidBody;

    private int readDirectionX = 0;
    private float actualDirectionX = 0;
    public float moveSpeed = 175f;

    private int remainingDashes;
    public int dashRestoreAmount = 1;
    private float lastDashedTime = 0;
    private float dashCooldownTime = 0.5f;
    private int dashDirection = 1;
    public float dashForce = 7f;


    private Vector2 floorDetectionSize;

    private int remainingJumps;
    public int jumpRestoreAmount = 1;
    private float lastJumpedTime = 0;
    private float restoreDetectionCooldownTime = 0.3f;
    public float jumpForce = 8.5f;

    private bool hitFloor = true;
    private bool canAirdive = false;
    public float airDiveForce = -20f;

    private void Awake()
    {
        playerMovementInput = inputActions.FindActionMap("PlayerMovement");

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        airdiveAction = InputSystem.actions.FindAction("AirDive");
        dashAction = InputSystem.actions.FindAction("Dash");

        collider = GetComponent<CapsuleCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();

        LayerMask layer_nothing = LayerMask.GetMask();

        collider.callbackLayers = layer_nothing;


        remainingDashes = dashRestoreAmount;
        remainingJumps = jumpRestoreAmount;
        

        floorDetectionSize = new Vector2(collider.size.x * 0.8f, collider.size.x * 0.25f);
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



        RaycastHit2D floorHit = Physics2D.BoxCast(rigidBody.position, floorDetectionSize, 0f, Vector2.down, ((collider.size.y - floorDetectionSize.y) * 0.5f + 0.02f), LayerMask.GetMask("Map"));
        //print(floorCastOrigin);
        //print(floorDetectionSize);
        //print(Vector2.down * floorDetectionSize.y);

        

        if (floorHit && floorHit.collider != null && Time.time - lastJumpedTime >= restoreDetectionCooldownTime)
        {
            print(floorHit.collider);
            
            hitFloor = true;

            remainingDashes = dashRestoreAmount;
            remainingJumps = jumpRestoreAmount;
        } else if (hitFloor)
        {
            print('b');
            hitFloor = false;
            canAirdive = true;
        }

        // dash
        if (remainingDashes > 0 && dashAction.WasPressedThisFrame() && Time.time - lastDashedTime >= dashCooldownTime)
        {
            remainingDashes -= 1;
            Dash();

            lastDashedTime = dashCooldownTime;
        }

        // jump
        if (remainingJumps > 0 && jumpAction.WasPressedThisFrame())
        {
            remainingJumps -= 1;
            Jump();

            lastJumpedTime = Time.time;

            hitFloor = false;
            canAirdive = true;
        }

        // airdive
        if (canAirdive && airdiveAction.WasPressedThisFrame())
        {
            hitFloor = false;
            canAirdive = false;
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
        print("jump");
        print(remainingJumps);
        rigidBody.linearVelocityY = jumpForce;
    }

    private void AirDive()
    {
        print("dive");
        print(canAirdive);
        rigidBody.linearVelocityY = airDiveForce;
    }

    private void Dash()
    {
        print("dash");
        print(remainingDashes);
        print(dashDirection);
        actualDirectionX = dashDirection * dashForce;
    }
}
