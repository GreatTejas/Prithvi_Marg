using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int totalPuzzlePieces = 16;
    private int placedPieces = 0;

    void Start()
    {
        
        UpdateScoreDisplay();
    }

    public void OnPuzzlePiecePlaced()
    {
        placedPieces = placedPieces + 1;
        // Debug logging
        Debug.Log($"Piece Placed: {placedPieces}/{totalPuzzlePieces}");
        UpdateScoreDisplay();
    }


    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            int score = placedPieces * 5;
            scoreText.text = $"SCORE : {score} / 160";
        }
    }
}