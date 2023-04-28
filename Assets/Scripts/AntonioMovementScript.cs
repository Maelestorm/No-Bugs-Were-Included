using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class AntonioMovementScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 4f;
    private float jumpingPower = 5f;
    private bool isFacingRight = true;
    public Animator animator;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingHeight = 2.5f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;


    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (horizontal != 0)
            animator.SetFloat("antSpeed", speed);
        else
            animator.SetFloat("antSpeed", 0);
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetBool("antJump", true); // set antJump to true when jumping
            Debug.Log("setbool set to true");
        }


        if (IsGrounded())
        {
            animator.SetBool("antJump", false); // set antJump to false when on the ground
            Debug.Log("setbool set to false");
        }
        else
        {
            animator.SetBool("antJump", true); // set antJump to true when in the air
        }



        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, transform.localScale.y * dashingHeight);
        tr.emitting= true;

        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale=originalGravity;
        isDashing= false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash= true;
    }
}