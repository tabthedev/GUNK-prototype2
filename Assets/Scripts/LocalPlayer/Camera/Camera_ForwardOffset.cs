using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_ForwardOffset : MonoBehaviour
{
    public float forwardOffset = 4f;

    private InputAction moveAction;

    private float actualDirectionX = 1;
    private int readDirectionX = 1;


    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    public Vector2 CalculateForwardOffset()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        if (math.abs(moveVector.x) < 0.05)
        {
            //readDirectionX = 0;
        }
        else if (moveVector.x > 0)
        {
            readDirectionX = 1;
        }
        else
        {
            readDirectionX = -1;
        }

        actualDirectionX = math.lerp(actualDirectionX, readDirectionX, 4f * Time.deltaTime);
        return Vector2.right * forwardOffset * actualDirectionX;
    }
}
