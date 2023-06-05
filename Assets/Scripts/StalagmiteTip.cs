using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalagmiteTip : MonoBehaviour
{
    public static float stalagmiteTipDamage = 20;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
