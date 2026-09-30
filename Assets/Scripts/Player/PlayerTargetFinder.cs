using UnityEngine;

public class PlayerTargetFinder : MonoBehaviour
{
    [SerializeField] private float targetRange = 6.0f;
    [SerializeField] private LayerMask enemyLayer;

    private Transform nearestTarget;

    // Update is called once per frame
    void Update()
    {
        // 가장 가까운 적을 찾는다.
        FindNearestTarget();
        // 선 그리기 (플레이어 ~ 가장 가까운 적)
        DrawTargetLine();
    }

    void FindNearestTarget()
    {
        Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(transform.position, targetRange, enemyLayer);

        nearestTarget = null;
        float nearestDistance = float.MaxValue;

        for(int i=0; i<targetsInRange.Length; i++)
        {
            Collider2D targetCollider = targetsInRange[i];

            Vector2 playerPosition = transform.position;
            Vector2 targetPosition = targetCollider.transform.position;

            float distance = Vector2.Distance(playerPosition, targetPosition);

            if(distance < nearestDistance)
            {
                nearestTarget = targetCollider.transform;
                nearestDistance = distance;
            }
        }
    }

    void DrawTargetLine()
    {
        if(nearestTarget == null)
        {
            return;
        }

        Debug.DrawLine(transform.position, nearestTarget.position, Color.red);
    }

    /// <summary>
    /// 가장 가까운 적의 트랜스폼을 반환.
    /// </summary>
    /// <returns>적의 트랜스폼 정보</returns>
    public Transform GetNearestTarget()
    {
        return nearestTarget;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
}
