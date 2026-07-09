using Unity.Mathematics;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public Camera camera;
    public GameObject player;

    public string cameraMode = "FollowPlayer"; // FollowPlayer / FollowPlayerXOnly / FollowPlayerYOnly / FixedPosition
    public float alphaSpeed = 2;
    public Vector2 cameraFixedPosition = Vector2.zero;
    public Vector2 cameraOffset = Vector2.up * 2;

    private Vector2 cameraTarget;
    private void Awake()
    {
        camera.transform.position = player.transform.position;
        cameraTarget = player.transform.position;
    }
    
    private void Update()
    {
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

        camera.transform.position = Vector2.Lerp(camera.transform.position, cameraTarget + cameraOffset, math.clamp(alphaSpeed * Time.deltaTime, 0, 1));
    }
}
