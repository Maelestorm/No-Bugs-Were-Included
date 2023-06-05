using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleAttack : MonoBehaviour
{
    //private bool isAttacking = false;
    //private float attackCooldown = 2.0f;
    //private float timeSinceLastAttack = 0.0f;
    public static float beetleAttackDamage = 15;

    [SerializeField] private CircleCollider2D meleeCollider;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;

    private bool isAttacking = false;


    private void Start()
    {
        meleeCollider.gameObject.SetActive(false);
    }

    private void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true; // Set the isAttacking flag
            anim.SetTrigger("isAttacking");
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("BettleAttack");
            }
        }
    }

    public void MeleeColliderActivate()
    {
        meleeCollider.gameObject.SetActive(true);
    }
    public void MeleeColliderDeActivate()
    {
        meleeCollider.gameObject.SetActive(false);
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

}
