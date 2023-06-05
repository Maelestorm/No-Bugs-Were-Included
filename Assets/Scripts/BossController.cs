using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject leftPoint;
    [SerializeField] private GameObject rightPoint;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float actionCooldown = 2f;
    private bool canChangePosition = true;
    private float changePositionCooldown = 10f; // Cooldown duration in seconds
    private float changePositionTimer = 0f;
    private Animator animator;
    private Rigidbody2D rb;
    private bool facingRight = true;

    private float actionCooldownTimer = 0f;

    private bool isChangingPosition = false;
    private bool isPerformingAction = false;
    private bool Jumped = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (!canChangePosition)
        {
            // Update the change position timer
            changePositionTimer += Time.deltaTime;

            // Check if the cooldown has finished
            if (changePositionTimer >= changePositionCooldown)
            {
                canChangePosition = true;
                changePositionTimer = 0f;
            }
        }

        if (IsGrounded() && canChangePosition)
        {
            ChangePosition();
        }
    }

    private void ChooseAction()
    {
        int action = Random.Range(0, 3);

        switch (action)
        {
            case 0:
                PerformMeleeAttack();
                break;
            case 1:
                PerformTongueAttack();
                break;
            case 2:
                ChangePosition();
                break;
        }
    }

    private void PerformMeleeAttack()
    {
        isPerformingAction = true;
        animator.SetTrigger("meleeAttack");
        // Add logic for melee attack behavior
    }

    private void PerformTongueAttack()
    {
        isPerformingAction = true;
        animator.SetTrigger("tongueAttack");
        // Add logic for tongue attack behavior
    }

    private void ChangePosition()
    {
        if (facingRight)
        {
            rb.AddForce(new Vector2(9f, 9f), ForceMode2D.Impulse);
            animator.SetTrigger("jump");
            // Add logic for changing position behavior
            Jumped = true;

            // Start the cooldown
            canChangePosition = false;
            Vector3 scale = rb.transform.localScale;
            scale.x = 1f;
            rb.transform.localScale = scale;
            facingRight = false;
        }
        else
        {
            rb.AddForce(new Vector2(-9f, 9f), ForceMode2D.Impulse);
            animator.SetTrigger("jump");
            // Add logic for changing position behavior
            Jumped = true;

            // Start the cooldown
            canChangePosition = false;
            Vector3 scale = rb.transform.localScale;
            scale.x = -1f;
            rb.transform.localScale = scale;
            facingRight = true;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
}