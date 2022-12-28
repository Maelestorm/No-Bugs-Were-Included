using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 3f;
    private float jumpingPower = 4f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;



    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Update()
    {
       horizontal = Input.GetAxisRaw("Horizontal");
       
       Flip();
       
       if (Input.GetButtonDown("Jump") && IsGrounded())
       {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
       }

       if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
       {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
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
            localScale.x*=-1f;
            transform.localScale = localScale;
        }
    }
}
