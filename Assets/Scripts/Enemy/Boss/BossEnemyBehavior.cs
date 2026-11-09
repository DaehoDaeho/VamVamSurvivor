using System.Collections;
using System.Xml;
using UnityEngine;

public class BossEnemyBehavior : MonoBehaviour
{
    private enum BossState
    {
        Chase,
        Prepare,
        Dash,
        Recover
    }

    [SerializeField] private float attackRange = 3.0f;
    [SerializeField] private float prepareTime = 0.7f;
    [SerializeField] private float dashSpeed = 9.0f;
    [SerializeField] private float dashTime = 0.35f;
    [SerializeField] private float recoverTime = 1.0f;

    [SerializeField] private float phaseTwoHealthThreshold = 0.5f;
    [SerializeField] private int phaseTwoDashCount = 2;

    [SerializeField] private float phaseTwoPrepareTime = 0.45f;

    [SerializeField] private Rigidbody2D body;
    [SerializeField] private EnemyChaser enemyChaser;
    [SerializeField] private EnemyHealth enemyHealth;

    private BossState currentState = BossState.Chase;
    private Transform playerTransform;
    private Vector2 dashDirection;
    private Coroutine attackRoutine;
    private bool phaseTwo;

    public void SetTarget(Transform target)
    {
        playerTransform = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(phaseTwo == false && enemyHealth != null && enemyHealth.HealthRatio <= phaseTwoHealthThreshold)
        {
            phaseTwo = true;
        }

        if (playerTransform == null || currentState != BossState.Chase)
        {
            return;
        }

        float distance = Vector2.Distance(body.position, playerTransform.position);
        if(distance > attackRange)
        {
            return;
        }

        currentState = BossState.Prepare;
        enemyChaser.enabled = false;
        body.linearVelocity = Vector2.zero;
        // 코루틴 함수 호출.
        attackRoutine = StartCoroutine(AttackSequence());
    }

    IEnumerator AttackSequence()
    {
        int repeatCount = phaseTwo ? phaseTwoDashCount : 1;
        float waitTime = phaseTwo ? phaseTwoPrepareTime : prepareTime;

        for(int i=0; i<repeatCount; i++)
        {
            currentState = BossState.Prepare;
            body.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(waitTime);

            if(playerTransform == null)
            {
                // 공격 시퀀스 종료.
                FinishAttack();
                yield break;
            }

            dashDirection = ((Vector2)playerTransform.position - body.position).normalized;
            currentState = BossState.Dash;

            yield return new WaitForSeconds(dashTime);

            body.linearVelocity = Vector2.zero;
        }

        currentState = BossState.Recover;

        yield return new WaitForSeconds(recoverTime);

        // 공격 시퀀스 종료 처리.
        FinishAttack();
    }

    void FinishAttack()
    {
        currentState = BossState.Chase;
        enemyChaser.enabled = true;
        attackRoutine = null;
    }

    private void FixedUpdate()
    {
        if(currentState == BossState.Dash)
        {
            body.linearVelocity = dashDirection * dashSpeed;
        }
    }
}
