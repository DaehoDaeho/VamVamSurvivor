using UnityEngine;

/// <summary>
/// 투사체의 이동을 담당하는 클래스.
/// </summary>
public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3.0f;

    [SerializeField] private Rigidbody2D body;

    private Vector2 moveDirection;
    private float moveSpeed;
    private bool isInitialized = false;

    private void Reset()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Initialize(Vector2 direction, float speed)
    {
        moveDirection = direction.normalized;
        moveSpeed = speed;
        isInitialized = true;
    }

    private void FixedUpdate()
    {
        if(isInitialized == false)
        {
            return;
        }

        body.linearVelocity = moveDirection * moveSpeed;
    }
}
