using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBarScript : MonoBehaviour
{
    public PlayerFoodManager foodManager;
    public Slider foodBar;

    private void Update()
    {
        foodBar.maxValue = foodManager.maxFood;
        foodBar.value = foodManager.currentFood;
    }
}
