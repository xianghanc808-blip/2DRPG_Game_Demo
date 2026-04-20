using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Torch_Movement : MonoBehaviour
{
    [Header("TorchProperty")]
    public float speed;
    public int facingDirection;
    public float attackCoolDown;

    [Header("FindPlayerCondition")]
    public Transform detectionPoint;
    public LayerMask playerLayer;
    public int playerDetectRange;
    public int attackRange;


    private float attackCoolDownTimer = 0.1f;
    private Rigidbody2D rb;
    private Transform playerTransform;
    private Animator anim;
    private EnemyState enemyState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(enemyState != EnemyState.KnockBack)
        {
            if (attackCoolDownTimer > 0)
            {
                attackCoolDownTimer -= Time.deltaTime;
            }

            FindPlayer();
        } 
    }

    public void Chase( Vector2 direction)
    {
        if (playerTransform.position.x > transform.position.x && facingDirection == -1 ||
            playerTransform.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        rb.velocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
    }

    public void FindPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if(hits.Length > 0 && SpringDialogueManager.Instance.isOverConversation)
        {
            playerTransform = hits[0].transform;
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            
            if (Vector2.Distance(transform.position, playerTransform.position) <= attackRange && attackCoolDownTimer <= 0)
            {
                attackCoolDownTimer = attackCoolDown;
                rb.velocity = Vector2.zero;
                if (direction.y > 0.5)
                {
                    ChangeState(EnemyState.UpAttacking);
                }
                else if (direction.y < -0.5)
                {
                    ChangeState(EnemyState.DownAttacking);
                }
                else
                {
                    ChangeState(EnemyState.Attacking);
                }
            }
            else if(Vector2.Distance(transform.position, playerTransform.position) >= attackRange)
            {
                ChangeState(EnemyState.Chasing);
                Chase(direction);
            }
            else
            {
                rb.velocity = Vector2.zero;
                ChangeState(EnemyState.Idle);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
        
    }

    public void ChangeState(EnemyState newState)
    {
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);
        else if (enemyState == EnemyState.UpAttacking)
            anim.SetBool("isUpAttacking", false);
        else if (enemyState == EnemyState.DownAttacking)
            anim.SetBool("isDownAttacking", false);

        enemyState = newState;

        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
        else if (enemyState == EnemyState.UpAttacking)
            anim.SetBool("isUpAttacking", true);
        else if (enemyState == EnemyState.DownAttacking)
            anim.SetBool("isDownAttacking", true);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
        Gizmos.color = Color.yellow;
    }

}

public enum EnemyState
{
    Non,
    Idle,
    Chasing,
    Attacking,
    UpAttacking,
    DownAttacking,
    KnockBack,
}