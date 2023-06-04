using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyBugAttack : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject sparkPrefab;
    public static float ladyBugAttackDamage = 5f;
    public float fireRate;
    float nextFire;
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            ShootSlow();
        }
    }
    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            SparkScript sparkScript = sparkPrefab.GetComponent<SparkScript>();
            if (sparkScript != null)
            {
                sparkScript.speed = 20f;
            }
            Instantiate(sparkPrefab, attackPoint.position, attackPoint.rotation);

            animator.SetTrigger("Shoot");
            FindObjectOfType<AudioManager>().Play("LadybugSparkSFX");
        }
        }
        void ShootSlow()
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                SparkScript sparkScript = sparkPrefab.GetComponent<SparkScript>();
                if (sparkScript != null)
                {
                    sparkScript.speed = 5f;
                }
                Instantiate(sparkPrefab, attackPoint.position, attackPoint.rotation);

                animator.SetTrigger("Shoot");
                FindObjectOfType<AudioManager>().Play("LadybugSparkSFX");
            }
        }
    }

