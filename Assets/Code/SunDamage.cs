using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SunDamage : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private HealthManager healthManager;
    private float countdown = 3f;

    private int hour;
    // Start is called before the first frame update
    void Start()
    {
        timeText = GameObject.Find("TimeText").ConvertTo<TextMeshProUGUI>();
        healthManager = transform.GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        string h = timeText.text.Substring(0, 2);
        int.TryParse(h, out hour);

        if (hour <= 18 && hour >= 6)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                healthManager.TakeDamage(Random.Range(3,5));
                countdown = 3;
            }
        }
    }
}
