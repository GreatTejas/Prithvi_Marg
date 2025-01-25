using UnityEngine;

using TMPro;
using UnityEngine.UI; // For UI elements


public class drystart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject canvas;
    public Button winButton;      // Reference to the button on the WinCanvas

    public GameObject canvas2;

    void Start()
    {
        // Ensure the canvas is active at the start
        canvas.SetActive(true);
    }
    void Update()
    {
        winButton.onClick.AddListener(() =>
               {
                   canvas.SetActive(false);
                   canvas2.SetActive(true);

               }
        );
    }
    public void OnButtonClick()
    {
        // Hide the canvas when the button is clicked
        canvas.SetActive(false);
    }
}


