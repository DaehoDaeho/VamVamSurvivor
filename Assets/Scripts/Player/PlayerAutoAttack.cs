using UnityEngine;

/// <summary>
/// 플레이어의 자동공격을 담당하는 클래스.
/// </summary>
public class PlayerAutoAttack : WeaponBase
{   
    [SerializeField] private PlayerTargetFinder targetFinder;
    [SerializeField] private int projectileCount = 1;
    [SerializeField] private float spreadAngle = 15.0f;

    private void Awake()
    {
        InitializeWeapon();

        // 투사체 개수와 각도 적용.
        ApplySpreadSettings();

        if (targetFinder == null)
        {
            targetFinder = GetComponent<PlayerTargetFinder>();
        }
    }

    bool SpawnProjectile(Transform target)
    {
        Vector2 startPosition = GetSpawnPosition();
        Vector2 targetPosition = target.position;
        Vector2 direction = targetPosition - startPosition;
        if(direction == Vector2.zero)
        {
            return false;
        }

        bool createAny = false;

        for(int i=0; i<projectileCount; i++)
        {
            // 중심 계산.
            float center = (projectileCount - 1) * 0.5f;
            float angleOffset = (i - center) * spreadAngle;
            Vector2 shotDirection = Quaternion.Euler(0.0f, 0.0f, angleOffset) * direction;
            if(FireProjectile(shotDirection) == true)
            {
                createAny = true;
            }
        }

        return createAny;
    }

    public void IncreaseProjectileCount(int amount)
    {
        projectileCount += amount;

        if(projectileCount > 5)
        {
            projectileCount = 5;
        }
    }

    void ApplySpreadSettings()
    {
        WeaponDataSO data = GetWeaponData();
        projectileCount = data.projectileCount;
        spreadAngle = data.spreadAngle;
    }

    protected override bool Attack()
    {
        if(targetFinder == null)
        {
            return false;
        }

        Transform target = targetFinder.GetNearestTarget();
        if (target == null)
        {
            return false;
        }

        return SpawnProjectile(target);
    }
}
