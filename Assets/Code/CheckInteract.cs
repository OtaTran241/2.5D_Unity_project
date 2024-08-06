using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInteract : MonoBehaviour
{
    public GameObject uiObject;
    public GameObject inventoryButton;
    public GameObject darkBackgroundUI;
    public GameObject craftableItemsUI;
    public GameObject recipeButton;
    public GameObject recipe;
    public float checkRadius;

    private Collider[] colliders;
    [HideInInspector] public bool chestInteract;
    [HideInInspector] public GameObject chestUI;
    [HideInInspector] public GameObject craftingUI;
    [HideInInspector] public GameObject furnaceUI;
    private string check = "";
    private GameObject dropButton;

    private void Start()
    {
        dropButton = GameObject.Find("DropButton");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            colliders = Physics.OverlapSphere(transform.position, checkRadius);
            if (uiObject.activeSelf == false)
            {
                chestInteract = false;

                check = "";

                if (colliders != null)
                {
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("Chest"))
                        {
                            chestInteract = true;
                            GameObject Chest = collider.transform.GetChild(0).gameObject;
                            chestUI = Chest.transform.GetChild(0).gameObject;
                            check = "Chest";
                            break;
                        }
                        if (collider.CompareTag("CraftingTable"))
                        {
                            chestInteract = true;
                            GameObject Table = collider.transform.GetChild(0).gameObject;
                            craftingUI = Table.transform.GetChild(0).gameObject;
                            check = "CraftingTable";
                            break;
                        }
                        if (collider.CompareTag("Furnace"))
                        {
                            chestInteract = true;
                            GameObject Furnace = collider.transform.GetChild(0).gameObject;
                            furnaceUI = Furnace.transform.GetChild(0).gameObject;
                            check = "Furnace";
                            break;
                        }
                    }
                }

                if (chestInteract == false)
                {
                    OpenInventory();
                }

                else
                {
                    if (check == "Chest")
                    {
                        OpenChest();
                    }
                    if (check == "CraftingTable")
                    {
                        OpenCraftingTable();
                    }
                    if (check == "Furnace")
                    {
                        OpenFurnace();
                    }
                }
            }
            else
            {
                CloseInventory();
            }
        }

        if (chestInteract == true)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (horizontalInput != 0 || verticalInput != 0)
            {
                chestInteract = false;

                CloseInventory();
            }
        }
    }

    public void OpenChest()
    {
        OpenInventory();

        chestUI.SetActive(true);
    }

    public void OpenCraftingTable()
    {
        OpenInventory();

        craftingUI.SetActive(true);
    }

    public void OpenInventory()
    {
        recipeButton.SetActive(true);

        uiObject.SetActive(true);

        inventoryButton.SetActive(false);

        darkBackgroundUI.SetActive(true);
    }

    public void OpenCraftableItems()
    {
        if (craftableItemsUI.activeSelf == true)
        {
            uiObject.SetActive(true);

            craftableItemsUI.SetActive(false);

            recipe.SetActive(false);
        }
        else
        {
            craftableItemsUI.SetActive(true);

            uiObject.SetActive(false);

            inventoryButton.SetActive(false);

            darkBackgroundUI.SetActive(true);

            recipe.SetActive(true);
        }
    }

    public void OpenFurnace()
    {
        OpenInventory();

        furnaceUI.SetActive(true);
    }

    public void CloseInventory()
    {
        recipeButton.SetActive(false);

        recipe.SetActive(false);

        craftableItemsUI.SetActive(false);

        darkBackgroundUI.SetActive(false);

        uiObject.SetActive(false);

        inventoryButton.SetActive(true);

        foreach (Transform child in dropButton.transform)
        {
            child.gameObject.SetActive(false);
        }

        if (craftingUI != null) craftingUI.SetActive(false);

        if(furnaceUI != null) furnaceUI.SetActive(false);

        if (chestUI != null) chestUI.SetActive(false);
    }
}
