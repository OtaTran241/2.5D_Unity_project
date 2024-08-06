using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanScript : MonoBehaviour
{
    public AstarPath astarPath;
    private void Start()
    {
        InvokeRepeating("Scan", 0f, 300f);
    }

    private void Scan()
    {
        astarPath.Scan();
    }
}
