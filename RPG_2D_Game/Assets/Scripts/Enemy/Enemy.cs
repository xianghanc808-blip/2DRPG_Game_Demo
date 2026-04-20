using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Enemy : MonoBehaviour
{
    public int speed;
    public int facingDirection = 1;
    public float attackRange;
    public float detectPlayerRange;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    public Rigidbody2D rb;
    public EnemyState enemyState;
    public Animator anim;

    private Transform playerTransform;

    private void Update()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, detectPlayerRange, playerLayer);
        if (hits.Length > 0)
        {
            playerTransform = hits[0].transform;
            if (Vector2.Distance(playerTransform.position, this.transform.position) >= attackRange)
            {
                ChangeState(EnemyState.Chasing);
                if (playerTransform.position.x > transform.position.x && facingDirection == -1 ||
                playerTransform.position.x < transform.position.x && facingDirection == 1)
                {
                    facingDirection *= -1;
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                rb.velocity = direction * speed;

            }
            else if (Vector2.Distance(playerTransform.position, this.transform.position) <= attackRange)
            {
                ChangeState(EnemyState.Attacking);
            }
        }
        else
        {
            ChangeState(EnemyState.Idle);
            rb.velocity = Vector2.zero;
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }

        enemyState = newState;

        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
    }

}


