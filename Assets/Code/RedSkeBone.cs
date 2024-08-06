using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSkeBone : MonoBehaviour
{
    [SerializeField] public int damage;

    private HealthManager healthManager;

    private void Start()
    {
        StartCoroutine(Destroy(2f));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            healthManager = other.gameObject.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(Random.Range(damage - 1, damage + 1));
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Destroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(transform.gameObject);
    }
}
