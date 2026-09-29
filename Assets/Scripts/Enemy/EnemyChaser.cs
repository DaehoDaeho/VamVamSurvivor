using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float stopDistance = 0.8f;

    [SerializeField] private float separationRadius = 0.6f;
    [SerializeField] private float separationWeight = 0.8f;

    [SerializeField] private LayerMask enemyLayer;
    
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    private float lastDirection = 1.0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        FlipSprite();
    }

    public void SetTarget(Transform targetTransform)
    {
        playerTransform = targetTransform;
    }

    private void FixedUpdate()
    {
        if(playerTransform == null)
        {
            body.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 playerPosition = playerTransform.position;
        Vector2 enemyPosition = transform.position;

        Vector2 difference = playerPosition - enemyPosition;

        float distance = difference.magnitude;

        if(distance <= stopDistance)
        {
            body.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = difference.normalized;
        // 분리 방향 계산.
        Vector2 separationDirection = CalculateSeparationDirection(enemyPosition);

        Vector2 finalDirection = direction + (separationDirection * separationWeight);

        if(finalDirection != Vector2.zero)
        {
            finalDirection = finalDirection.normalized;

            if(finalDirection.x != 0.0f && finalDirection.x != lastDirection)
            {
                lastDirection = finalDirection.x;
            }
        }

        body.linearVelocity = finalDirection * moveSpeed;
    }

    Vector2 CalculateSeparationDirection(Vector2 enemyPosition)
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(enemyPosition, separationRadius, enemyLayer);

        Vector2 separationDirection = Vector2.zero;

        for(int i=0; i<nearbyEnemies.Length; i++)
        {
            Collider2D nearbyEnemy = nearbyEnemies[i];

            if(nearbyEnemy.transform == transform)
            {
                continue;
            }

            Vector2 nearbyPosition = nearbyEnemy.transform.position;
            Vector2 awayDirection = enemyPosition - nearbyPosition;
            float distance = awayDirection.magnitude;

            if(distance <= 0.0f)
            {
                continue;
            }

            separationDirection += awayDirection.normalized / distance;
        }

        return separationDirection;
    }

    void FlipSprite()
    {
        if(spriteRenderer == null)
        {
            return;
        }

        spriteRenderer.flipX = lastDirection > 0.0f ? false : true;
    }
}
