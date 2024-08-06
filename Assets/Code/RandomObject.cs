using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{
    //raandom theo hình vuông

    public GameObject objectsToSpawn;
    public int numberOfSpawns = 10;
    public float spawnRadius = 10f;
    public GameObject parentObject;
    public float yPos;
    public LayerMask terrainLayer;

    public void SpawnObjects()
    {
        for (int i = 0; i < numberOfSpawns; i++)
        {
            Vector3 targetPosition = transform.position;
            Vector3 randomPosition = targetPosition + new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                yPos,
                Random.Range(-spawnRadius, spawnRadius)
            );
            Vector3 randomPos = new Vector3(randomPosition.x, GetHeight(randomPosition), randomPosition.z);
            GameObject spawnedObject = Instantiate(objectsToSpawn, randomPos, Quaternion.identity);

            spawnedObject.transform.parent = parentObject.transform;
        }
    }

    private float GetHeight(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        float height = 0;
        if (Physics.Raycast(ray, out hit, 1000, terrainLayer))
        {
            Vector3 mPos = hit.point;
            height = mPos.y;
        }
        return height;
    }


    //Viền vòng tròn

    //public GameObject objectToSpawn;
    //public int numberOfSpawns = 150;
    //public float spawnRadius = 18f;
    //public float angleStep = 360f / 1f; // Góc giữa các object

    //public Transform parentTransform; // Reference to the parent Transform

    //public void SpawnObjects()
    //{
    //    for (int i = 0; i < numberOfSpawns; i++)
    //    {
    //        // Calculate angle for this object
    //        float angle = i * angleStep;

    //        // Convert angle to radians
    //        float angleInRadians = angle * Mathf.Deg2Rad;

    //        // Calculate position on the circle
    //        Vector3 targetPosition = transform.position;
    //        Vector3 randomPosition = targetPosition + new Vector3(
    //            Mathf.Cos(angleInRadians) * spawnRadius,
    //            0.1f,
    //            Mathf.Sin(angleInRadians) * spawnRadius
    //        );

    //        GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

    //        // Set the parent of the spawned object to the specified parentTransform
    //        spawnedObject.transform.parent = parentTransform;
    //    }
    //}

    //random trong vòng tròn

    //public GameObject objectToSpawn;
    //public int numberOfSpawns = 10;
    //public float spawnRadius = 18f;

    //public Transform parentTransform; // Reference to the parent Transform

    //public void SpawnObjects()
    //{
    //    for (int i = 0; i < numberOfSpawns; i++)
    //    {
    //        // Calculate random angle in radians
    //        float angle = Random.Range(0f, Mathf.PI * 2f);

    //        // Calculate random distance from the center (0 to spawnRadius)
    //        float randomDistance = Random.Range(0f, spawnRadius);

    //        // Calculate random position inside the circle
    //        Vector3 targetPosition = transform.position;
    //        Vector3 randomPosition = targetPosition + new Vector3(
    //            Mathf.Cos(angle) * randomDistance,
    //            0f,
    //            Mathf.Sin(angle) * randomDistance
    //        );

    //        GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

    //        // Set the parent of the spawned object to the specified parentTransform
    //        spawnedObject.transform.parent = parentTransform;
    //    }
    //}
}
