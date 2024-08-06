using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject floatingText;
    public int currentHealth;
    public int maxHealth;
    public float flashLength;

    private int damageToTake;
    [HideInInspector] public float flashCounter;
    [HideInInspector] public bool flashActive;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        flashActive = true;
        flashCounter = flashLength;
        damageToTake = damage;

        if (floatingText != null)
        {
            ShowDamage();
        }

        if (currentHealth <= 0)
        {
            Destroy(transform.gameObject);
        }
    }

    private void ShowDamage()
    {
        var go = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = "-"+damageToTake.ToString();
    }
}
