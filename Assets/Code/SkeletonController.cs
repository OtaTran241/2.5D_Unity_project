using Pathfinding;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.UI;

public class SkeletonController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] public float minRange;
    [SerializeField] public Transform homePos;
    [SerializeField] private Collider thisC;

    [HideInInspector] public float moveX;
    [HideInInspector] public float moveZ;
    [HideInInspector] public bool isMoving = false;
    private Vector3 home;
    private bool randomMove;
    private Vector3 randomPosition;
    private Collider targetC;
    private Transform target;
    private AIPath enemyAI;
    private AIDestinationSetter playerTarget;
    private Transform rt;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        targetC = target.GetComponent<Collider>();
        enemyAI = GetComponent<AIPath>();
        playerTarget = GetComponent<AIDestinationSetter>();
        home = transform.position;
        StartCoroutine(RandomMove());
        transform.rotation = Quaternion.Euler(new Vector3(45f, 0f, 0f));
        rt = new GameObject().transform;
        enemyAI.maxSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
            {
                if (thisC.bounds.Intersects(targetC.bounds))
                {
                    return;
                }
                else
                {
                    randomMove = false;
                    FollowTarget();
                }
            }
            else if (Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                //homePos.position = home;
                //BackHome();
                randomMove = true;
            }
        }
        else
        {
            //homePos.position = home;
            //BackHome();
            randomMove = true;
        }
        if ((Vector3.Distance(transform.position, randomPosition) < 0.8f))
        {
            isMoving = false;
        }
    }

    public void FollowTarget()
    {
        isMoving = true;
        moveX = target.position.x - transform.position.x;
        moveZ = target.position.z - transform.position.z;
        playerTarget.target = target.transform;
        enemyAI.endReachedDistance = minRange;
    }

    public void BackHome()
    {
        moveX = homePos.position.x - transform.position.x;
        moveZ = homePos.position.z - transform.position.z;
        playerTarget.target = homePos.transform;

        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            isMoving = false;
        }
    }

    private IEnumerator RandomMove()
    {
        while (true)
        {
            if (randomMove)
            {
                float moveRadius = 6f;

                Vector2 randomOffset;

                randomOffset = Random.insideUnitSphere * moveRadius;

                randomPosition = new Vector3(home.x, transform.position.y, home.z) + new Vector3(randomOffset.x, 0f, randomOffset.y);

                rt.position = randomPosition;
                enemyAI.endReachedDistance = 0.8f;
                isMoving = true;
                moveX = randomPosition.x - transform.position.x;
                moveZ = randomPosition.z - transform.position.z;

                playerTarget.target = rt;
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            Vector3 diff = transform.position - other.transform.position;
            transform.position = new Vector3(transform.position.x + diff.x, transform.position.y, transform.position.z + diff.z);
        }
    }
}
