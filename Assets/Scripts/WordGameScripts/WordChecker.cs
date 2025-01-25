using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameEvents;

public class WordChecker : MonoBehaviour
{
    public GameData currentGameData;
    private string _word;
    private int _assignedpoints = 0;
    private int _completedWords = 0;
    private Ray _rayUp, _rayDown;
    private Ray _rayLeft, _rayRight;
    private Ray _rayDiagonalLeftUp, _rayDiagonalLeftDown;
    private Ray _rayDiagonalRightUp, _rayDiagonalRightDown;
    private Vector3 _rayStartPosition;
    private Ray _currentRay = new Ray();
    private List<int> _correctSquareList = new List<int>(); 

    
    private void OnEnable()
    {
        GameEvents.OnCheckSquare += SquareSelected;
        GameEvents.OnClearSelection += ClearSelection; 
    }
    private void OnDisable()
    {
        GameEvents.OnCheckSquare -= SquareSelected;
        GameEvents.OnClearSelection -= ClearSelection;
    }
    private void Start()
    {
        _assignedpoints = 0;
        _completedWords = 0;
    }
    private void Update()
    {
        if (_assignedpoints > 0 && Application.isEditor)
        {
            Debug.DrawRay(_rayUp.origin,_rayUp.direction * 4);
            Debug.DrawRay(_rayDown.origin,_rayDown.direction*4);
            Debug.DrawRay(_rayLeft.origin, _rayLeft.direction*4);
            Debug.DrawRay(_rayRight.origin, _rayRight.direction*4);
            Debug.DrawRay(_rayDiagonalLeftUp.origin, _rayDiagonalLeftUp.direction*4);
            Debug.DrawRay(_rayDiagonalLeftDown.origin, _rayDiagonalLeftDown.direction*4);
            Debug.DrawRay(_rayDiagonalRightUp.origin, _rayDiagonalRightUp.direction*4);
            Debug.DrawRay(_rayDiagonalRightDown.origin, _rayDiagonalRightDown.direction * 4);
        }
    }
    private void SquareSelected(string letter, Vector3 position, int squareIndex)
    {
        if (_assignedpoints == 0)
        {
            
            _rayStartPosition = position;
            _correctSquareList.Add(squareIndex);
            _word += letter;

            _rayUp = new Ray(new Vector2(position.x, position.y), new Vector2(0f, 1));
            _rayDown = new Ray(new Vector2(position.x, position.y), new Vector2(0f, -1));
            _rayLeft = new Ray(new Vector2(position.x, position.y), new Vector2(-1f, 0));
            _rayRight = new Ray(new Vector2(position.x, position.y), new Vector2(1f, 0));
            _rayDiagonalLeftUp = new Ray(new Vector2(position.x, position.y), new Vector2(-1f, 1f));
            _rayDiagonalLeftDown = new Ray(new Vector2(position.x, position.y), new Vector2(-1f, -1f));
            _rayDiagonalRightUp = new Ray(new Vector2(position.x, position.y), new Vector2(1f, 1f));
            _rayDiagonalRightDown = new Ray(new Vector2(position.x, position.y), new Vector2(1f, -1f));
        }
        else if(_assignedpoints == 1)
        {
            _correctSquareList.Add(squareIndex);
            _currentRay = SelectRay(_rayStartPosition,position);
            GameEvents.SelectSquareMethod(position);
            _word += letter;
            CheckWord();
        }
        else
        {
            if(IsPointOnTheRay(_currentRay , position)){
                _correctSquareList.Add(squareIndex);
                GameEvents.SelectSquareMethod(position);
                _word += letter;
                CheckWord();
            }
        }
        _assignedpoints++;
    }

    private void CheckWord()
    {
        foreach (var searchingWord in currentGameData.selectedboardData.SearchWords)
        {
            if (_word == searchingWord.Word)
            {
                GameEvents.CorrectWordMethod(_word, _correctSquareList);
               _word= string.Empty;
                _correctSquareList.Clear();
                return;


            }
        }
    }


    private bool IsPointOnTheRay(Ray currentRay, Vector3 point)
    {
        var hits = Physics.RaycastAll(currentRay, 100.0f);
        for (int i = 0; i < hits.Length; i++)
        {

            if (hits[i].transform.position == point)
            {
                return true;
            }
        }
        return false;
    }
    private Ray SelectRay(Vector2 firstPosition, Vector2 secondPosition)
    {
        var direction = (secondPosition - firstPosition).normalized;
        float tolerance = 0.01f;

        if (Math.Abs(direction.x) < tolerance && Math.Abs(direction.y - 1f) < tolerance)
        {
            return _rayUp; 
        }

        if (Math.Abs(direction.x) < tolerance && Math.Abs(direction.y - (-1f)) < tolerance)
        {
            return _rayDown; 
        }

        if (Math.Abs(direction.x - (-1f)) < tolerance && Math.Abs(direction.y) < tolerance)
        {
            return _rayLeft; 
        }

        if (Math.Abs(direction.x - 1f) < tolerance && Math.Abs(direction.y) < tolerance)
        {
            return _rayRight; 
        }
        if (direction.x  < 0f && direction.y > 0f)
        {
            return _rayDiagonalLeftUp;
        }
        if (direction.x < 0f && direction.y < 0f)
        {
            return _rayDiagonalLeftDown ;
        }
        if (direction.x > 0f && direction.y > 0f)
        {
            return _rayDiagonalRightUp;
        }
        if (direction.x > 0f && direction.y < 0f)
        {
            return _rayDiagonalRightDown;
        }
        return _rayDown ;
    }
    private void ClearSelection()
    {
        _assignedpoints = 0;
        _correctSquareList.Clear();
        _word = string.Empty;

    }
    private void CheckBoardComplete()
    {
        bool loadNextLevel = false;
        if(currentGameData.selectedboardData.SearchWords.Count == _completedWords)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

}
