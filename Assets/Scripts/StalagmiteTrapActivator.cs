using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalagmiteTrapActivator : MonoBehaviour
{
    public GameObject staglamiteTrap;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = staglamiteTrap.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 2f;
            }
        }
    }
}
