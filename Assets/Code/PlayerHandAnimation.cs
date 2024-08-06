using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandAnimation : MonoBehaviour
{
    [SerializeField] private Animator an;
    [SerializeField] private SpriteRenderer handSprite;
    [SerializeField] private GameObject targetObject;

    private float mineTime = 0.3f;
    private float mineCounter = 0.3f;
    [HideInInspector] public bool isMining;
    private float lastPressTime;
    private PlayerController move;

    void Start()
    {
        move = targetObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (move != null)
        {
            if (move.Horizontal == 1 || move.Horizontal == -1 || move.Vertical == 1 || move.Vertical == -1)
            {
                an.SetFloat("lastMoveX", move.Horizontal);
                an.SetFloat("lastMoveZ", move.Vertical);
            }
        }

        if (isMining)
        {
            mineCounter -= Time.deltaTime;
            if (mineCounter <= 0)
            {
                an.SetBool("isMining", false);
                isMining = false;
            }
        }

        InventoryManager im = targetObject.GetComponentInChildren<InventoryManager>();
        InventorySlot slot = im.currentlySelectedSlot;
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (handSprite.sprite != null)
        {
            if (Input.GetMouseButtonDown(0) && itemInSlot.item.type == ItemType.tool)
            {
                if (Time.time - lastPressTime >= mineTime)
                {
                    mineCounter = mineTime;
                    an.SetBool("isMining", true);
                    isMining = true;
                    lastPressTime = Time.time;
                }
            }
        }
    }
}
