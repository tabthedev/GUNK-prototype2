using TMPro;
using UnityEngine;

public class Transition_Finish : MonoBehaviour
{
    public string winMessage = "You won!";

    public GameObject finishMessageFrame;
    public GameObject finishMessageContextLabel;

    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject != player) return;

        FinishLevel();

    }





    public void FinishLevel()
    {
        finishMessageFrame.SetActive(true);
    }
    public void FinishLevel(string message)
    {
        finishMessageContextLabel.GetComponent<TextMeshPro>().text = message;
        finishMessageFrame.SetActive(true);
    }
}
