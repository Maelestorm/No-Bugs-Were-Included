using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadybugHp : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private Animator anim;

    private LadyBugMovementScript movementScript;
    private LadyBugAttack attackScript;

    private Rigidbody2D rb;
    private Collider2D playerCollider;

    private void Start()
    {
        movementScript = GetComponent<LadyBugMovementScript>();
        attackScript = GetComponent<LadyBugAttack>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LarvaAttackCollider"))
        {
            health -= LarvaAI.larvaAttackDamage;
            Debug.Log("Player health : " + health);

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
