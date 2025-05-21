using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    public float jumpPower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);    
        }
    }
}
