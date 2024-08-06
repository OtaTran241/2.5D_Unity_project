using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject targetGameObject;
    public TextMeshProUGUI levelText;
    public int baseExp = 1000;
    public int[] expToLevelUp;

    private int maxLevel =  999;
    private PlayerLevel Level;

    // Start is called before the first frame update
    void Start()
    {
        Level = targetGameObject.GetComponent<PlayerLevel>();
        expToLevelUp = new int[maxLevel];
        expToLevelUp[0] = baseExp;
        for (int i = 1; i < expToLevelUp.Length; i++)
        {
            expToLevelUp[i] = Mathf.FloorToInt(expToLevelUp[i - 1] * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Level: " + Level.playerLevel;
    }
}
