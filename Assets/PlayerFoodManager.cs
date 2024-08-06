using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoodManager : MonoBehaviour
{
    public int currentFood;
    public int maxFood;

    public void ReduceFood(int reduce)
    {
        currentFood -= reduce;
    }
}
