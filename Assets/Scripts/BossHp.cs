using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHp : MonoBehaviour
{
    [SerializeField] private float health = 500;
    [SerializeField] private Animator anim;

    private Rigidbody2D rb;
    private Collider2D bossCollider;
    private BossController bossController;
    public HealthBarScript bossHealthBar;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AntonioMeleeCollider"))
        {
            health -= AntonioAttack.antonioAttackDamage;
            bossHealthBar.SetHealth(health);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("BossHurt");
            }
            Debug.Log("Boss health: " + health);
            if (health <= 0f)
            {
                Die();
            }
        }
        if (other.CompareTag("LadyBugSparkCollider"))
        {
            health -= LadyBugAttack.ladyBugAttackDamage;
            bossHealthBar.SetHealth(health);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("BossHurt");
            }
            Debug.Log("Boss health: " + health);
            if (health <= 0f)
            {
                Die();
            }
        }
        if (other.CompareTag("BeetleMeleeCollider"))
        {
            health -= BeetleAttack.beetleAttackDamage;
            bossHealthBar.SetHealth(health);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("BossHurt");
            }
            Debug.Log("Boss health: " + health);

            if (health <= 0f)
            {
                Die();
            }
        }
    }


    private void Die()
    {
        anim.SetTrigger("die");
        FindObjectOfType<AudioManager>().Play("BossDeath");

        // Disable movement script
        if (bossController != null)
        {
            bossController.enabled = false;
        }


        // "Disable" Rigidbody2D
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f; // Set gravity scale to zero
        }

        // Disable Collider2D
        if (bossCollider != null)
        {
            bossCollider.enabled = false;
        }
    }

    private void RemoveObjectFromScene()
    {
        Destroy(gameObject);
    }

    private void GoToVictoryScene()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Stop("BossMusic");
        }
        SceneManager.LoadScene(8);
        if (audioManager != null)
        {
            audioManager.Play("MenuMusic");
        }
    }
}
