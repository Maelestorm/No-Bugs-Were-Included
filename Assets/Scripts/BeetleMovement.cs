using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class BeetleMovement : MonoBehaviour
{
    private float speed = 4f;

    private float jumpForce = 5f;
    private bool isJumping;
    private float jumpTimeCounter;
    [SerializeField] private float jumpTime;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 3f;
    [SerializeField] private float dashingHeight = 0f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 5f;


    private BeetleAttack beetleAttack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        beetleAttack = GetComponent<BeetleAttack>();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        // Check if the Beetle is attacking
        bool isAttacking = beetleAttack.IsAttacking();

        // Check if the Beetle can charge
        bool canCharge = IsGrounded() && !isAttacking;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            anim.SetTrigger("takeOff");
            FindObjectOfType<AudioManager>().Play("Jump");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (IsGrounded())
        {
            anim.SetBool("inAir", false);
        }
        else
        {
            anim.SetBool("inAir", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Checking for idling or running
        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }



        //flipping
        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (canCharge && Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            anim.SetTrigger("chargeStart");
            StartCoroutine(Dash());
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }



    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        rb.velocity = new Vector2(transform.localScale.x * dashingPower, transform.localScale.y * dashingHeight);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;

        anim.SetTrigger("chargeEnd"); // Trigger chargeEnd parameter

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}