using UnityEngine;

/// <summary>
/// 플레이어의 자동공격을 담당하는 클래스.
/// </summary>
public class PlayerAutoAttack : MonoBehaviour
{
    [SerializeField] private PlayerTargetFinder targetFinder;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float attackInterval = 0.7f;

    private float nextAttackTime;

    private void Awake()
    {
        if(targetFinder == null)
        {
            targetFinder = GetComponent<PlayerTargetFinder>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 공격 처리.
        TryAutoAttack();
    }

    /// <summary>
    /// 자동 공격 시도.
    /// </summary>
    void TryAutoAttack()
    {
        if(targetFinder == null)
        {
            return;
        }

        if(Time.time < nextAttackTime)
        {
            return;
        }

        Transform target = targetFinder.GetNearestTarget();
        if(target == null)
        {
            return;
        }

        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if(enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageAmount);
            nextAttackTime = Time.time + attackInterval;
        }
    }
}
