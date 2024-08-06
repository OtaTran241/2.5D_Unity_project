using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineshaftGate : MonoBehaviour
{
    public GameObject MineGateSpawn;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = MineGateSpawn.transform.position;
        }
    }
}
