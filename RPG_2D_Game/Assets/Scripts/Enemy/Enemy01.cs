using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy01 : MonoBehaviour
{
    public int speed;
    public int facingDirection;
    public Transform detectPoint;
    public float detectRange;
    public float attackingRange;
    public LayerMask playerLayer;
    
    public EnemyState enemyState;

    public Transform attackPoint;
    public float damageRange;
    public int damage;

    public float maxHealth;
    public float currentHealth;

    public RectTransform enemyRect;
    private Rect hpRect;
    public Texture hpTexture;

    public int offsetY;
    public int offsetX;
    public float hpWidth;
    public int hpHeight;

    public float knockBackForce;
    public float stunTime;

    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;
    public int expReward;
    //Start is called before the first frame update


    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;
    private Vector2 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyState != EnemyState.KnockBack)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(detectPoint.position, detectRange, playerLayer);
            if (hits.Length > 0)
            {
                player = hits[0].transform;
                direction = (player.position - this.transform.position).normalized;
                if (Vector2.Distance(player.position, this.transform.position) >= attackingRange )
                {
                    ChangeState(EnemyState.Chasing);

                    if (player.transform.position.x > transform.position.x && facingDirection == 1 ||
                       player.transform.position.x < transform.position.x && facingDirection == -1)
                    {
                        facingDirection *= -1;
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    }
                    rb.velocity = direction * speed;
                }
                else if (Vector2.Distance(player.position, this.transform.position) <= attackingRange )
                {
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
            }
            else
            {
                rb.velocity = Vector2.zero;
                ChangeState(EnemyState.Idle);
            }
        }
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, damageRange, playerLayer);
        if(hits.Length > 0)
        {
            //hits[0].GetComponent<Player>().ChangeHealth(-damage);
            hits[0].GetComponent<Player>().knockback(this.transform, knockBackForce, stunTime);
        }
    }

    public void ChangeHealth(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            OnMonsterDefeated(expReward);
        }
    }

    public void KnockBack(Transform playerTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        ChangeState(EnemyState.KnockBack);
        StartCoroutine(StunTimer(knockbackTime, stunTime));
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.velocity = direction * knockbackForce;
    }

    IEnumerator StunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        ChangeState(EnemyState.Idle);
    }


    private void OnGUI()
    {

        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

        screenPos.y = Screen.height - screenPos.y;

        hpRect.y = screenPos.y - offsetY;
        hpRect.x = screenPos.x - offsetX;

        hpRect.width = (currentHealth / maxHealth) * hpWidth;
        hpRect.height = hpHeight;

        GUI.DrawTexture(hpRect, hpTexture);
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
}


