using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public Item itemToDrop;
    public GameObject exp;
    public GameObject itemDropPf;
    public int dropChance = 80;

    private bool sceneClosing = false;

    private void OnDestroy()
    {
        if (!sceneClosing)
        {
            if (Random.Range(0, 100) < dropChance)
            {
                DropItem();
            }

            Instantiate(exp, transform.position, Quaternion.Euler(new Vector3(75f, 0f, 0f)));
        }
    }

    private void DropItem()
    {
        Vector3 dropP = new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1), transform.position.y + 2, Random.Range(transform.position.z - 1, transform.position.z + 1));
        var go = Instantiate(itemDropPf, dropP, Quaternion.Euler(new Vector3(45f, 0f, 0f)));
        go.GetComponent<Pickup>().item = itemToDrop;
        go.GetComponent<Pickup>().count = 1;
        go.GetComponent<SpriteRenderer>().sprite = itemToDrop.image;
    }

    private void OnApplicationQuit()
    {
        sceneClosing = true;
    }
}
