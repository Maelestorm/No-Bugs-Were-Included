using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class AntonioMovementScript : MonoBehaviour
{
    private float speed = 4f;

    [SerializeField] private float jumpForce;
    private bool isJumping;
    private float jumpTimeCounter;
    [SerializeField] private float jumpTime;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {


        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);


        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            anim.SetTrigger("takeOf");
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
        else if (moveInput != 0 && IsGrounded())
        {
            anim.SetBool("isRunning", true);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("FootstepGrass");
            }
        }
        // else
        // {
        //     anim.SetBool("isRunning", true);
        // }



        //flipping
        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
}