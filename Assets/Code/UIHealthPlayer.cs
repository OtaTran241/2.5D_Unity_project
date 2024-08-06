using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthPlayer : MonoBehaviour
{
    private HealthManager healthManager;
    public Slider healthBar;
    public TextMeshProUGUI hpText;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        healthManager = player.GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = healthManager.maxHealth;
        healthBar.value = healthManager.currentHealth;
        hpText.text = "HP: " + healthManager.currentHealth + "/" + healthManager.maxHealth;
    }
}
