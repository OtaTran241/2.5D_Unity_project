using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedSkeAttack : MonoBehaviour
{
    [SerializeField] private GameObject BonePrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float boneSpeed;
    [SerializeField] private float shrink = 0.1f;

    private float attackTime;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        attackTime = attackSpeed;
    }

    private void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        attackSpeed -= Time.deltaTime;

        if (attackSpeed > 0) return;

        attackSpeed = attackTime;

        SkeletonController sc = GetComponent<SkeletonController>();
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= sc.minRange)
        {
            GameObject BoneObj = Instantiate(BonePrefab, transform.position, Quaternion.identity);
            Rigidbody BoneRG = BoneObj.GetComponent<Rigidbody>();
            Vector3 moveDirection = (player.position - BoneObj.transform.position).normalized;

            BoneRG.velocity = moveDirection * boneSpeed;

            Destroy(BoneObj, 5f);

            Shrink();
        }
    }

    public void Shrink()
    {
        if (transform.localScale.x > 0.3)
        {
            transform.localScale -= new Vector3(shrink, shrink, shrink);
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }
}
