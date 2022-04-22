using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PGCTerrain))]                        //Need to tell the Editor what class to act on
[CanEditMultipleObjects]

public class PGCTerrainEditor : Editor
{

    SerializedProperty heightMapScale;
    SerializedProperty heightMapImage;
    SerializedProperty hillVariance;
    SerializedProperty offset;
    SerializedProperty hillVariance2;
    SerializedProperty maximumHeight;

    bool showPerlinMap = false;

    private void OnEnable()
    {
        heightMapScale = serializedObject.FindProperty("heightMapScale");
        heightMapImage = serializedObject.FindProperty("heightMapImage");
        hillVariance = serializedObject.FindProperty("hillVariance");
        offset = serializedObject.FindProperty("offset");
        hillVariance2 = serializedObject.FindProperty("hillVariance2");
        //maximumHeight = serializedObject.FindProperty("maximumHeight");
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        PGCTerrain terrain = (PGCTerrain)target;               // Target is the thing the editor is working on

        showPerlinMap = EditorGUILayout.Foldout(showPerlinMap, "Perlin Map");

        if (showPerlinMap)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Perlin Map", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(hillVariance);
            EditorGUILayout.PropertyField(hillVariance2);
            EditorGUILayout.PropertyField(offset);
            EditorGUILayout.PropertyField(heightMapImage);
            EditorGUILayout.PropertyField(heightMapScale);
            //EditorGUILayout.PropertyField(maximumHeight);
            if (GUILayout.Button("Perlin Map"))
            {
                terrain.PerlinMap();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
