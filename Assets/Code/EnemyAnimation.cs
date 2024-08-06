using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator an;
    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private GameObject targetObject;

    private float moveX;
    private float moveZ;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SkeletonController move = targetObject.GetComponent<SkeletonController>();
        an.SetBool("isMoving", move.isMoving);
        an.SetFloat("moveX", move.moveX);
        an.SetFloat("moveZ", move.moveZ);

        HealthManager health = targetObject.GetComponent<HealthManager>();

        if (health.flashActive)
        {
            if (health.flashCounter > health.flashLength * .99f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (health.flashCounter > health.flashLength * .82f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (health.flashCounter > health.flashLength * .66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (health.flashCounter > health.flashLength * .49f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (health.flashCounter > health.flashLength * .33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (health.flashCounter > health.flashLength * .16f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (health.flashCounter > 0f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                health.flashActive = false;
            }
            health.flashCounter -= Time.deltaTime;
        }
    }
}
