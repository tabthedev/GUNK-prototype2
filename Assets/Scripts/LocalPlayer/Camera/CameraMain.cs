using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public Camera camera;
    public GameObject player;

    private Camera_ForwardOffset ForwardOffset;

    public string cameraMode = "FollowPlayer"; // FollowPlayer / FollowPlayerXOnly / FollowPlayerYOnly / FixedPosition
    public float cameraXAlphaSpeed = 2;
    public float cameraYAlphaSpeed = .5f;
    public Vector2 cameraFixedPosition = Vector2.zero;
    public Vector2 cameraOffset = Vector2.up * 2;

    

    public float cameraProjectionSize = 5;
    public float cameraProjectionSizeAlphaSpeed = 5;

    private Vector2 cameraTarget;

    public Vector3 currentPosition; // 외부에서 읽는 용
    private void Awake()
    {
        camera.transform.position = player.transform.position;
        camera.orthographicSize = cameraProjectionSize;
        cameraTarget = player.transform.position;

        ForwardOffset = GetComponent<Camera_ForwardOffset>();
        Movement_Death.OnDeath += ForwardOffset.ResetDirection;
    }
    
    private void Update()
    {
        camera.orthographicSize = math.lerp(camera.orthographicSize, cameraProjectionSize, math.clamp(cameraProjectionSizeAlphaSpeed * Time.deltaTime, 0, 1));

        if (cameraMode == "FollowPlayer")
        {
            cameraTarget = player.transform.position;
        }
        else if (cameraMode == "FollowPlayerXOnly")
        {
            cameraTarget = new Vector2(player.transform.position.x, cameraFixedPosition.y);
        }
        else if (cameraMode == "FollowPlayerYOnly")
        {
            cameraTarget = new Vector2(cameraFixedPosition.x, player.transform.position.y);
        }
        else if (cameraMode == "FixedPosition")
        {
            cameraTarget = cameraFixedPosition;
        }

        Vector2 actualCameraOffset = cameraMode == "FixedPosition" ? Vector2.zero : cameraOffset + ForwardOffset.CalculateForwardOffset();

        currentPosition = new Vector3(
            math.lerp(camera.transform.position.x, cameraTarget.x + actualCameraOffset.x, math.clamp(cameraXAlphaSpeed * Time.deltaTime, 0, 1)),
            math.lerp(camera.transform.position.y, cameraTarget.y + actualCameraOffset.y, math.clamp(cameraYAlphaSpeed * Time.deltaTime, 0, 1)),
            -10
        );

        camera.transform.position = currentPosition;
    }
}