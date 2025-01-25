using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TMP_Text scoreText;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}
