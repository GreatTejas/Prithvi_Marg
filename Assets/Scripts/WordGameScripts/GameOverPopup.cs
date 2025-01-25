using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class GameOverPopup : MonoBehaviour
{
    public GameObject gameOverPopup;
    public GameObject Replay;
    public BoardData Words;

    void Start()
    {
        Replay.GetComponent<Button>().interactable = false;
        gameOverPopup.SetActive(false);
        GameEvents.OnGameOver += ShowGameOverPopup;
    }
    private void OnDisable()
    {
        GameEvents.OnGameOver -= ShowGameOverPopup;
    }
    void Update()
    {
        
    }


    private void ShowGameOverPopup()
    {
       gameOverPopup.SetActive(true);
        Replay.GetComponent <Button>().interactable = false;

    }
}
