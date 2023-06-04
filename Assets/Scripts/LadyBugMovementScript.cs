using System.Collections.Generic;
using UnityEngine;

public class LadyBugMovementScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 4f;
    private float jumpForce = 5f;
    public static bool isGrounded;
    private bool isJumping;
    private bool isRising;
    private bool isFalling;
    private float jumpTimeCounter;
    [SerializeField] private float jumpTime;
    private bool inAir;  // new variable to track if the character is in air

    private Rigidbody2D controller;
    private Animator anim;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        controller = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        anim.SetFloat("vertVelocity", controller.velocity.y);
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift) && !isGrounded && controller.velocity.y < 0)
        {
            controller.gravityScale = 0.1f;
            anim.SetBool("gliding", true);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("LadybugWingFlap");
            }

        }
        else
        {
            controller.gravityScale = 1.0f;
            anim.SetBool("gliding", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            anim.SetTrigger("takeOf");
            FindObjectOfType<AudioManager>().Play("Jump");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            controller.velocity = Vector2.up * jumpForce;
        }

        if (IsGrounded())
        {
            anim.SetBool("inAir", false);
            isJumping = false;  // set isJumping to false when grounded
            inAir = false;  // set inAir to false when grounded
        }
        else
        {
            anim.SetBool("inAir", true);
            inAir = true;  // set inAir to true when not grounded
        }

        // new code to prevent jumping mid-air
        if (inAir && isJumping)
        {
            isJumping = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isJumping && !inAir)  // modified condition to check if not in air
        {
            if (jumpTimeCounter > 0)
            {
                controller.velocity = Vector2.up * jumpForce;
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
        controller.velocity = new Vector2(moveInput * speed, controller.velocity.y);
        if (!Mathf.Approximately(moveInput, 0))
        {
            anim.SetBool("isRunning", true);
            if (isGrounded)
            {
                AudioManager audioManager = FindObjectOfType<AudioManager>();
                if (audioManager != null)
                {
                    audioManager.Play("FootstepGrass");
                }
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            controller.velocity = new Vector2(0, controller.velocity.y);
        }

        //flipping the character's sprite
        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        //handling the jump animation phases
        if (controller.velocity.y > 0 && !isGrounded)
        {
            if (!isJumping)
            {
                isJumping = true;
                isRising = true;
            }
        }
        else if (controller.velocity.y < 0 && !isGrounded)
        {
            if (isRising)
            {
                isRising = false;
                isFalling = true;
            }
        }
        else if (isGrounded)
        {
            if (isFalling)
            {
                isFalling = false;
            }
        }


    }

    private bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        return isGrounded;
    }

}