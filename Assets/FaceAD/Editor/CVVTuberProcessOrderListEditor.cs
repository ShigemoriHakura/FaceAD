﻿using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;

namespace FaceADVtuber
{
    [CustomEditor (typeof(FaceADVTuberProcessOrderList), true)]
    public class CVVTuberProcessOrderListEditor : Editor
    {
        ReorderableList m_list;

        void OnEnable ()
        {
            m_list = new ReorderableList (
                serializedObject,
                serializedObject.FindProperty ("processOrderList")
            );

            m_list.drawElementCallback += (rect, index, isActive, isFocused) => {

                var element = m_list.serializedProperty.GetArrayElementAtIndex (index);
                if (element != null) {
                    rect.y += 2;

                    if (element.objectReferenceValue == null) {
                        EditorGUI.ObjectField (new UnityEngine.Rect (rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, new UnityEngine.GUIContent ((index + 1) + ". "));
                    } else {
                        EditorGUI.ObjectField (new UnityEngine.Rect (rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, new UnityEngine.GUIContent ((index + 1) + ". " + ((FaceADVTuberProcess)element.objectReferenceValue).GetDescription ()));
                    }
                }
            };

            m_list.drawHeaderCallback += (rect) => {
                EditorGUI.LabelField (rect, "Process Order List");
            };

            m_list.onAddCallback += (list) => {

                var prop = list.serializedProperty;

                prop.arraySize++;
                list.index = prop.arraySize - 1;
                var element = prop.GetArrayElementAtIndex (list.index);
                element.objectReferenceValue = null;
            };
        }

        public override void OnInspectorGUI ()
        {
            serializedObject.Update ();

            m_list.DoLayoutList ();

            serializedObject.ApplyModifiedProperties ();
        }
    }
}