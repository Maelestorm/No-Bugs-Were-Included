using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("CharacterHurt");
            }
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
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("CharacterHurt");
            }
            healthBar.SetHealth(health);
            Debug.Log("Player health : " + health);

            if (health <= 0f)
            {
                Die();
            }
        }
        if (other.CompareTag("StalagmiteTipCollider"))
        {
            health -= StalagmiteTip.stalagmiteTipDamage;
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("CharacterHurt");
            }
            healthBar.SetHealth(health);
            Debug.Log("Player health : " + health);
            if (health <= 0f)
            {
                Die();
            }
        }
        if (other.CompareTag("bossTongueAttackCollider"))
        {
            health -= BossAttack.bossTongueColliderDamage;
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("CharacterHurt");
            }
            healthBar.SetHealth(health);
            Debug.Log("Player health : " + health);
            if (health <= 0f)
            {
                Die();
            }
        }
        if (other.CompareTag("bossMeleeAttackCollider"))
        {
            health -= BossAttack.bossMeleeColliderDamage;
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("CharacterHurt");
            }
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

        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("CharacterDeath");
        }


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

    private void LoadEndGame()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Stop("ThemeSong");
            audioManager.Stop("BossMusic");
        }
        SceneManager.LoadScene(1);
        audioManager.Play("EndGameMusic");
    }
}
