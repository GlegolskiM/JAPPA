using Unity.VisualScripting;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public static GameStates instance;

    public GameObject youWinText;
    public GameObject gameOverText;

    
    public bool gameEnded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public void gameOver()
    {
        gameOverText.SetActive(true);
        SoundController.instance.GameOver();
        Time.timeScale = true ? 0f : 1f; // Freeze or resume time
    }

    public void youWin()
    {


        youWinText.SetActive(true);
        Time.timeScale = true ? 0f : 1f; // Freeze or resume time
    }
}
