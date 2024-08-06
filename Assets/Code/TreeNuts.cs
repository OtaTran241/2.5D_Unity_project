using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TreeNuts : MonoBehaviour
{
    public GameObject[] treePf;
    public int timeToGrow;

    private int plantingDate;
    private TextMeshProUGUI dayText;

    void Start()
    {
        dayText = GameObject.Find("DayText").ConvertTo<TextMeshProUGUI>();
        string text = dayText.text;
        int colonIndex = text.IndexOf(":");
        string characterAfterColon = text.Substring(colonIndex + 1);
        int.TryParse(characterAfterColon, out plantingDate);
    }

    // Update is called once per frame
    void Update()
    {
        dayText = GameObject.Find("DayText").ConvertTo<TextMeshProUGUI>();
        string text = dayText.text;
        int colonIndex = text.IndexOf(":");
        string characterAfterColon = text.Substring(colonIndex + 1);
        int currentTime;
        int.TryParse(characterAfterColon, out currentTime);
        if (currentTime == (plantingDate + timeToGrow))
        {
            bool check = Instantiate(treePf[Random.Range(0,treePf.Length)], transform.position, Quaternion.identity);
            
            if (check) 
            {
                Destroy(transform.gameObject);
            }
        }
    }
}
