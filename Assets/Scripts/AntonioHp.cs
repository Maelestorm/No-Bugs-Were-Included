using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntonioHp : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private Animator anim;

    private AntonioMovementScript movementScript;
    private AntonioAttack attackScript;
    private Rigidbody2D rb;
    public HealthBarScript healthBar;

    private Collider2D playerCollider;

    private void Start()
    {
        movementScript = GetComponent<AntonioMovementScript>();
        attackScript = GetComponent<AntonioAttack>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LarvaAttackCollider"))
        {
            health -= LarvaAI.larvaAttackDamage;
            healthBar.SetHealth(health);
            Debug.Log("Player health : " + health);

            if (health <= 0f)
            {
                Die();
            }
        }

        if (other.CompareTag("fLarvaAttackCollider"))
        {
            health -= fLarvaAI.fLarvaAttackDamage;
            healthBar.SetHealth(health);
            Debug.Log("Player health : " + health);

            if (health <= 0f)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        anim.SetTrigger("die");
        // FindObjectOfType<AudioManager>().Play("xxxDeath")

        // Disable movement script
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        // Disable attack script
        if (attackScript != null)
        {
            attackScript.enabled = false;
        }

        // "Disable" Rigidbody2D
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f; // Set gravity scale to zero
        }

        // Disable Collider2D
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }
    }
}
