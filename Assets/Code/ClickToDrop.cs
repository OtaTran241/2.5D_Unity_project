using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class ClickToDrop : MonoBehaviour
{
    public GameObject itemToPickupPf;
    public TextMesh countText;
    public GameObject itemToPlacePf;

    [HideInInspector] public InventoryItem clickedItem;
    private GameObject dropButton;
    private GameObject player;

    private void Start()
    {
        dropButton = GameObject.Find("DropButton");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ClickDrop()
    {
        TurnOff();
        Vector3 dropPos = RandomPos(player.transform.position);
        var go = Instantiate(itemToPickupPf, dropPos, Quaternion.identity);
        go.GetComponent<Pickup>().item = clickedItem.item;
        go.GetComponent<Pickup>().count = clickedItem.count;
        go.GetComponent<SpriteRenderer>().sprite = clickedItem.item.image;
        go.GetComponent<Pickup>().countText = countText;
        Destroy(clickedItem.gameObject);
    }

    public void ClickPlace()
    {
        TurnOff();
        if (clickedItem.item.name == "TreeNut")
        {
            if (CheckLayer())
            {
                var go = Instantiate(itemToPlacePf, clickedItem.transform.position, Quaternion.identity);
                go.GetComponent<MoveWithMouse>().image = clickedItem.item.image;
                go.GetComponent<MoveWithMouse>().StartMoving();
                go.GetComponent<MoveWithMouse>().item = clickedItem.item;
                if (clickedItem.count == 1)
                {
                    Destroy(clickedItem.gameObject);
                }
                else
                {
                    clickedItem.count -= 1;
                    clickedItem.RefreshCount();
                }
            }
        }
        else
        {
            var go = Instantiate(itemToPlacePf, clickedItem.transform.position, Quaternion.identity);
            go.GetComponent<MoveWithMouse>().image = clickedItem.item.image;
            go.GetComponent<MoveWithMouse>().StartMoving();
            go.GetComponent<MoveWithMouse>().item = clickedItem.item;
            Destroy(clickedItem.gameObject);
        }
    }

    private void TurnOff()
    {
        if (dropButton != null)
        {
            foreach (Transform child in dropButton.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public Vector3 RandomPos(Vector3 playerPos)
    {
        float radius = 2f;
        float distance = 1f;
        Vector2 randomOffset = Random.insideUnitCircle.normalized * radius;
        Vector3 randomPosition = new Vector3(randomOffset.x, 0f, randomOffset.y);
        randomPosition += playerPos - new Vector3(0f, 0f, distance);
        return randomPosition;
    }
    private bool CheckLayer()
    {
        bool check = false;
        Ray ray = new Ray(player.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Terrain") check = true;
            else check = false;
        }
        return check;
    }
}
