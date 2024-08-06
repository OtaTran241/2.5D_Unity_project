using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryManager : MonoBehaviour
{
    public GameObject Hand_Pos;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    private SpriteRenderer hand;
    [HideInInspector] public InventorySlot currentlySelectedSlot;

    int selectedSlot = -1;

    private void Start()
    {
        ChangeSelectedSlot(0);
        hand = Hand_Pos.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (currentlySelectedSlot != null)
        {
            InventoryItem itemInSlot = currentlySelectedSlot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                hand.sprite = null;
            }
            else if (hand != null && itemInSlot != null && itemInSlot.item.type == ItemType.tool || itemInSlot.item.type == ItemType.sword)
            {
                hand.sprite = itemInSlot.item.image;
            }
            else
            {
                hand.sprite = null;
            }
        }

        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number >= 0 && number <= 9)
            {
                if (number == 0)
                {
                    ChangeSelectedSlot(9);
                }
                else
                {
                    ChangeSelectedSlot(number - 1);
                }
            }
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
        InventorySlot slot = inventorySlots[newValue];
        currentlySelectedSlot = slot;
    }

    //public bool AddItem(Item item)
    //{
    //    for (int i = 0; i < inventorySlots.Length; i++)
    //    {
    //        InventorySlot slot = inventorySlots[i];
    //        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
    //        if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < itemInSlot.item.stackSize && itemInSlot.item.stackable == true)
    //        {
    //            itemInSlot.count++;
    //            itemInSlot.RefreshCount();
    //            return true;
    //        }
    //    }

    //    for (int i=0; i<inventorySlots.Length; i++)
    //    {
    //        InventorySlot slot = inventorySlots[i];
    //        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
    //        if (itemInSlot == null)
    //        {
    //            SpawnNewItem(item, 1, slot);
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    public bool AddItems(Item item, int count)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < itemInSlot.item.stackSize && itemInSlot.item.stackable == true)
            {
                if ((itemInSlot.count + count) <= itemInSlot.item.stackSize)
                {
                    itemInSlot.count += count;
                    itemInSlot.RefreshCount();
                    return true;
                }
                if ((itemInSlot.count + count) > itemInSlot.item.stackSize)
                {
                    int c = itemInSlot.count;
                    itemInSlot.count = itemInSlot.item.stackSize;
                    AddItems(item, ((c + count) - itemInSlot.item.stackSize));
                    itemInSlot.RefreshCount();
                    return true;
                }
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, count, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, int count, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item,count);
        inventoryItem.count = count;
        inventoryItem.dropButton = GameObject.Find("DropButton");
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }

        return null;
    }

}
