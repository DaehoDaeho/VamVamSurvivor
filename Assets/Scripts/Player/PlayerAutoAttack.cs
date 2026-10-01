using UnityEngine;

/// <summary>
/// 플레이어의 자동공격을 담당하는 클래스.
/// </summary>
public class PlayerAutoAttack : MonoBehaviour
{
    [SerializeField] private PlayerTargetFinder targetFinder;
    [SerializeField] private ProjectileMovement projectilePrefab;

    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed = 8.0f;

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

        bool isProjectileCreated = SpawnProjectile(target);
        if(isProjectileCreated == false)
        {
            return;
        }

        nextAttackTime = Time.time + attackInterval;
    }

    bool SpawnProjectile(Transform target)
    {
        if(projectilePrefab == null)
        {
            Debug.LogWarning("투사체 프리팹이 없습니다.");
            return false;
        }

        Vector2 startPosition = projectileSpawnPoint.position;
        Vector2 targetPosition = target.position;
        Vector2 direction = targetPosition - startPosition;

        if(direction == Vector2.zero)
        {
            return false;
        }

        ProjectileMovement projectileObject = Instantiate(projectilePrefab, startPosition, Quaternion.identity);

        if(projectileObject != null)
        {
            projectileObject.Initialize(direction, projectileSpeed);
            return true;
        }

        return false;
    }
}
