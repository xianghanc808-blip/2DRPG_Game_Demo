using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Torch_AStartRoam : MonoBehaviour
{
    private IAstarAI ai;
    private AIDestinationSetter destinationSetter;

    [Header("TorchProperty")]
    public float speed;
    public int facingDirection;
    public float attackCoolDown;

    [Header("FindPlayerCondition")]
    public Transform detectionPoint;
    public LayerMask playerLayer;
    public int playerDetectRange;
    public int attackRange;

    [Header("Patrol Settings (—≤¬ﬂ…Ë÷√)")]
    public float patrolRadius = 5f;
    public float waitTimeAtNode = 2f;
    private Vector3 spawnPoint;
    private float patrolTimer;


    private float attackCoolDownTimer = 0.1f;
    private Transform playerTransform;
    private Animator anim;
    private EnemyState enemyState;
    private bool needConversation;

    private void Start()
    {
        anim = GetComponent<Animator>();

        ai = GetComponent<IAstarAI>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        spawnPoint = transform.position;
        ai.maxSpeed = speed;

        ChangeState(EnemyState.Idle);
    }

    private void OnEnable()
    {
        SpringNPC_Talk.OnAIIsStop += AIisStopApplay;
        SpringNPC_Talk.OnAIRoam += AIRoamApply;

    }

    private void OnDisable()
    {
        SpringNPC_Talk.OnAIIsStop -= AIisStopApplay;
        SpringNPC_Talk.OnAIRoam += AIRoamApply;
    }

    private void Update()
    {
        if (enemyState == EnemyState.KnockBack)
            return;
        if (attackCoolDownTimer > 0)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }
        LogicUpdate();
        HandleFlip();
    }

    private void LogicUpdate()
    {
        Collider2D playerHit = Physics2D.OverlapCircle(detectionPoint.position, playerDetectRange, playerLayer);

        if (playerHit != null && SpringDialogueManager.Instance.isOverConversation)
        {
            needConversation = false;
            playerTransform = playerHit.transform;
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if(distanceToPlayer <= attackRange)
            {
                if(attackCoolDownTimer <= 0)
                {
                    ExecuteAttack();
                }
                else
                {
                    ai.isStopped = true;
                    ChangeState(EnemyState.Idle);
                }
            }
            else
            {
                ai.isStopped = false;
                ai.destination = playerTransform.position;
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            if (!needConversation)
            {
                playerTransform = null;
                RoamLogic();
            }
        }
    }

    private void RoamLogic()
    {
        ai.isStopped = false;

        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            patrolTimer += Time.deltaTime;
            ChangeState(EnemyState.Idle);

            if(patrolTimer >= waitTimeAtNode)
            {
                Vector3 randomDir = Random.insideUnitCircle * patrolRadius;
                ai.destination = spawnPoint + randomDir;
                ai.SearchPath();
                patrolTimer = 0;
            }
        }
        else
        {
            ChangeState(EnemyState.Chasing);
        }
    }

    private void ExecuteAttack()
    {
        ai.isStopped = true;
        attackCoolDownTimer = attackCoolDown;

        Vector2 direction = (playerTransform.position - transform.position).normalized;

        if (direction.y > 0.5f) ChangeState(EnemyState.UpAttacking);
        else if (direction.y < -0.5f) ChangeState(EnemyState.DownAttacking);
        else ChangeState(EnemyState.Attacking);
    }

    private void HandleFlip()
    {
        if (ai.desiredVelocity.x > 0.1f && facingDirection == -1) Flip();
        else if (ai.desiredVelocity.x < 0.1f && facingDirection == 1) Flip();
    }

    void Flip()
    {
        facingDirection *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void ChangeState(EnemyState newState)
    {
        if (enemyState == newState) return;
        Debug.Log(newState);
        SetAnimParams(enemyState, false);
        enemyState = newState;
        SetAnimParams(enemyState, true);

    }

    private void SetAnimParams(EnemyState state, bool value)
    {
        switch (state)
        {
            case EnemyState.Idle: anim.SetBool("isIdle", value);
                break;
            case EnemyState.Chasing: anim.SetBool("isChasing", value);
                break;
            case EnemyState.Attacking: anim.SetBool("isAttacking", value);
                break;
            case EnemyState.UpAttacking: anim.SetBool("isUpAttacking", value);
                break;
            case EnemyState.DownAttacking: anim.SetBool("isDownAttacking", value);
                break;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(spawnPoint, patrolRadius);
        
    }

    public void AIisStopApplay()
    {
        ai.isStopped = true;
        ChangeState(EnemyState.Idle);
        Debug.Log(ai.isStopped);
        needConversation = true;
    }

    public void AIRoamApply()
    {
        needConversation = false;
    }
}
