using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICurrentExp : MonoBehaviour
{
    private PlayerLevel playerLevel;
    public Slider currentExp;

    // Start is called before the first frame update
    void Start()
    {
        playerLevel = FindObjectOfType<PlayerLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        currentExp.maxValue = playerLevel.expNeed - playerLevel.maxEXPNeed;
        currentExp.value = playerLevel.currentExp - playerLevel.maxEXPNeed;
    }
}
