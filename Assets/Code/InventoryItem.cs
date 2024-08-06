using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using System.Text.RegularExpressions;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;
    public Item item;

    [HideInInspector] public GameObject dropButton;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int count = 1;

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(GameObject.Find("InventoryUI").transform);
        DisableButton();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void ClickItem()
    {
        if (dropButton.transform.position == transform.position)
        {
            if (dropButton.transform.GetChild(0).gameObject.activeSelf == true)
            {
                DisableButton();
            }
            else
            {
                if (transform.GetComponent<InventoryItem>().item.type == ItemType.placeable)
                {
                    EnableButtonPlace();
                }
                else
                {
                    EnableButtonDrop();
                }
            }
        }
        else
        {
            dropButton.transform.position = transform.position;
            if (transform.GetComponent<InventoryItem>().item.type == ItemType.placeable)
            {
                EnableButtonPlace();
            }
            else
            {
                EnableButtonDrop();
            }
        }
    }

    private void EnableButtonDrop()
    {
        if (dropButton != null)
        {
            ClickToDrop c = dropButton.GetComponent<ClickToDrop>();
            c.clickedItem = transform.GetComponent<InventoryItem>();
            foreach (Transform child in dropButton.transform)
            {
                if (child.name == "BGName")
                {
                    Transform tf = child.transform.GetChild(0);
                    TextMeshProUGUI nameText = tf.ConvertTo<TextMeshProUGUI>();
                    nameText.text = Regex.Replace(transform.GetComponent<InventoryItem>().item.name,
                        "(\\B[A-Z])", " $1");
                    float desiredWidth = nameText.preferredWidth;
                    nameText.rectTransform.sizeDelta = new Vector2(desiredWidth, nameText.rectTransform.sizeDelta.y);
                }
                child.gameObject.SetActive(true);
                if (child.name == "Place") child.gameObject.SetActive(false);
            }
        }
    }

    private void EnableButtonPlace()
    {
        if (dropButton != null)
        {
            ClickToDrop c = dropButton.GetComponent<ClickToDrop>();
            c.clickedItem = transform.GetComponent<InventoryItem>();
            foreach (Transform child in dropButton.transform)
            {
                if (child.name == "BGName")
                {
                    Transform tf = child.transform.GetChild(0);
                    TextMeshProUGUI nameText = tf.ConvertTo<TextMeshProUGUI>();
                    nameText.text = transform.GetComponent<InventoryItem>().item.name;
                    float desiredWidth = nameText.preferredWidth;
                    nameText.rectTransform.sizeDelta = new Vector2(desiredWidth, nameText.rectTransform.sizeDelta.y);
                }
                child.gameObject.SetActive(true);
            }
        }
    }

    public void DisableButton()
    {
        if (dropButton != null)
        {
            foreach (Transform child in dropButton.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void InitialiseItem(Item newItem,int count)
    {
        item = newItem;
        image.sprite = newItem.image;
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
}
