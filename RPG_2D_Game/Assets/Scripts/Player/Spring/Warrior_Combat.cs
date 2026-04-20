using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_Combat : MonoBehaviour
{
    public int weaponRange;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float coolDownTime = 1f;
    public bool isGuarding;


    private float timer = .1f;
    private Animator anim;
    private int heavyBlow;
   

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0 && (!anim.GetCurrentAnimatorStateInfo(0).IsTag("isAttacking") && !anim.GetCurrentAnimatorStateInfo(0).IsTag("isHeavyAttacking")))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {

                Attack();
                timer = coolDownTime;
            }
        }
        Guarding();
    }


    public void Attack()
    {
        

        heavyBlow += 1;
        if (heavyBlow == 4)
        {
            heavyBlow = 0;

            anim.SetBool("isHeavyAttacking", false);
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isHeavyAttacking", true);
            anim.SetBool("isAttacking", false);
        }
    }

    public void PlayAttackSound()
    {
        SoundMusics.Instance.PlaySound("AttackingSound");
    }

    public void Guarding()
    {
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

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);
        if(enemies.Length > 0)
        {
            enemies[0].GetComponent<Torch_Health>().ChangeHealth(DatasManager.Instance.damamge);
            SoundMusics.Instance.PlaySound("AttackedSound");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    public void FinishHeavyAttacking()
    {
        anim.SetBool("isHeavyAttacking", false);
    }
   
}
