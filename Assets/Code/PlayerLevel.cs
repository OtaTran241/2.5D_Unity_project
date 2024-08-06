using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int playerLevel = 1;
    public int currentExp;
    [SerializeField] private GameObject targetGameObject;

    private LevelManager level;
    [HideInInspector] public int expNeed;
    [HideInInspector] public int maxEXPNeed;

    void Start()
    {
        level = targetGameObject.GetComponent<LevelManager>();
    }

    void Update()
    {
        expNeed = ExpNeedToLevelUp();
        maxEXPNeed = ExpNeedToLevelUp2();

        if (currentExp >= ExpNeedToLevelUp())
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerLevel += 1;
    }

    public void UpdateExp(int exp)
    {
        currentExp += exp;
    }

    private int ExpNeedToLevelUp()
    {
        return level.expToLevelUp[playerLevel+1];
    }

    private int ExpNeedToLevelUp2()
    {
        return level.expToLevelUp[playerLevel];
    }
}
