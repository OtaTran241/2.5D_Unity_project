using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float destroyTime = 2f;
    public Vector3 offSet = new Vector3(0,1,0);
    public Vector3 RandomIntensity = new Vector3(0.5f, 0, 0);

    private void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offSet;
        transform.localPosition += new Vector3(Random.Range(-RandomIntensity.x, RandomIntensity.x),
                                                Random.Range(-RandomIntensity.y, RandomIntensity.y),
                                                Random.Range(-RandomIntensity.z, RandomIntensity.z));
    }
}
