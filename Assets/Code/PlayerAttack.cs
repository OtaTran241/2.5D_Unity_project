using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private int damage = 0;
    private InventoryManager im;
    private InventorySlot slot;
    private InventoryItem itemInSlot;
    private PlayerFoodManager pFM;

    private void Start()
    {
        pFM = target.gameObject.GetComponent<PlayerFoodManager>();
    }

    void Update()
    {
        im = target.GetComponentInChildren<InventoryManager>();

        slot = im.currentlySelectedSlot;
        itemInSlot = slot.GetComponentInChildren<InventoryItem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pFM.currentFood > 5)
        {
            if (other.gameObject.tag == "Enemy")
            {
                HealthManager eHM;
                eHM = other.gameObject.GetComponent<HealthManager>();

                if (im != null)
                {
                    if (itemInSlot != null)
                    {
                        damage = itemInSlot.item.damage;
                    }
                }

                if (eHM != null)
                {
                    eHM.TakeDamage(Random.Range(damage - 1, damage + 1));
                    pFM.ReduceFood(Random.Range(1, 3));
                }
            }
            if (other.gameObject.tag == "Tree" && itemInSlot.item.name == "IronAxe")
            {
                DropScript tree;
                tree = other.gameObject.GetComponent<DropScript>();

                if (im != null)
                {
                    damage = itemInSlot.item.damage;
                }
                tree.TakeDamage(Random.Range(damage - 1, damage + 1));
                pFM.ReduceFood(Random.Range(1, 3));
            }

            if (other.gameObject.tag == "Ore" && itemInSlot.item.name == "Pickaxe")
            {
                DropScript ore;
                ore = other.gameObject.GetComponent<DropScript>();

                if (im != null)
                {
                    damage = itemInSlot.item.damage;
                }
                ore.TakeDamage(Random.Range(damage - 1, damage + 1));
                pFM.ReduceFood(Random.Range(1, 3));
            }
        }
    }
}
