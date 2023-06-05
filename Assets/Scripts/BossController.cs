using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject leftPoint;
    [SerializeField] private GameObject rightPoint;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float positionChangeCooldown = 5f;
    [SerializeField] private float actionCooldown = 2f;

    private Animator animator;
    private Rigidbody2D rb;

    private float positionChangeTimer = 0f;
    private float actionCooldownTimer = 0f;

    private bool isChangingPosition = false;
    private bool isPerformingAction = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isChangingPosition && !isPerformingAction)
        {
            actionCooldownTimer += Time.deltaTime;

            if (actionCooldownTimer >= actionCooldown)
            {
                ChooseAction();
                actionCooldownTimer = 0f;
            }
        }
        else if (isChangingPosition)
        {
            positionChangeTimer += Time.deltaTime;

            if (positionChangeTimer >= positionChangeCooldown)
            {
                // Change position complete
                isChangingPosition = false;
                positionChangeTimer = 0f;
            }
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
        isChangingPosition = true;
        animator.SetTrigger("jump");
        rb.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
        // Add logic for changing position behavior
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
}