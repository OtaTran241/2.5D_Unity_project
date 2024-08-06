using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAnimaton : MonoBehaviour
{
    [SerializeField] private Animator an;
    [SerializeField] private SpriteRenderer bearSprite;
    [SerializeField] private GameObject targetObject;

    //private float attackTime = 0.3f;
    //private float attackCounter = 0.3f;
    [HideInInspector] public bool isAttacking;
    private float lastPressTime;
    private BearController move;
    //private Sprite hand;

    // Start is called before the first frame update
    void Start()
    {
        move = targetObject.GetComponent<BearController>();
    }

    // Update is called once per frame
    void Update()
    {
        an.SetBool("isMoving", move.isMoving);
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

        //if (isAttacking)
        //{
        //    attackCounter -= Time.deltaTime;
        //    if (attackCounter <= 0)
        //    {
        //        an.SetBool("isAttacking", false);
        //        handSprite.sprite = hand;
        //        isAttacking = false;
        //    }
        //}
    }
    public void Hurt()
    {
        HealthManager health = targetObject.GetComponent<HealthManager>();

        if (health.flashActive)
        {
            if (health.flashCounter > health.flashLength * .99f)
            {
                bearSprite.color = new Color(bearSprite.color.r, bearSprite.color.g, bearSprite.color.b, 0f);
            }
            else if (health.flashCounter > health.flashLength * .82f)
            {
                bearSprite.color = new Color(bearSprite.color.r, bearSprite.color.g, bearSprite.color.b, 1f);
            }
            else if (health.flashCounter > health.flashLength * .66f)
            {
                bearSprite.color = new Color(bearSprite.color.r, bearSprite.color.g, bearSprite.color.b, 0f);
            }
            else if (health.flashCounter > health.flashLength * .49f)
            {
                bearSprite.color = new Color(bearSprite.color.r, bearSprite.color.g, bearSprite.color.b, 1f);
            }
            else if (health.flashCounter > health.flashLength * .33f)
            {
                bearSprite.color = new Color(bearSprite.color.r, bearSprite.color.g, bearSprite.color.b, 0f);
            }
            else if (health.flashCounter > health.flashLength * .16f)
            {
                bearSprite.color = new Color(bearSprite.color.r, bearSprite.color.g, bearSprite.color.b, 1f);
            }
            else if (health.flashCounter > 0f)
            {
                bearSprite.color = new Color(bearSprite.color.r, bearSprite.color.g, bearSprite.color.b, 0f);
            }
            else
            {
                bearSprite.color = new Color(bearSprite.color.r, bearSprite.color.g, bearSprite.color.b, 1f);
                health.flashActive = false;
            }
            health.flashCounter -= Time.deltaTime;
        }
    }
}
