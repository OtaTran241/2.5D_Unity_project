using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject[] animal;
    public GameObject[] boss;
    public int spawnNumber = 6;
    public float timeBetweenSpawns = 10f;
    public TextMeshProUGUI timeText;
    public float detectionRadius = 15f;
    public float distance = 10f;

    public LayerMask terrainLayer;
    private GameObject player;
    private bool spawn;
    private int hour;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        string h = timeText.text.Substring(0, 2);
        int.TryParse(h, out hour);

        if (hour >= 18 || hour <= 6) spawn = true;
        else spawn = false;
    }

    private IEnumerator SpawnEnemy()
    {
        while (spawn)
        { 
            if (CheckQuantity() && CheckTerrain())
            {
                Instantiate(enemy[Random.Range(0, enemy.Length)], SpawnPos(), Quaternion.identity);
            }
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private Vector3 RandomPos()
    {
        Vector2 randomOffset = Random.insideUnitCircle.normalized * detectionRadius;
        Vector3 randomPosition = new Vector3(randomOffset.x, 0f, randomOffset.y);
        randomPosition += player.transform.position - new Vector3(0f, 0f, distance);
        return randomPosition;
    }

    private Vector3 SpawnPos()
    {
        float height;
        Vector3 spawnPos;
        do
        {
            Vector3 playerPos = player.transform.position;
            Vector3 pos = RandomPos();
            height = GetHeight(pos);
            spawnPos = new Vector3(pos.x, height, pos.z);
        } while ((height - player.transform.position.y) > 2);
        return spawnPos;
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
        return height + 1;
    }


    private bool CheckTerrain()
    {
        Ray ray = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, terrainLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckQuantity()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        int enemyCount = 0;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemyCount++;
            }
        }

        if (enemyCount < spawnNumber)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
