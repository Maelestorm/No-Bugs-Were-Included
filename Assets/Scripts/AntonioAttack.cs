using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntonioAttack : MonoBehaviour
{
    //private bool isAttacking = false;
    //private float attackCooldown = 2.0f;
    //private float timeSinceLastAttack = 0.0f;
    public static float antonioAttackDamage = 10;

    [SerializeField] private CircleCollider2D meleeCollider;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;

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
            anim.SetTrigger("isAttacking");
            Debug.Log("attacked");
            FindObjectOfType<AudioManager>().Play("AntonioAttack");
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
}
