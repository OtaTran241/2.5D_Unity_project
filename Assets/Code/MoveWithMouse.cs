using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveWithMouse : MonoBehaviour
{
    public Sprite image;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject[] itemPf;
    public Item item;

    private bool isMoving = false;
    private Vector3 pos;
    private RaycastHit hit;
    private SpriteRenderer spriteRenderer;
    private GameObject player;
    private Rigidbody rb;
    private float detectionRadius = 5f;
    private LineRenderer lineRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = image;
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        lineRenderer = player.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 380;
        DrawCircle();
    }

    private void OnDestroy()
    {
        rb.isKinematic = false;
        ClearCircle();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            Vector3 mPos = hit.point;
            pos = new Vector3(mPos.x - 0.5f, mPos.y, mPos.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Place();
        }
        if (Input.GetMouseButtonDown(1))
        {
            InventoryManager playerI = player.GetComponentInChildren<InventoryManager>();
            bool result = playerI.AddItems(item, 1);
            if (result)
            {
                Destroy(transform.gameObject);
            }
        }
        ChangeColor();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = pos;
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    private void ChangeColor()
    {
        if (CheckPlacePos() && CheckForCollisions())
        {
            spriteRenderer.color = new Color(0, 1, 0, 0.5f);
        }
        else
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
        }
    }

    private void Place()
    {
        if (CheckPlacePos() && CheckForCollisions())
        {
            for (int i = 0; i < itemPf.Length; i++)
            {
                if (itemPf[i].name == item.name)
                {
                    Instantiate(itemPf[i], pos, Quaternion.Euler(new Vector3(40f, 0f, 0f)));
                    Destroy(transform.gameObject);
                    break;
                }
            }
        }
    }

    private bool CheckPlacePos()
    {
        float distance = Vector3.Distance(player.transform.position, pos);

        if (distance <= detectionRadius) return true;
        else
        {
            return false;
        }
    }

    private void DrawCircle()
    {
        for (int i = 0; i < 380; i++)
        {
            float angle = i * Mathf.PI / 180;
            float x = Mathf.Cos(angle) * detectionRadius;
            float z = Mathf.Sin(angle) * detectionRadius;

            Vector3 position = new Vector3(x, -0.5f, z) + player.transform.position;
            lineRenderer.SetPosition(i, position);
        }
    }

    private void ClearCircle()
    {
        lineRenderer.positionCount = 0;
    }

    private bool CheckForCollisions()
    {
        bool check = false;
        Collider[] colliders = Physics.OverlapSphere(pos, 1f);
        foreach (Collider col in colliders)
        {
            Vector3 tranformPos = new Vector3(col.transform.position.x - 0.4f, col.transform.position.y, col.transform.position.z);
            if (colliders.Length == 1 && colliders[0].gameObject.tag == "Terrain")
            {
                check = true;
            }
            else if (Vector3.Distance(tranformPos, pos) >= 1.5f)
            {
                check = true;
            }
            else
            {
                check = false;
            }
        }
        return check;
    }
}
