using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject targetHandObject;
    public LayerMask terrainMask;
    public Rigidbody rb;
    public SpriteRenderer sr;

    [HideInInspector] public Vector3 moveD;
    [HideInInspector] public float moveX;
    [HideInInspector] public float moveZ;
    [HideInInspector] public float Horizontal;
    [HideInInspector] public float Vertical;
    [HideInInspector] public bool isAttacking;
    private PlayerAnimation ac;
    private PlayerHandAnimation an;
    private SpriteRenderer hand;
    private PlayerFoodManager fM;
    private int pSpeed;
    //private GameObject mainCam;
    //private Quaternion r;
    //private bool isSpacePressed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //mainCam = GameObject.Find("PlayerCamera");
        ac = targetObject.GetComponent<PlayerAnimation>();
        an = targetHandObject.GetComponent<PlayerHandAnimation>();
        hand = targetHandObject.GetComponent<SpriteRenderer>();
        fM = GetComponent<PlayerFoodManager>();
        pSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (fM.currentFood < 5)
        {
            speed = pSpeed * 50 / 100;
        }
        else
        {
            speed = pSpeed;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    isSpacePressed = true;
        //}
        //else
        //{
        //    isSpacePressed = false;
        //}

        //if (isSpacePressed)
        //{
        //    r = Quaternion.Euler(new Vector3(45, 180, 0));
        //    mainCam.transform.rotation = r;
        //}
        //else
        //{
        //    r = Quaternion.Euler(new Vector3(45, 0, 0));
        //    mainCam.transform.rotation = r;
        //}

        moveD = new Vector3(x, 0, y);
        if (rb.isKinematic == false)
        {
            Vector3 moveDir = moveD * speed;
            Move(moveDir);
        }
        Physics.gravity = new Vector3(0, gravity, 0);

        moveX = rb.velocity.x;
        moveZ = rb.velocity.z;

        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        if (ac.isAttacking)
        {
            rb.velocity = Vector3.zero;
            hand.sprite = null;
        }
        if (an.isMining)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void Move(Vector3 moveDir)
    {
        rb.velocity = moveDir;
    }
}
