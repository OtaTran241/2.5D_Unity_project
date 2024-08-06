using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public List<Item> itemList;
    public InventorySlot[] craftingSlot;
    public Image image;
    public TextMeshProUGUI countText;
    public GameObject craftingUI;
    public GameObject itemToPickupPf;
    public Item thisItem;
    public GameObject dropButton;

    private GameObject player;
    private CheckInteract check;
    private InventoryManager inventoryManager;
    [HideInInspector] public List<string> recipes { get; set; }
    [HideInInspector] public List<itemsCrafting> itemCraftings;
    private Item itemsToCraft;
    private int craftCount;
    private bool buttonActive;

    public class itemsCrafting
    {
        public Item item { get; set; }
        public int count { get; set; }
    }


    private void Start()
    {
        player = GameObject.Find("Player");
        inventoryManager = player.GetComponentInChildren<InventoryManager>();
        check = player.GetComponent<CheckInteract>();
        recipes = CreateRecipesList();
        itemCraftings = CreateItemsCraftingList();
    }

    private void Update()
    {
        Check();
        if (buttonActive)
        {
            if (Vector3.Distance(transform.position, player.transform.position) >= 5)
            {
                buttonActive = false;
            }
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            dropButton.transform.position = pos;
            dropButton.gameObject.SetActive(true);
        }
        else
        {
            dropButton.gameObject.SetActive(false);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (dropButton.gameObject.activeSelf == true)
            {
                buttonActive = false;
            }
            else
            {
                buttonActive = true;
            }
        }
    }

    public void DropThisTranform()
    {
        GameObject rand = GameObject.Find("DropButton");
        ClickToDrop pos = rand.GetComponent<ClickToDrop>();
        for (int i = 0; i < craftingSlot.Length; i++)
        {
            InventorySlot slot = craftingSlot[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                var item = Instantiate(itemToPickupPf, pos.RandomPos(transform.position), Quaternion.identity);
                item.GetComponent<Pickup>().item = itemInSlot.item;
                item.GetComponent<Pickup>().count = itemInSlot.count;
                item.GetComponent<SpriteRenderer>().sprite = itemInSlot.item.image;
                Destroy(itemInSlot.gameObject);
            }
        }
        var go = Instantiate(itemToPickupPf, transform.position, Quaternion.identity);
        go.GetComponent<Pickup>().item = thisItem;
        go.GetComponent<Pickup>().count = 1;
        go.GetComponent<SpriteRenderer>().sprite = thisItem.image;
        Destroy(transform.gameObject);
    }

    private void OnMouseDown()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 2.5f)
        {
            check.CloseInventory();
            check.craftingUI = craftingUI;
            check.chestInteract = true;
            check.OpenCraftingTable();
        }
        else
        {
            Debug.Log("too far away");
        }
    }

    public void Check()
    {
        bool success = false;
        string itemToCraft = "";
        for (int i = 0; i < craftingSlot.Length; i++)
        {
            InventorySlot slot = craftingSlot[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                itemToCraft += (itemInSlot.item.name + " ");
            }
            else
            {
                itemToCraft += "Null ";
            }
        }

        for (int j = 0; j < recipes.Count; j++)
        {
            if (recipes[j].Trim() == itemToCraft.Trim())
            {
                image.sprite = itemCraftings[j].item.image;
                countText.text = itemCraftings[j].count.ToString();
                image.enabled = true;
                itemsToCraft = itemCraftings[j].item;
                craftCount = itemCraftings[j].count;
                success = true;
                break;
            }
            else
            {
                success = false;
            }
        }

        if (success == false)
        {
            image.sprite = null;
            image.enabled = false;
            countText.text = null;
            itemsToCraft = null;
            craftCount = 0;
        }
    }

    public List<string> CreateRecipesList()
    {
        List<string> recipe = new List<string>();
        recipe.Add("Stick IronHilt IronBlade Null");
        recipe.Add("Stick Coal Null Null");
        recipe.Add("Iron Iron Null Null");
        recipe.Add("Wood Null Null Null");
        recipe.Add("Wood Wood Stick Stick");
        recipe.Add("Rock Rock Coal Null");
        recipe.Add("Iron Null Null Null");
        recipe.Add("Iron Iron Iron Null");
        recipe.Add("Stick Iron Iron Null");
        recipe.Add("Iron Stick Iron Null");
        recipe.Add("Stick Iron Null Null");
        recipe.Add("Stick Iron Iron Iron");
        recipe.Add("Stick Iron Null Null");
        recipe.Add("Wood Wood Iron Iron");
        recipe.Add("Wood Wood Diamond Diamond");

        return recipe;
    }

    public List<itemsCrafting> CreateItemsCraftingList()
    {
        List<itemsCrafting> itemCrafting = new List<itemsCrafting>();
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "IronSword"), count = 1 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "Torch"), count = 2 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "AnimalTrap"), count = 2 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "Stick"), count = 4 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "CraftingTable"), count = 1 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "Furnace"), count = 1 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "IronHilt"), count = 1 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "IronBlade"), count = 1 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "IronAxe"), count = 1 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "IronPickaxe"), count = 1 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "IronShovel"), count = 1 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "IronHammer"), count = 1 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "IronHoe"), count = 1 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "WoodChest"), count = 1 });
        itemCrafting.Add(new itemsCrafting { item = itemList.Find(item => item.name == "DiamondChest"), count = 1 });

        return itemCrafting;
    }

    public void ClickToCraft()
    {
        if (itemsToCraft != null && craftCount != 0)
        {
            bool result = inventoryManager.AddItems(itemsToCraft, craftCount);

            if (result)
            {
                for (int i = 0; i < craftingSlot.Length; i++)
                {
                    InventorySlot slot = craftingSlot[i];
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                    if (itemInSlot != null)
                    {
                        itemInSlot.count -= 1;
                        itemInSlot.RefreshCount();

                        if (itemInSlot.count <= 0)
                        {
                            Destroy(itemInSlot.gameObject);
                        }
                    }
                }
            }
        }
    }
}
