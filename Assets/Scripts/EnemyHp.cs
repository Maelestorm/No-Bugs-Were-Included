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
        //if (other.CompareTag("LadybugProjectileCollider"))
        //{
        //    health -= LadybugAttack.ladybugAttackDamage;
        //
        //    if (health <= 0f)
        //    {
        //        Die();
        //    }
        //}
        //if (other.CompareTag("beetleMeleeCollider"))
        //{
        //    health -= BeetleAttack.beetleAttackDamage;
        //
        //    if (health <= 0f)
        //    {
        //        Die();
        //    }
        //}
    }

    private void Die()
    {
        //death animation calls
        //death sound queue calls
        Destroy(gameObject);
    }
}
