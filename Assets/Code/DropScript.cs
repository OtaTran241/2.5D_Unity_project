using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    public Item[] itemToDrop;
    public GameObject itemDropPf;
    public int health;
    public int[] dropCount;

    private bool sceneClosing = false;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        if (!sceneClosing)
        {
            DropItem();
        }
    }

    private void DropItem()
    {
        for (int k = 0; k < itemToDrop.Length; k++)
        {
            int dropC = Random.Range(dropCount[k] - 2, dropCount[k]);

            for (int i = 0; i < dropC; i++)
            {
                Vector3 dropP = new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1), transform.position.y + 2, Random.Range(transform.position.z - 1, transform.position.z + 1));
                var go = Instantiate(itemDropPf, dropP, Quaternion.identity);
                go.GetComponent<Pickup>().item = itemToDrop[k];
                go.GetComponent<Pickup>().count = 1;
                go.GetComponent<SpriteRenderer>().sprite = itemToDrop[k].image;
                
            }
        }
    }

    private void OnApplicationQuit()
    {
        sceneClosing = true;
    }
}
