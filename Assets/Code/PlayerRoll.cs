using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerRoll : MonoBehaviour
{
    public float rollSpeed;
    public float rollTime;
    public GameObject playerModel;
    public ParticleSystem moveParticle;

    private PlayerController playerController;
    private PlayerFoodManager playerFoodManager;
    private Animator an;
    private bool isRolling;
    private float rollCounter;
    private float timeBetweenRolls = 2f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerFoodManager = GetComponent<PlayerFoodManager>();
        an = playerModel.GetComponent<Animator>();
        rollCounter = rollTime;
        moveParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenRolls -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (timeBetweenRolls <= 0 && playerFoodManager.currentFood > 5)
            {
                StartCoroutine(Roll());
                timeBetweenRolls = 2f;
            }
        }

        if (isRolling)
        {
            rollTime -= Time.deltaTime;
            if (rollTime <= 0)
            {
                an.SetBool("isRolling", false);
                rollTime = rollCounter;
                moveParticle.Stop();
                isRolling = false;
            }
        }
    }

    private IEnumerator Roll()
    {
        float startTime = Time.time;

        while (Time.time < startTime + rollTime)
        {
            an.SetBool("isRolling", true);
            isRolling = true;
            moveParticle.Play();
            playerFoodManager.ReduceFood(5);
            playerController.Move(playerController.moveD * rollSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
