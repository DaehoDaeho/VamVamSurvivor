using UnityEngine;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private int initialSize = 30;
    private GameObject projectilePrefab;

    private Queue<ProjectileMovement> availableProjectiles = new Queue<ProjectileMovement>();

    public void Configure(GameObject prefab)
    {
        if(prefab == null || prefab.GetComponent<ProjectileMovement>() == null)
        {
            return;
        }

        projectilePrefab = prefab;

        for(int i=0; i<initialSize; i++)
        {
            // 총알 생성.
            ProjectileMovement projectile = CreateProjectile();
            if(projectile != null)
            {
                availableProjectiles.Enqueue(projectile);
            }
        }
    }

    ProjectileMovement CreateProjectile()
    {
        GameObject instance = Instantiate(projectilePrefab, transform);
        ProjectileMovement projectile = instance.GetComponent<ProjectileMovement>();
        if(projectile == null)
        {
            Destroy(instance);
            return null;
        }

        // 추가할 코드 더 있음.
        projectile.SetPool(this);
        instance.SetActive(false);
        return projectile;
    }

    public ProjectileMovement Get(Vector2 position)
    {
        ProjectileMovement projectile = null;
        if(availableProjectiles.Count > 0)
        {
            projectile = availableProjectiles.Dequeue();
        }
        else
        {
            projectile = CreateProjectile();
        }

        //projectile.transform.SetParent(null);
        projectile.transform.position = position;
        projectile.transform.rotation = Quaternion.identity;
        projectile.gameObject.SetActive(true);
        return projectile;
    }

    public void Release(ProjectileMovement projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        availableProjectiles.Enqueue(projectile);
    }
}
