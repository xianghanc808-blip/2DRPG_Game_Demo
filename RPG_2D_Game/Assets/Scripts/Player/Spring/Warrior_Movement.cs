using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Warrior_Movement : MonoBehaviour
{
    public int facingDirection;

    private Rigidbody2D rb;
    private Animator animator;

    public bool isShooting;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(isShooting == true)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0 && transform.localScale.x < 0 ||
                horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }

            animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
            animator.SetFloat("Vertical", Mathf.Abs(vertical));

            rb.velocity = new Vector2(horizontal, vertical) * DatasManager.Instance.speed;
        }

            
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
