#if UNITY_EDITOR 
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RandomObject))]
public class RandomObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RandomObject spawner = (RandomObject)target;

        if (GUILayout.Button("Spawn Random Objects"))
        {
            spawner.SpawnObjects();
        }
    }
}
#endif
