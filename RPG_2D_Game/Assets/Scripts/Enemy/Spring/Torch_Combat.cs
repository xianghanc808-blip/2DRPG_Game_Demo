using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_Combat : MonoBehaviour
{
    public int damage;
    public int weaponRange;
    public Transform attackPoint;
    public LayerMask playerLayer;


    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);

        if (hits.Length > 0)
        {
            if (!hits[0].GetComponent<Warrior_Combat>().isGuarding)
            {
                hits[0].GetComponent<Warrior_Health>().ChangeHealth(damage);
                SoundMusics.Instance.PlaySound("EnemyAttackSound");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
