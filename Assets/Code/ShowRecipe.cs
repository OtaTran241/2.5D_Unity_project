using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.Windows;
using static Crafting;

public class ShowRecipe : MonoBehaviour
{
    public GameObject recipeUI;
    public GameObject craftableItemsUI;
    public GameObject itemToRecipePf;
    public Image[] recipeSlots;
    public Image ItemReceived;
    public Image toolNeeded;
    public TextMeshProUGUI itemName;
    public GameObject craftingTable;
    public GameObject furnace;
    public Sprite nullSprite;

    private Melting melting;
    private Crafting crafting;
    private List<itemsCrafting> itemToCraft;
    private List<Item> itemsCrafting = new List<Item>();
    private List<Item> itemsMelting;
    private List<Item> dustItems;
    private List<string> recipes;
    private List<Item> allItem;

    private void Start()
    {
        melting = furnace.GetComponent<Melting>();
        crafting = craftingTable.GetComponent<Crafting>();
        itemsMelting = melting.oreList;
        itemToCraft = crafting.CreateItemsCraftingList();
        recipes = crafting.CreateRecipesList();
        allItem = crafting.itemList;
        dustItems = melting.dustList;
        nullSprite = recipeSlots[2].sprite;
        for (int i = 0; i < itemsMelting.Count; i++)
        {
            var go = Instantiate(itemToRecipePf);
            go.GetComponent<Show>().item = itemsMelting[i];
            go.GetComponent<Show>().tool = furnace;
            go.transform.SetParent(craftableItemsUI.transform);
        }

        for (int k = 0; k < itemToCraft.Count; k++)
        {
            itemsCrafting.Add(itemToCraft[k].item);
        }

        for (int j = 0; j < itemsCrafting.Count; j++)
        {
            var go = Instantiate(itemToRecipePf);
            go.GetComponent<Show>().item = itemsCrafting[j];
            go.GetComponent<Show>().tool = craftingTable;
            go.transform.SetParent(craftableItemsUI.transform);
        }
    }

    public void OpenRecipe()
    {
        recipeUI.SetActive(true);
    }

    public void CloseRecipe()
    {
        recipeUI.SetActive(false);
    }

    public void ChangeRecipe(Item item, GameObject tool)
    {
        toolNeeded.sprite = tool.GetComponent<SpriteRenderer>().sprite;
        ItemReceived.sprite = item.image;
        itemName.text = Regex.Replace(item.name, "(\\B[A-Z])", " $1");
        if (tool == craftingTable)
        {
            for (int i = 0; i < itemsCrafting.Count; i++)
            {
                if (itemsCrafting[i] == item)
                {
                    string[] words = recipes[i].Split(' ');

                    for (int j = 0; j < recipeSlots.Length; j++)
                    {
                        if (words[j] == "Null")
                        {
                            recipeSlots[j].sprite = nullSprite;
                            recipeSlots[j].color = new Color(255f, 255f, 255f, 255f);
                        }
                        else
                        {
                            recipeSlots[j].sprite = allItem.Find(item => item.name == words[j]).image;
                            recipeSlots[j].color = new Color(255f, 255f, 255f, 255f);
                        }
                    }
                    break;
                }
            }
        }
        if (tool == furnace)
        {
            for (int i = 0; i < itemsMelting.Count; i++)
            {
                if (itemsMelting[i] == item)
                {
                    for (int j = 0; j < recipeSlots.Length; j++)
                    {
                        if (j == 3) recipeSlots[3].sprite = dustItems[i].image;
                        else
                        {
                            recipeSlots[j].sprite = null;
                            recipeSlots[j].color = new Color(0f,0f,0f,0f);
                        }
                    }
                    break;
                }
            }
        }
    }
}
