using System.Security.Claims;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

    protected int bonusDamage;
    private float nextAttackTime;

    protected void InitializeWeapon()
    {
        if (weaponData != null)
        {
            projectilePrefab = weaponData.projectilePrefab;
            projectileSpeed = weaponData.projectileSpeed;
            attackInterval = weaponData.attackInterval;
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

        Vector2 startPosition = projectileSpawnPoint.position;

        if (direction == Vector2.zero)
        {
            return false;
        }

        ProjectileMovement projectileObject = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        if(projectileObject == null)
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

        if(Attack() == true)
        {
            FinishAttack();
        }
    }

    protected abstract bool Attack();
}
