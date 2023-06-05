using UnityEngine;
using System.Collections;
public class BossController : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius;
    [SerializeField] private float actionCooldown = 2f;
    private bool canChangePosition = true;
    private float changePositionCooldown = 5f;
    private float changePositionTimer = 0f;
    private Animator animator;
    private Rigidbody2D rb;
    private float actionCooldownTimer = 0f;
    private bool isChangingPosition = false;
    private bool isPerformingAction = false;
    private bool jumped = false;
    public Transform player;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(FiveSecondLatency());
    }


    private float actionTimer = 0f;
    private float actionInterval = 4f;


    private void Update()
    {
        AnimTrigger();
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }

        actionTimer += Time.deltaTime;

        if (actionTimer >= actionInterval)
        {
            ChooseAction();
            actionTimer = 0f;
        }
    }

    private void AnimTrigger()
    {
        if (!IsGrounded())
        {
            animator.SetTrigger("onAir");
        }
        else
        {
            animator.SetTrigger("onGround");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftArea"))
        {
            FlipRight();
        }
        else if (collision.CompareTag("RightArea"))
        {
            FlipLeft();
        }
    }
    private void FlipLeft()
    {
        Vector3 scale = rb.transform.localScale;
        scale.x = 1f;
        rb.transform.localScale = scale;
    }
    private void FlipRight()
    {
        Vector3 scale = rb.transform.localScale;
        scale.x = -1f;
        rb.transform.localScale = scale;
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
        Debug.Log("meleeAttacked");

        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("BossMelee");
        }
    }

    private void PerformTongueAttack()
    {
        isPerformingAction = true;
        animator.SetTrigger("tongueAttack");
        Debug.Log("tongueattacked");

        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("BossTongue");
        }
    }

    private void ChangePosition()
    {   
        Debug.Log("Jumped");
        Vector3 scale = rb.transform.localScale;
        if (scale.x == -1)
        {
            rb.AddForce(new Vector2(3.5f, 11f), ForceMode2D.Impulse);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("BossJump");
            }
            canChangePosition = false;

        }
        else
        {
            rb.AddForce(new Vector2(-3.5f, 11f), ForceMode2D.Impulse);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.Play("BossJump");
            }
            canChangePosition = false;
        }
        jumped = true;
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private IEnumerator FiveSecondLatency()
    {
        yield return new WaitForSeconds(5f);
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("BossMusic");
        }
    }
}