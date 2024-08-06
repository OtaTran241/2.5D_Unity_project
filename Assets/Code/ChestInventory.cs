using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ChestInventory : MonoBehaviour
{
    [SerializeField] private GameObject chestUI;
    public InventorySlot[] chestSlot;
    public GameObject itemToPickupPf;
    public Item thisItem;
    public GameObject dropButton;

    private bool buttonActive;
    private CheckInteract check;
    private GameObject player;


    private void Start()
    {
        player = GameObject.Find("Player");
        check = player.GetComponentInChildren<CheckInteract>();
    }

    private void Update()
    {
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
        for (int i = 0; i < chestSlot.Length; i++)
        {
            InventorySlot slot = chestSlot[i];
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
            check.chestUI = chestUI;
            check.chestInteract = true;
            check.OpenChest();
        }
        else
        {
            Debug.Log("too far away");
        }
    }

}
