using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyBugAttack : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject sparkPrefab;
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
    }
    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(sparkPrefab, attackPoint.position, attackPoint.rotation);
            animator.SetTrigger("Shoot");
        }
    }

}
