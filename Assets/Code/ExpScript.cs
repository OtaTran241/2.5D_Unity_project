using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpScript : MonoBehaviour
{
    [SerializeField] public int exp;

    private GameObject player;
    private PlayerLevel playerLevel;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLevel = player.GetComponent<PlayerLevel>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerLevel.UpdateExp(exp);

            Destroy(gameObject);
        }
    }
}
