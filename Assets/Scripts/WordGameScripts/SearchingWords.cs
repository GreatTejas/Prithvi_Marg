using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class SearchingWords : MonoBehaviour
{
    public TMP_Text displayedText;
    public Image crossLine;
    private string _word;
    private static int _totalWords;
    private static int _foundWords;

    void Start()
    {
     

    }

    private void OnEnable()
    {
        GameEvents.OnCorrectWord += CorrectWord;
    }
    private void OnDisable()
    {
        GameEvents.OnCorrectWord -= CorrectWord;
    }
    public void SetWord(string word)
    {
        _word = word;
        displayedText.text = word;

    }
    private void CorrectWord(string word, List<int> squareIndex)
    {
        if (word == _word)
        {
            crossLine.gameObject.SetActive(true);
            GameEvents.IncrementFoundWords();
        }
    }
}
