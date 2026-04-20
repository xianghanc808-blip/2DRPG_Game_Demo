using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{
    public Vector2[] patrolPoints;
    public float pauseDuration;
    public bool isPused;

    public float speed = 2;
    public int currentPatrolIndex;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 target;
    private bool isTolking;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(SetPatrolPoint());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            isTolking = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!enabled)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SetPatrolPoint());
            isTolking = false;
        }
    }

    private void Update()
    {
        if (isTolking || isPused)
        {
            rb.velocity = Vector2.zero;
            anim.Play("Idle");
            return;
        }

        Vector2 direction = ((Vector3)target - transform.position).normalized;

        if (direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        rb.velocity = direction * speed;

        if (Vector2.Distance(transform.position, target) < 1f)
            StartCoroutine(SetPatrolPoint());



    }
    
    IEnumerator  SetPatrolPoint()
    {
        isPused = true;
        anim.Play("Idle");
        yield return new WaitForSeconds(pauseDuration);

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        target = patrolPoints[currentPatrolIndex];
        isPused = false;
        anim.Play("Walk");
    }
}
