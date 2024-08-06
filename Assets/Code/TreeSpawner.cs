#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;

//[CustomEditor(typeof(Terrain))]
public class RandomObjectPlacer : Editor
{
    public GameObject prefabToSpawn;
    public int numberOfObjects = 50;
    public float VectorY;
    public Vector3 spawnSize = new Vector3(100, 0, 100);

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);

        prefabToSpawn = EditorGUILayout.ObjectField("Prefab to Spawn", prefabToSpawn, typeof(GameObject), true) as GameObject;
        numberOfObjects = EditorGUILayout.IntField("Number of Objects", numberOfObjects);
        VectorY = EditorGUILayout.FloatField("Vector Y", VectorY);
        spawnSize = EditorGUILayout.Vector3Field("Spawn Size", spawnSize);

        GUILayout.Space(10);

        if (GUILayout.Button("Spawn Objects"))
        {
            SpawnObjects();
        }
    }

    void SpawnObjects()
    {
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab to spawn is not assigned!");
            return;
        }

        Terrain terrain = target as Terrain;
        TerrainData terrainData = terrain.terrainData;

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(0, spawnSize.x),
                0,
                Random.Range(0, spawnSize.z)
            );

            Vector3 worldPosition = terrain.GetPosition() + randomPosition;

            float terrainHeight = terrain.SampleHeight(worldPosition);
            worldPosition.y = terrainHeight;

            Instantiate(prefabToSpawn, worldPosition, Quaternion.identity);
        }
    }
}
#endif
