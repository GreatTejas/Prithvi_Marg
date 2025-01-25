using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float gameTime ;           // Total time for the game
    public float spawnStopTime ;      // Time at which trash stops spawning
    public TMP_Text timerText;            // UI Text to show the countdown
    public GameObject trashSpawner;       // Reference to the trash spawner
    public GameObject winPopup;           // Win screen popup
    public GameObject losePopup;          // Lose screen popup
    private bool gameEnded = false;

    void Start()
    {
        winPopup.SetActive(false);
        losePopup.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(gameTime).ToString();

            // Stop spawning new trash at 45 seconds
            if (gameTime <= spawnStopTime && trashSpawner.activeSelf)
            {
                trashSpawner.SetActive(false);
                Debug.Log("Trash spawner component disabled after 45 seconds.");
            }
        }
        else
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game ended. Checking win/lose condition...");

        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");

        if (trashObjects.Length == 0)
        {
            Debug.Log("You Win! All trash collected.");
            winPopup.SetActive(true); 
        }
        else
        {
            Debug.Log("You Lose! Trash remains.");
            losePopup.SetActive(true);
        }

        Time.timeScale = 0;
    }
}
