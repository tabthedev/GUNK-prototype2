using UnityEngine;

public class Movement_Death_Killer : MonoBehaviour
{
    private GameObject player;
    private Movement_Death Death;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Death = player.GetComponent<Movement_Death>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject == player)
        {
            Death.FetchDeath();
        }
    }
}
