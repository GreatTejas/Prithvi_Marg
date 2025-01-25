using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public static class GameEvents 
{
    public delegate void EnableSquareSelection();
    public static event EnableSquareSelection OnEnableSquareSelection;
    public static void EnableSquareSelectionMethod()
    {
        if (OnEnableSquareSelection != null)
        {
            OnEnableSquareSelection();
        }
    }

    public delegate void DisableSquareSelection();
    public static event DisableSquareSelection OnDisableSquareSelection;
    public static void DisableSquareSelectionMethod()
    {
        if (OnDisableSquareSelection != null)
        {
            OnDisableSquareSelection();
        }
    }
    public delegate void SelectSquare(Vector3 position);
    public static event SelectSquare OnSelectSquare;
    public static void SelectSquareMethod(Vector3 position)
    {
        if (OnSelectSquare != null)
        {
            OnSelectSquare(position);
        }
    }


    public delegate void CheckSquare(string letter , Vector3 position , int squareIndex);
    public static event CheckSquare OnCheckSquare;
    public static void CheckSquareMethod(string letter, Vector3 position, int squareIndex)
    {
        if (OnCheckSquare != null)
        {
            OnCheckSquare(letter,position,squareIndex);
        }
    }

    public delegate void ClearSelection();
    public static event ClearSelection OnClearSelection;
    public static void ClearSelectionMethod()
    {
        if (OnClearSelection != null)
        {
            OnClearSelection();
        }
    }
    public delegate void CorrectWord(string word , List<int> sqaueIndex);
    public static event CorrectWord OnCorrectWord;
    public static void CorrectWordMethod(string word, List<int> sqaueIndex)
    {
        if(OnCorrectWord != null)
        {
            OnCorrectWord(word , sqaueIndex);
        }
    }
    public delegate void BoardCompleted();
    public static event BoardCompleted OnBoardCompleted;
    public static void BoardCompletedMethod()
    {
        if(OnBoardCompleted != null)
        {
            OnBoardCompleted();
        }
    }
    public delegate void GameOver();
    public static event GameOver OnGameOver;
    public static void GameOverMethod()
    {
        if (OnGameOver != null)
        {
           
            OnGameOver();
        }
    }
    public static int TotalWords = 0;
    public static int WordsFound = 0;

    public static void SetTotalWords(int total)
    {
        TotalWords = total;
    }

    public static void IncrementFoundWords()
    {
        WordsFound++;
        if (WordsFound >= TotalWords)
        {
            BoardCompletedMethod();
        }
    }


}
