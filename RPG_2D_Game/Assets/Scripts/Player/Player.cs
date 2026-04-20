using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    private int facingDirection = 1;
    public Animator anim;

    public Slider HPSlider;
    public float sliderValue;
    public TMP_Text HPText;

    public Transform attackPoint;

    public LayerMask enemyLayer;
    public float cooldown = 0.02f;
    //private float timer = 0.1f;

    public bool isGuarding;
    private bool isKnockback;

    private void Start()
    {
        HPText.text = "HP:" + StatsManager.Instance.currentHealth + "\\" + StatsManager.Instance.maxHealth;
    }

    private void Update()
    {
        
            //if (timer > 0)
            //{
            //    timer -= Time.deltaTime;
            //}
            //if (timer < 0)
            //{
            if (Input.GetKeyDown(KeyCode.K))
            {
                rb.velocity = Vector2.zero;
                Attack();
                //timer = cooldown;
            }
            //}
            if (Input.GetKeyDown(KeyCode.J))
            {
                anim.SetBool("isGuarding", true);
                isGuarding = true;
            }
            if (Input.GetKeyUp(KeyCode.J))
            {
                anim.SetBool("isGuarding", false);
                isGuarding = false;
            }


    }

    private void FixedUpdate()
    {
        if(isKnockback == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0 && this.transform.localScale.x < 0 ||
                horizontal < 0 && this.transform.localScale.x > 0)
            {
                Flip();
            }

            anim.SetFloat("Horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("Vertical", Mathf.Abs(vertical));

            rb.velocity = new Vector2(horizontal, vertical) * StatsManager.Instance.speed;
        }
        
    }


    public void knockback(Transform enemyTransform, float knockbackForce, float stunTime)
    {
        isKnockback = true;
        StartCoroutine(KnockbackCounter(stunTime));
        Vector2 direction = (this.transform.position - enemyTransform.position).normalized;
        rb.velocity = direction * knockbackForce;
    }

    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        isKnockback = false;
    }

    void Flip()
    {
        facingDirection *= -1;
        this.transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

    }

    public void ChangeHealth(float damage)
    {
        if (isGuarding)
        {
            damage *= StatsManager.Instance.guard;
        }
        StatsManager.Instance.currentHealth += damage;
        sliderValue = damage / StatsManager.Instance.maxHealth;

        HPSlider.value += sliderValue;

        if (StatsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        HPText.text = "HP:" + StatsManager.Instance.currentHealth + "\\" + StatsManager.Instance.maxHealth;
    }


    public void Attack()
    {
        anim.SetBool("isAttacking", true);
    }
    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    public void FinishGuard()
    {
        anim.SetBool("isGuarding", false);
    }
    public void RealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.attackRange, enemyLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<Enemy01>().ChangeHealth(StatsManager.Instance.damage);
            hits[0].GetComponent<Enemy01>().KnockBack(this.transform, StatsManager.Instance.knockbackForce, StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
        }
    }
}
