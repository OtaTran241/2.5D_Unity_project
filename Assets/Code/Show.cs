using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show : MonoBehaviour
{
    private GameObject craftableItemsUI;
    private ShowRecipe showRecipe;
    public Item item;
    public Image image;
    public GameObject tool;

    private void Start()
    {
        craftableItemsUI = GameObject.Find("CraftableItems");
        showRecipe = craftableItemsUI.GetComponent<ShowRecipe>();
        image.sprite = item.image;
    }
    public void TurnOnResipe()
    {
        showRecipe.ChangeRecipe(item, tool);
        showRecipe.OpenRecipe();
    }

    public void TurnOffResipe()
    {
        showRecipe.CloseRecipe();
    }
}
