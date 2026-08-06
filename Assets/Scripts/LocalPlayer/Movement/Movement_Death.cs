using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement_Death : MonoBehaviour
{
    public float fallDeathHeight = -10f;
    public bool ResetStageOnDeath = false;

    public void DeathCalculation()
    {
        if (IsFallingDeath())
        {
            FetchDeath("fall");
        }
    }


    private void FetchDeath(string reason)
    {
        if (ResetStageOnDeath)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }



    private bool IsFallingDeath()
    {
        return transform.position.y < fallDeathHeight;
    }
}