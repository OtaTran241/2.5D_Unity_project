using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletonAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;

    private bool isTouching = false;
    private float aS;
    private HealthManager healthManager;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        healthManager = player.GetComponent<HealthManager>();
        aS = attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching)
        {
            attackSpeed -= Time.deltaTime;
            if (attackSpeed <= 0)
            {
                healthManager.TakeDamage(Random.Range(damage-1,damage+1));
                attackSpeed = aS;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            //other.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = false;
            attackSpeed = aS;
        }
    }
}
