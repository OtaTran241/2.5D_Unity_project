using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item;
    public int count = 1;
    public TextMesh countText;

    private Animator an;
    private GameObject target;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        target = GameObject.Find("Player");
        inventoryManager = target.GetComponentInChildren<InventoryManager>();
    }

    private void Start()
    {
        Destroy(gameObject, 360);
        an = GetComponent<Animator>();
        an.Play("ItemPickupAnimation");
        transform.rotation = Quaternion.Euler(new Vector3(45f, 0f, 0f));

        if (count > 1)
        {
            var go = Instantiate(countText,
                new Vector3(transform.position.x + 0.9f, transform.position.y + 0.4f, transform.position.z + 0.4f)
            , transform.rotation, transform);
            go.GetComponent<TextMesh>().text = "x" + count.ToString();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            bool result = inventoryManager.AddItems(item, count);
            if (result)
            {
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "Terrain" && !transform.GetComponent<Rigidbody>().isKinematic)
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
