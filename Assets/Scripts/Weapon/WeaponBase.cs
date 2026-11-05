using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 무기 관련 추상 클래스.
/// </summary>
public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;

    [SerializeField] private ProjectileMovement projectilePrefab;

    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed = 8.0f;

    [SerializeField] private float attackInterval = 0.7f;

    [SerializeField] private int damageAmount = 1;

    [SerializeField] private ProjectilePool projectilePool;

    protected int bonusDamage;
    private float nextAttackTime;

    private readonly List<Collider2D> overlapResults = new List<Collider2D>(64);
    private readonly HashSet<IDamageable> hitTargets = new HashSet<IDamageable>();

    protected void InitializeWeapon()
    {
        if (weaponData != null)
        {
            projectilePrefab = weaponData.projectilePrefab;
            projectileSpeed = weaponData.projectileSpeed;
            attackInterval = weaponData.attackInterval;
        }

        if(projectilePool != null && projectilePrefab != null)
        {
            projectilePool.Configure(projectilePrefab.gameObject);
        }
    }

    protected WeaponDataSO GetWeaponData()
    {
        return weaponData;
    }

    protected Vector2 GetSpawnPosition()
    {
        return projectileSpawnPoint.position;
    }

    protected bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }

    protected void FinishAttack()
    {
        nextAttackTime = Time.time + attackInterval;
    }

    protected bool FireProjectile(Vector2 direction)
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("투사체 프리팹이 없습니다.");
            return false;
        }

        if(projectilePool == null)
        {
            return false;
        }

        Vector2 startPosition = projectileSpawnPoint.position;

        if (direction == Vector2.zero)
        {
            return false;
        }

        //ProjectileMovement projectileObject = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        ProjectileMovement projectileObject = projectilePool.Get(projectileSpawnPoint.position);

        if (projectileObject == null)
        {
            return false;
        }

        projectileObject.Initialize(direction, projectileSpeed);

        if(weaponData != null)
        {
            projectileObject.SetBaseDamage(weaponData.baseDamage);
        }

        projectileObject.AddBonusDamage(bonusDamage);

        return true;
    }

    public void IncreaseDamage(int amount)
    {
        bonusDamage += amount;
    }

    public void IncreaseAttackSpeed(float amount)
    {
        attackInterval -= amount;

        if (attackInterval < 0.15f)
        {
            attackInterval = 0.15f;
        }
    }

    public void TryAttack()
    {
        if(CanAttack() == false)
        {
            return;
        }

        //if(Attack() == true)
        //{
        //    FinishAttack();
        //}
        Attack();
        FinishAttack();
    }

    protected abstract bool Attack();

    protected int GetAttackDamage()
    {
        int baseDamage = 1;
        if(weaponData != null)
        {
            baseDamage = weaponData.baseDamage;
        }

        return Mathf.Max(1, baseDamage + bonusDamage);
    }

    protected int DamageTargetsInCircle(Vector2 center, float radius, LayerMask targetLayers)
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(targetLayers);

        overlapResults.Clear();
        int resultCount = Physics2D.OverlapCircle(center, radius, contactFilter, overlapResults);

        hitTargets.Clear();

        int damage = GetAttackDamage();

        for(int i=0; i<resultCount; i++)
        {
            Collider2D hitCollider = overlapResults[i];
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();
            if(damageable == null)
            {
                continue;
            }

            if(hitTargets.Add(damageable) == false)
            {
                continue;
            }

            damageable.TakeDamage(damage);
        }

        return hitTargets.Count;
    }
}
