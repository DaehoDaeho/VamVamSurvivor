using UnityEngine;
using System.Collections;

public class EliteEnemyBehavior : MonoBehaviour
{
    private enum EliteState
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

    [SerializeField] private Rigidbody2D body;
    [SerializeField] private EnemyChaser enemyChaser;

    private Transform playerTransform;
    private EliteState currentState = EliteState.Chase;
    private Vector2 dashDirection;
    private Coroutine attackRoutine;

    public void SetTarget(Transform target)
    {
        playerTransform = target;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(playerTransform == null || currentState != EliteState.Chase)
        {
            return;
        }

        float distance = Vector2.Distance(body.position, playerTransform.position);
        if(distance > attackRange)
        {
            return;
        }

        currentState = EliteState.Prepare;
        enemyChaser.enabled = false;
        body.linearVelocity = Vector2.zero;
        // 코루틴 함수 호출 - 공격 시퀀스 처리.
        attackRoutine = StartCoroutine(AttackSequence());
    }

    IEnumerator AttackSequence()
    {
        yield return new WaitForSeconds(prepareTime);

        if(playerTransform != null)
        {
            dashDirection = ((Vector2)playerTransform.position - body.position).normalized;
            currentState = EliteState.Dash;

            yield return new WaitForSeconds(dashTime);

            body.linearVelocity = Vector2.zero;

            currentState = EliteState.Recover;

            yield return new WaitForSeconds(recoverTime);

            // 공격 시퀀스 종료 처리.
            FinishAttack();
        }
        else
        {
            FinishAttack();
            yield break;
        }
    }

    void FinishAttack()
    {
        currentState = EliteState.Chase;
        enemyChaser.enabled = true;
        attackRoutine = null;
    }

    private void FixedUpdate()
    {
        if(currentState == EliteState.Dash)
        {
            body.linearVelocity = dashDirection * dashSpeed;
        }
    }

    private void OnDisable()
    {
        if(attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }

        if(body != null)
        {
            body.linearVelocity = Vector2.zero;
        }
    }
}
