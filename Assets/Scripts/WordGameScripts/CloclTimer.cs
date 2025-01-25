using UnityEngine;
using UnityEngine.UI;
using static GameEvents;
using TMPro;
using System;
public class CloclTimer : MonoBehaviour
{
    public GameData currentGameData;
    public TMP_Text timetext;
    public GameObject WordGrid;
    public GameObject GameoverPanel;
    public GameObject WellDonePanel;
    private float timeLeft;
    private float _minutes;
    private float _seconds;
    private float _oneSecondDown;
    private bool _timeOut;
    private bool _stopTimer;
    void Start()
    {
        _stopTimer = false;
        _timeOut = false;
        timeLeft = currentGameData.selectedboardData.timeInSeconds;
        _oneSecondDown = timeLeft - 1f;
        GameEvents.OnBoardCompleted += StopTimer;
        GameoverPanel.SetActive(false);
        WellDonePanel.SetActive(false);
        GameEvents.OnBoardCompleted += ShowWellDonePanel;

    }
    private void Update()
    {
        if (_stopTimer == false)
        {
            timeLeft -= Time.deltaTime;
        }

        if (timeLeft <= _oneSecondDown)
        {
            _oneSecondDown = timeLeft - 1f;
        }
        if (timeLeft <= 0 && !_timeOut)
        {
            _stopTimer = true;
            ActivateGameOverGUI();
        }
    }

    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= StopTimer;
        GameEvents.OnBoardCompleted -= ShowWellDonePanel;

    }
    public void StopTimer()
    {
        _stopTimer = true;
    }

    void OnGUI()
    {
        if (_timeOut == false)
        {
            if (timeLeft > 0)
            {
                int _minutes = Mathf.FloorToInt(timeLeft / 60);
                int _seconds = Mathf.RoundToInt(timeLeft % 60);

                timetext.text = _minutes.ToString("00") + ":" + _seconds.ToString("00");
            }
            else
            {
                _stopTimer = true;
                ActivateGameOverGUI();
            }
        }
    }

    private void ActivateGameOverGUI()
    {
        if (GameEvents.WordsFound >= GameEvents.TotalWords)
        {
            ShowWellDonePanel();
            return;
        }
        else {
            GameEvents.GameOverMethod();
            WordGrid.SetActive(false);
            _timeOut = true;
            WellDonePanel.SetActive(false);
            GameoverPanel.SetActive(_timeOut);
        }

    }

    private void ShowWellDonePanel()
    {

        WordGrid.SetActive(false); // Hide the word grid.
        GameoverPanel.SetActive(false); // Ensure the Game Over panel is hidden.
        WellDonePanel.SetActive(true);
    }



}
