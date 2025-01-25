using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static AlphabetData;
using static GameEvents;

public class GridSquare : MonoBehaviour
{
    public int squareIndex { get; set; }
    private AlphabetData.LetterData _normalLetterdata;
    private AlphabetData.LetterData _selectedLetterdata;
    private AlphabetData.LetterData _correctLetterdata;

    private SpriteRenderer displayImage;

    private bool _selected;
    private bool _clicked;
    private int _index = -1;
    private bool correct;

    public void SetIndex (int index)
    {
        _index = index;

    }
    public int GetIndex()
    {
        return _index;
    }

     void Start()
    {
        displayImage = GetComponent<SpriteRenderer>();
        _selected = false;
        _clicked = false;
        correct = false;

    }

    private void OnEnable()
    {
        GameEvents.OnEnableSquareSelection += OnEnableSquareSelection;
        GameEvents.OnDisableSquareSelection += OnDisableSquareSelection;
        GameEvents.OnSelectSquare += SelectSquare;
        GameEvents.OnCorrectWord += CorrectWord;
       
    }

    private void OnDisable()
    {
        GameEvents.OnEnableSquareSelection -= OnEnableSquareSelection;
        GameEvents.OnDisableSquareSelection -= OnDisableSquareSelection;
        GameEvents.OnSelectSquare -= SelectSquare;
        GameEvents.OnCorrectWord -= CorrectWord;

    }
    private void CorrectWord(string word , List<int> squareIndex) 
    {
        if (_selected && squareIndex.Contains(_index))
        {
            correct = true;
            displayImage.sprite = _correctLetterdata.image;
        }
      
        _selected = false;
        _clicked = false;

    }
   

    public void OnEnableSquareSelection()
    {
        _clicked= true;
        _selected= false;
    }
    public void OnDisableSquareSelection()
    {
        _clicked= false;
        _selected= false;
        if (correct == true)
            displayImage.sprite = _correctLetterdata.image;
        else
            displayImage.sprite = _normalLetterdata.image;
    }

    private void SelectSquare( Vector3 position)
    {
        if(this.gameObject.transform.position== position)
        {
            displayImage.sprite = _selectedLetterdata.image;

        }

    }
    public void SetSprite(AlphabetData.LetterData normalletterdata, AlphabetData.LetterData selectedletterdata,
        AlphabetData.LetterData correctletterdata)
    {
        _normalLetterdata = normalletterdata;
        _selectedLetterdata = selectedletterdata;
        _correctLetterdata = correctletterdata;

        GetComponent<SpriteRenderer>().sprite= _normalLetterdata.image;

    }
    private void OnMouseDown()
    {
        OnDisableSquareSelection();
        GameEvents.EnableSquareSelectionMethod();
        CheckSquare();
        displayImage.sprite = _selectedLetterdata.image;

    }
    private void OnMouseEnter()
    {
        CheckSquare();
    }
    private void OnMouseUp()
    {
        GameEvents.ClearSelectionMethod();
        GameEvents.DisableSquareSelectionMethod();
    }
    public void CheckSquare()
    {
        if(_selected == false && _clicked == true)
        {
            _selected = true;
            GameEvents.CheckSquareMethod(_normalLetterdata.letter,gameObject.transform.position , _index);

        } 
    }
}
