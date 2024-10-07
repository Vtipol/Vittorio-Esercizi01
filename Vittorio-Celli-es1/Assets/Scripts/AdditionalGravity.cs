using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalGravity : MonoBehaviour
{
    public float additionalGravity = 9.81f; 
    private Rigidbody rb; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ApplyGravity(); 
    }

    private void ApplyGravity()
    {
        if (rb != null)
        {
            Vector3 gravityForce = Vector3.down * additionalGravity;

            rb.AddForce(gravityForce, ForceMode.Acceleration);
        }
    }
}
