using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(45f, 0f, 0f));
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!rb.isKinematic)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            {
                rb.isKinematic = true;
            }
        }
    }
}
