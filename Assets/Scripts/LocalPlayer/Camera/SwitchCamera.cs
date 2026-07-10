using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    private Collider2D collider;
    private GameObject player;
    private Camera camera;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindAnyObjectByType<Camera>();

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
        print(collision.otherCollider.gameObject);
        print(collision.gameObject);
        if (collision.otherCollider.gameObject == player)
        {
            print("a");
        }
    }
}
