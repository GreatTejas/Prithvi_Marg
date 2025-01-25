using System;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
[CustomEditor(typeof(BoardData),false)]
[CanEditMultipleObjects]
[System.Serializable]
public class BoardDataDrawer : Editor
{
    private BoardData GameDataInstance => target as BoardData;
    private ReorderableList dataList;
    private void OnEnable()
    {
        if (GameDataInstance.SearchWords == null)
        {
            GameDataInstance.SearchWords = new List<BoardData.SearchingWord>();
        }
        IntialisedReorderablelist(ref dataList , "SearchWords", "SearchingWord");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GameDataInstance.timeInSeconds =
            EditorGUILayout.FloatField("Max Game Time(in seconds", GameDataInstance.timeInSeconds);



        DrawColumnsRowsInputFeilds();
        EditorGUILayout.Space();
        ConvertToUpperButton();
        serializedObject.ApplyModifiedProperties();
        if(GameDataInstance.Board != null && GameDataInstance.Columns > 0 && GameDataInstance.Rows >0)
        {
            DrawBoardTable();
        }
        GUILayout.BeginHorizontal();
        ClearBoardButton();
        FillUpWithRandLettersButton();
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
        dataList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(GameDataInstance);
        }
    }
    private void DrawColumnsRowsInputFeilds()
    {
        var ColumnsTemp = GameDataInstance.Columns;
        var RowsTemp = GameDataInstance.Rows;

        GameDataInstance.Columns = EditorGUILayout.IntField("Columns ", GameDataInstance.Columns);
        GameDataInstance.Rows = EditorGUILayout.IntField("Rows ", GameDataInstance.Rows);
        if(GameDataInstance.Columns != ColumnsTemp || GameDataInstance.Rows != RowsTemp
            && GameDataInstance.Columns>0 &&GameDataInstance.Rows>0 )
        {
            GameDataInstance.CreateNewBoard();
        }
    }
    private void DrawBoardTable()
    {
        var TableStyle = new GUIStyle("box");
        TableStyle.padding = new RectOffset(10, 10, 10, 10);
        TableStyle.margin.left = 32;
        var headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 35;
        var ColumnStyle = new GUIStyle();
        ColumnStyle.fixedWidth = 50;
        var RowStyle = new GUIStyle();
        RowStyle.fixedHeight = 25;
        RowStyle.fixedWidth = 40;
        RowStyle.alignment = TextAnchor.MiddleCenter;
        var textFeildStyle = new GUIStyle();
        textFeildStyle.normal.background = Texture2D.grayTexture;
        textFeildStyle.normal.textColor = Color.white;
        textFeildStyle.fontStyle = FontStyle.Bold;
        textFeildStyle.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.BeginHorizontal(TableStyle);
        for (var x = 0; x < GameDataInstance.Columns; x++)
        {
            EditorGUILayout.BeginVertical(x==-1 ? headerColumnStyle : ColumnStyle);
            for( var y = 0; y < GameDataInstance.Rows; y++)
            {
                if(x >=0 && y >= 0)
                {
                    EditorGUILayout.BeginHorizontal(RowStyle);
                    var character = (string)EditorGUILayout.TextArea(GameDataInstance.Board[x].Row[y], textFeildStyle);
                    if (GameDataInstance.Board[x].Row[y].Length > 1)
                    {
                        character= GameDataInstance.Board[x].Row[y].Substring(0,1);
                    }
                    GameDataInstance.Board[x].Row[y] = character;
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal ();
    }
    private void IntialisedReorderablelist(ref ReorderableList list , string porpertyName , string listlabel)
    {
        list = new ReorderableList(serializedObject,serializedObject.FindProperty(porpertyName),
            true,true,true,true);
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, listlabel);
        };
        var l = list;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {

            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(new Rect(rect.x,rect.y,EditorGUIUtility.labelWidth,EditorGUIUtility.singleLineHeight), 
                element.FindPropertyRelative("Word"),GUIContent.none);
        };
    }
    private void ConvertToUpperButton()
    {
        if(GUILayout.Button("To Upper"))
        {
            for(var i = 0; i < GameDataInstance.Columns; i++)
            {
                for( var j=0; j<GameDataInstance.Rows; j++)
                {
                    var errorCounter = Regex.Matches(GameDataInstance.Board[i].Row[j], @"[a-z]").Count;
                    if(errorCounter > 0)
                        GameDataInstance.Board[i].Row[j]=GameDataInstance.Board[i].Row[j].ToUpper();
                }
            }

            foreach( var SearchWords in GameDataInstance.SearchWords)
            {
                var errorCounter = Regex.Matches(SearchWords.Word, @"[a-z]").Count;
                if(errorCounter > 0)
                {
                    SearchWords.Word = SearchWords.Word.ToUpper();
                }
            }
        }
    }

    private void ClearBoardButton()
    {
        if (GUILayout.Button("ClearBoard"))
        {
            for(int i=0;i<GameDataInstance.Columns;i++)
            {
                for(int j = 0; j < GameDataInstance.Rows; j++)
                {
                    GameDataInstance.Board[i].Row[j] = " ";
                }
            }
        }
    }

    private void FillUpWithRandLettersButton()
    {
        if(GUILayout.Button("Fill up with Random"))
        {
            for(int i=0; i<GameDataInstance.Columns;i++)
            {
                for(int j = 0;j < GameDataInstance.Rows; j++)
                {
                    int errorCounter = Regex.Matches(GameDataInstance.Board[i].Row[j], @"[a-zA-Z]").Count;
                    string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    int index = UnityEngine.Random.Range(0,letters.Length);
                    if (errorCounter == 0)
                    {
                        GameDataInstance.Board[i].Row[j] = letters[index].ToString();

                    }
                }
            }
        }
    }



}
