using UnityEngine;
using UnityEngine.SceneManagement; // For reloading scenes
using UnityEngine.UI; // For UI elements
using TMPro;
using System.Threading;
using System.IO.Compression; // For TextMeshPro UI elements
public class Gm : MonoBehaviour
{
    public GameObject winCanvas;  // Reference to the WinCanvas
    public GameObject lossCanvas;
   public TextMeshProUGUI timerText;        // Reference to the Timer UI Text
    public TextMeshProUGUI resultText;       // Reference to the result text on the WinCanvas
    public Button winButton;      // Reference to the button on the WinCanvas
    public float gameDuration = 30f; // Total game duration in seconds
    private int totalObjects;     // Total objects in the scene
    private int objectsInBins;    // Number of objects placed in the correct bins
    private float timeRemaining;  // Time remaining in the game
    private bool gameActive = true;
    private bool over=false;
    public Button button;      // Reference to the button on the WinCanvas

    private void Start()
    {
        // Find all objects with the tags "Dry" and "Wet"
        totalObjects = 23;
        objectsInBins = 0;
        timeRemaining = gameDuration;
        // Ensure the WinCanvas is hidden
        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
            lossCanvas.SetActive(false);
        }
        Debug.Log($"Total objects to place: {totalObjects}");
        
    }

    private void Update()
    {
        
        Debug.Log(gameActive+"in update ");
    if (!gameActive||over){ return;
         // Exit early if the game is not active
        }
        Debug.Log("over is"+over);

    // Decrease timer and ensure it doesn't go below zero
    timeRemaining = Mathf.Max(0, timeRemaining - Time.deltaTime);

    UpdateTimerUI();

    if (timeRemaining == 0&&!over)
    {
        over=true;
       /*lossCanvas.SetActive(true);
       winCanvas.SetActive(false);*/
       if (winCanvas.activeSelf)
{
         lossCanvas.SetActive(false);
}

else
{
     
        
          lossCanvas.SetActive(true);
          button.onClick.AddListener(() =>
            {
                Debug.Log("activated");
                    SceneManager.LoadScene("SampleScene");

                });

}
        }
    }
    
    

    private void UpdateTimerUI()
    {
        if (timerText != null)
    {
        timerText.text = $"Time: {timeRemaining:F1}s";
    }
    else
    {
        Debug.LogError("Timer Text is not assigned in the Inspector!");
    }
    }

    public void ObjectPlaced()
    {
        Debug.Log(gameActive+"in objectplaced ");
        if (!gameActive) {
            return;
        }

        objectsInBins++;
      Debug.Log("Objects in bins"+ objectsInBins);
        // Check if all objects are placed correctly
        if ((objectsInBins == totalObjects)&&!over)
        {
            over=true;
            winCanvas.SetActive(true);
            lossCanvas.SetActive(false);
             winButton.onClick.AddListener(() =>
            {
                
                    SceneManager.LoadScene("SampleScene");
                
                });
            
            //EndGame(true);
        }
    }

    private void EndGame(bool playerWon)
    {        Debug.Log(gameActive+"in endgame ");
                      
                if (!gameActive) 
                {return; // Prevent `EndGame` from running multiple times
                }
        gameActive = false;
       
        Debug.Log("running endgame");

        // Show the WinCanvas and update the result text
        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
            resultText.text = playerWon ? "You Won!" : "You Lost!";
            winButton.GetComponentInChildren<TextMeshProUGUI>().text = playerWon ? "Go Back" : "Try Again";
             winButton.onClick.RemoveAllListeners();
            winButton.onClick.AddListener(() =>
            {
                if (playerWon)
                {
                    SceneManager.LoadScene("MainMenu");
                }
                else
                {
                    SceneManager.LoadScene("SampleScene");
                }
            });
        }
             Debug.Log(playerWon ? "Game Over - You Won!" : "Game Over - You Lost!");
    }
}
