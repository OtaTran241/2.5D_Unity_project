using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Melting : MonoBehaviour
{
    [SerializeField] private InventorySlot itemSlot;
    [SerializeField] private InventorySlot fuelSlot;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private List<Item> fuelList;
    [SerializeField] private GameObject furnaceUI;
    [SerializeField] private new Light light;
    public GameObject itemToPickupPf;
    public Item thisItem;
    public GameObject dropButton;
    public List<Item> oreList;
    public List<Item> dustList;

    private bool buttonActive;
    private GameObject player;
    private InventoryManager inventoryManager;
    private bool checkItem;
    private bool checkFuel;
    private float meltingTime = 0;
    private float countdown = 2f;
    private Item oreItem;
    private int count = 0;
    private InventoryItem itemInSlot;
    private InventoryItem fuelInSlot;
    private CheckInteract check;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        inventoryManager = player.GetComponentInChildren<InventoryManager>();
        check = player.GetComponentInChildren<CheckInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        itemInSlot = itemSlot.GetComponentInChildren<InventoryItem>();
        fuelInSlot = fuelSlot.GetComponentInChildren<InventoryItem>();

        if (meltingTime > 0)
        {
            meltingTime -= Time.deltaTime;
            light.gameObject.SetActive(true);
        }
        else
        {
            light.gameObject.SetActive(false);
        }
        meltingTime = CheckZero(meltingTime);
        CheckTrue();
        if (itemInSlot != null && fuelInSlot != null)
        {
            if (checkItem == true && checkFuel == true && (oreItem == null || oreItem == ItemReceived(itemInSlot)))
            {
                if (meltingTime == 0)
                {
                    fuelInSlot.count -= 1;
                    fuelInSlot.RefreshCount();
                    meltingTime = 6f;
                    CountCheck(fuelInSlot);
                }
            }
        }

        if (itemInSlot != null)
        {
            if (checkItem == true && meltingTime > 0 && (oreItem == null || oreItem == ItemReceived(itemInSlot)))
            {
                if (countdown > 0)
                {
                    countdown -= Time.deltaTime;
                }
                countdown = CheckZero(countdown);
                if (countdown == 0)
                {
                    itemInSlot.count -= 1;
                    itemInSlot.RefreshCount();
                    countdown = 2f;
                    oreItem = ItemReceived(itemInSlot);
                    CountCheck(itemInSlot);
                    CheckItemOre();
                    count += 1;
                    countText.text = count.ToString();
                }
            }
        }

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
        if (itemInSlot != null)
        {
            var item = Instantiate(itemToPickupPf, pos.RandomPos(transform.position), Quaternion.identity);
            item.GetComponent<Pickup>().item = itemInSlot.item;
            item.GetComponent<Pickup>().count = itemInSlot.count;
            item.GetComponent<SpriteRenderer>().sprite = itemInSlot.item.image;
            Destroy(itemInSlot.gameObject);
        }

        if (fuelInSlot != null)
        {
            var fuel = Instantiate(itemToPickupPf, pos.RandomPos(transform.position), Quaternion.identity);
            fuel.GetComponent<Pickup>().item = fuelInSlot.item;
            fuel.GetComponent<Pickup>().count = fuelInSlot.count;
            fuel.GetComponent<SpriteRenderer>().sprite = fuelInSlot.item.image;
            Destroy(itemInSlot.gameObject);
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
            check.furnaceUI = furnaceUI;
            check.chestInteract = true;
            check.OpenFurnace();
        }
        else
        {
            Debug.Log("too far away");
        }
    }

    private void CountCheck(InventoryItem item)
    {
        if (item.count == 0)
        {
            Destroy(item.gameObject);
        }
    }

    private Item ItemReceived(InventoryItem itemInSlot)
    {
        Item item = null;
        for (int i = 0; i < dustList.Count; i++)
        {
            if (itemInSlot.item == dustList[i])
            {
                item = oreList[i];
                break;
            }
        }
        return item;
    }

    private void CheckItemOre()
    {
        if (oreItem != null)
        {
            itemImage.sprite = oreItem.image;
        }
    }

    private float CheckZero(float number)
    {
        float n = number;
        if (number < 0)
        {
            n = 0;
        }
        return n;
    }

    public void AddToInventory()
    {
        if (oreItem != null && count != 0)
        {
            bool result = inventoryManager.AddItems(oreItem, count);
            if (result)
            {
                oreItem = null;
                itemImage.sprite = null;
                countText.text = null;
                count = 0;
            }
        }
    }

    public void CheckTrue()
    {
        if (itemInSlot != null)
        {
            checkItem = false;
            for (int i = 0; i < dustList.Count; i++)
            {
                if (itemInSlot.item == dustList[i])
                {
                    checkItem = true;
                    break;
                }
                else
                {
                    checkItem = false;
                }
            }
        }

        if (fuelInSlot != null)
        {
            checkFuel = false;
            for (int i = 0; i < fuelList.Count; i++)
            {
                if (fuelInSlot.item == fuelList[i])
                {
                    checkFuel = true;
                    break;
                }
                else
                {
                    checkFuel = false;
                }
            }
        }
    }
}
