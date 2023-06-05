using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private Animator anim;

    private Rigidbody2D rb;
    private Collider2D larvaCollider;
    private LarvaAI larvaAIScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AntonioMeleeCollider"))
        {
            health -= AntonioAttack.antonioAttackDamage;
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("LarvaHurt");
            }
            Debug.Log("Enemy health: " + health);

            if (health <= 0f)
            {
                Die();
            }
        }
        if (other.CompareTag("LadyBugSparkCollider"))
        {
            health -= LadyBugAttack.ladyBugAttackDamage;
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("LarvaHurt");
            }
            if (health <= 0f)
            {
                Die();
            }
        }
        if (other.CompareTag("BeetleMeleeCollider"))
        {
            health -= BeetleAttack.beetleAttackDamage;
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("LarvaHurt");
            }
            if (health <= 0f)
            {
                Die();
            }
        }
    }


    private void Die()
    {
        anim.SetTrigger("die");
        FindObjectOfType<AudioManager>().Play("LarvaDeath");

        // Disable movement script
        if (larvaAIScript != null)
        {
            larvaAIScript.enabled = false;
        }


        // "Disable" Rigidbody2D
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f; // Set gravity scale to zero
        }

        // Disable Collider2D
        if (larvaCollider != null)
        {
            larvaCollider.enabled = false;
        }
    }

    private void RemoveObjectFromScene()
    {
        Destroy(gameObject);
    }
}
