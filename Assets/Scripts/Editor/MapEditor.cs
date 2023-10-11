using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnityEditorInternal;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor {

    SerializedProperty _mapElementObjects;

    public override void OnInspectorGUI() {

        Map map = (Map)target;
        _mapElementObjects = serializedObject.FindProperty("mapElements");
        EditorGUILayout.Space();
        if (GUILayout.Button("Store Map Elements")) {
            serializedObject.Update();

            _mapElementObjects.ClearArray();
            for (int i = 0;i < map.transform.childCount;++i) {

                if (map.transform.GetChild(i).TryGetComponent(out MapElement mapElement)) {
                    _mapElementObjects.arraySize++;
                    _mapElementObjects.GetArrayElementAtIndex(_mapElementObjects.arraySize - 1).objectReferenceValue = map.transform.GetChild(i).gameObject;
                }
            }
            Debug.Log(_mapElementObjects.arraySize + " Elements found and stored.");

            serializedObject.ApplyModifiedProperties();
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_mapElementObjects);
    }

}
