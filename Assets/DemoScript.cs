using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItems(itemToPickup[id],1);
        if (result == true)
        {
            Debug.Log("1");
        }
        else
        {
            Debug.Log("2");
        }
    }
    public void GetSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);

        if (receivedItem != null)
        {
            Debug.Log("Item:" + receivedItem);
        }
        else
        {
            Debug.Log("No item");
        }
    }

    public void UseSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);

        if (receivedItem != null)
        {
            Debug.Log("Item:" + receivedItem);
        }
        else
        {
            Debug.Log("No item use");
        }
    }
}
