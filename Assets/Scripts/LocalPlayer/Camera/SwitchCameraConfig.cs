using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraConfig : MonoBehaviour
{
    private Collider2D collider;
    private GameObject player;
    private Camera camera;
    private GameObject cameraManager;
    private CameraMain cameraMain;

    public string cameraMode = "FollowPlayer";
    public Vector2 cameraAlphaSpeed;
    public Vector2 cameraTargetFixedPosition;
    public Vector2 cameraTargetOffset;
    public float cameraProjectionSize = 5;
    public float cameraProjectionSizeAlphaSpeed = 1;
    public Vector2 actualCameraPosition;

    public bool ignoreCameraModeChange;
    public bool ignoreCameraAlphaSpeed;
    public bool ignoreCameraTargetFixedPosition;
    public bool ignoreCameraTargetOffset;
    public bool ignoreCameraProjectionSize;
    public bool ignoreCameraProjectionSizeAlphaSpeed;
    public bool reflectActualCameraPosition;




    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindAnyObjectByType<Camera>();
        cameraManager = GameObject.FindGameObjectWithTag("CameraManager");
        cameraMain = cameraManager.GetComponent<CameraMain>();

        collider = gameObject.GetComponent<Collider2D>();

        LayerMask layer_nothing = LayerMask.GetMask();
        LayerMask layer_localPlayer = LayerMask.GetMask("LocalPlayer");

        collider.includeLayers = layer_nothing;
        collider.excludeLayers = layer_nothing;
        collider.forceSendLayers = layer_nothing;
        collider.forceReceiveLayers = layer_nothing;
        collider.contactCaptureLayers = layer_localPlayer;
        collider.callbackLayers = layer_localPlayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.gameObject);
        print(collision.gameObject);
        if (collision.collider.gameObject == player)
        {
            print("foo");
            if (!ignoreCameraModeChange)
            {
                cameraMain.cameraMode = cameraMode;
            }
            if (!ignoreCameraAlphaSpeed)
            {
                cameraMain.cameraXAlphaSpeed = cameraAlphaSpeed.x;
                cameraMain.cameraYAlphaSpeed = cameraAlphaSpeed.y;
            }
            if (!ignoreCameraTargetFixedPosition)
            {
                cameraMain.cameraFixedPosition = cameraTargetFixedPosition;
            }
            if (!ignoreCameraTargetOffset)
            {
                cameraMain.cameraOffset = cameraTargetOffset;
            }
            if (!ignoreCameraProjectionSize)
            {
                cameraMain.cameraProjectionSize = cameraProjectionSize;
            }
            if (!ignoreCameraProjectionSizeAlphaSpeed)
            {
                cameraMain.cameraProjectionSizeAlphaSpeed = cameraProjectionSizeAlphaSpeed;
            }
            if (reflectActualCameraPosition)
            {
                actualCameraPosition = camera.transform.position;
            }
        }
    }
}
