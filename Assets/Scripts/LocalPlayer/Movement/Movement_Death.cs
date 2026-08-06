using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement_Death : MonoBehaviour
{
    private MovementMain movementMain;

    private Rigidbody2D rigidBody;

    public bool isAlive = true;
    public float fallDeathHeight = -10f;
    public bool ResetStageOnDeath = false;
    private Vector2 lastCheckpoint = Vector2.zero;


    public void SetCheckpoint(Vector2 position)
    {
        lastCheckpoint = position;
    }




    private void Awake()
    {
        movementMain = GetComponent<MovementMain>();

        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        DeathCalculation();
    }


    private void DeathCalculation()
    {
        if (IsFallingDeath())
        {
            FetchFallDeath();
        }
    }

    private void FetchPreDeath()
    {
        isAlive = false;
    }
    private void FetchDeath()
    {
        if (isAlive)
        {
            FetchPreDeath();
        }

        if (ResetStageOnDeath)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            movementMain.ResetMovement();
            movementMain.MoveBody(lastCheckpoint);
        }
    }



    // 나중에 도움이 되지 않을까...?
    private bool IsFallingDeath()
    {
        return rigidBody.position.y < fallDeathHeight;
    }
    private void FetchFallDeath()
    {
        FetchPreDeath();
        FetchDeath();
    }
}