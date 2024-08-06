using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator an;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private SpriteRenderer handSprite;
    [SerializeField] private GameObject targetObject;

    private float attackTime = 0.3f;
    private float attackCounter = 0.3f;
    [HideInInspector] public bool isAttacking;
    private float lastPressTime;
    private PlayerController move;
    private Sprite hand;

    // Start is called before the first frame update
    void Start()
    {
        move = targetObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (move != null)
        {
            an.SetFloat("moveX", move.moveX);
            an.SetFloat("moveZ", move.moveZ);

            if (move.Horizontal == 1 || move.Horizontal == -1 || move.Vertical == 1 || move.Vertical == -1)
            {
                an.SetFloat("lastMoveX", move.Horizontal);
                an.SetFloat("lastMoveZ", move.Vertical);
            }
        }

        Hurt();

        if (isAttacking)
        {
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                an.SetBool("isAttacking", false);
                handSprite.sprite = hand;
                isAttacking = false;
            }
        }

        InventoryManager im = targetObject.GetComponentInChildren<InventoryManager>();
        InventorySlot slot = im.currentlySelectedSlot;
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (handSprite.sprite != null)
        {
            if (Input.GetMouseButtonDown(0) && itemInSlot.item.type == ItemType.sword)
            {
                if (Time.time - lastPressTime >= attackTime)
                {
                    attackCounter = attackTime;
                    an.SetBool("isAttacking", true);
                    hand = handSprite.sprite;
                    handSprite.sprite = null;
                    isAttacking = true;
                    lastPressTime = Time.time;
                }
            }
        }
    }
    public void Hurt()
    {
        HealthManager health = targetObject.GetComponent<HealthManager>();

        if (health.flashActive)
        {
            if (health.flashCounter > health.flashLength * .99f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (health.flashCounter > health.flashLength * .82f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (health.flashCounter > health.flashLength * .66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (health.flashCounter > health.flashLength * .49f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (health.flashCounter > health.flashLength * .33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (health.flashCounter > health.flashLength * .16f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (health.flashCounter > 0f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                health.flashActive = false;
            }
            health.flashCounter -= Time.deltaTime;
        }
    }
}
