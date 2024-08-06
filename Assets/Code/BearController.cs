using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BearController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] public float minRange;
    [SerializeField] public Transform homePos;
    [SerializeField] private Collider thisC;

    [HideInInspector] public float moveX;
    [HideInInspector] public float moveZ;
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public float Horizontal;
    [HideInInspector] public float Vertical;

    private Vector3 home;
    private bool randomMove;
    private Vector3 randomPosition;
    private Collider targetC;
    private Transform target;
    private Vector3 lastPosition;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        targetC = target.GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
        StartCoroutine(RandomMove());
        lastPosition = transform.position;
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
                    homePos.position = home;
                    randomMove = true;
                }
            }
            else if (Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                homePos.position = home;
                randomMove = true;
            }
        }
        else
        {
            homePos.position = home;
            randomMove = true;
        }
        if ((Vector2.Distance(transform.position, randomPosition) < 0.8f))
        {
            isMoving = false;
        }
    }

    public void FollowTarget()
    {
        isMoving = true;
        moveX = randomPosition.x - transform.position.x;
        moveZ = randomPosition.z - transform.position.z;

        Horizontal = moveX;
        Vertical = moveZ;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
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

                while (Vector3.Distance(transform.position, randomPosition) > 0.8f)
                {
                    isMoving = true;
                    moveX = randomPosition.x - transform.position.x;
                    moveZ = randomPosition.z - transform.position.z;

                    Vector3 moveDirection = lastPosition - randomPosition;

                    lastPosition = randomPosition;
                    Horizontal = (int)System.Math.Sign(moveDirection.x);
                    Vertical = (int)System.Math.Sign(moveDirection.z);

                    transform.position = Vector3.MoveTowards(transform.position, randomPosition, speed * Time.deltaTime);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(2f);

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
