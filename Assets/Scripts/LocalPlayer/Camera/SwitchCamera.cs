using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    private GameObject player;
    private Camera camera;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindAnyObjectByType<Camera>();
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
