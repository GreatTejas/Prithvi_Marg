using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework.Internal;

[CustomEditor(typeof(AlphabetData))]
[CanEditMultipleObjects]
[System.Serializable]
public class AlphabetDataDrawer : Editor
{
    private ReorderableList AlphabetPlainList;
    private ReorderableList AlphabetNormalList;
    private ReorderableList AlphabetHighlightedList;
    private ReorderableList AlphabetWrongList;

    private void OnEnable()
    {
        IntialiseReorderableList(ref AlphabetWrongList, "AlphabetWrong", "AlphabetWrong");
        IntialiseReorderableList(ref AlphabetPlainList, "AlphabetPlain", "AlphabetPlain");
        IntialiseReorderableList(ref AlphabetNormalList, "AlphabetNormal", "AlphabetNormal");
        IntialiseReorderableList(ref AlphabetHighlightedList, "AlphabetHighlighted", "AlphabetHighlighted");

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        AlphabetPlainList.DoLayoutList();
        AlphabetNormalList.DoLayoutList();
        AlphabetHighlightedList.DoLayoutList();
        AlphabetWrongList.DoLayoutList();
        serializedObject.ApplyModifiedProperties(); 
    }
    private void IntialiseReorderableList(ref ReorderableList list, string propertyName, string listLabel)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName),
            true, true, true, true);
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, listLabel);
        };
        var l = list;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {

            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("letter"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x+70, rect.y, rect.width-60-30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("image"), GUIContent.none);

        };
    }
}
