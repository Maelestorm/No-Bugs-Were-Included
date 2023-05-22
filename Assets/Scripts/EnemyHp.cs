using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private float health;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AntonioMeleeCollider"))
        {
            health -= AntonioAttack.antonioAttackDamage;
            Debug.Log("Enemy health: " + health);

            if (health <= 0f)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
