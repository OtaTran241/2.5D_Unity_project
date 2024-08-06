using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform character;

    void Update()
    {
        if (character != null)
        {
            Vector3 directionToCharacter = character.position - transform.position;

            transform.Translate(directionToCharacter.normalized);
        }
    }
}
