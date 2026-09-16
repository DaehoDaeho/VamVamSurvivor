using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float stopDistance = 0.8f;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
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

        Vector2 velocity = direction * moveSpeed;

        body.linearVelocity = velocity;
    }
}
