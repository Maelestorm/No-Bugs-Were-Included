using System.Collections;
using UnityEngine;

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

        // Check if the Beetle can charge
        bool canCharge = IsGrounded() && !beetleAttack.IsAttacking();

        float input = Input.GetAxisRaw("Horizontal");

        if (canCharge && input != 0 && canDash)
        {
            anim.SetTrigger("chargeStart");
            StartCoroutine(Dash(input));
        }

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Check for idling or running
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

        // Flipping
        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
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

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            anim.SetTrigger("takeOff");
            FindObjectOfType<AudioManager>().Play("Jump");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger("chargeStart");
            StartCoroutine(Dash(rb.velocity.x));
        }

        if (isJumping && Input.GetKey(KeyCode.Space))
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

        if (IsGrounded())
        {
            anim.SetBool("inAir", false);
        }
        else
        {
            anim.SetBool("inAir", true);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private IEnumerator Dash(float direction)
    {
        canDash = false;
        isDashing = true;

        float dashDirection = Mathf.Sign(direction);

        rb.velocity = new Vector2(direction * dashingPower, dashingHeight);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;

        anim.SetTrigger("chargeEnd");

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
