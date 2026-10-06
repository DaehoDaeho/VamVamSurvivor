using UnityEngine;

/// <summary>
/// 투사체의 이동을 담당하는 클래스.
/// </summary>
public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3.0f;

    [SerializeField] private int damageAmount = 1;
    [SerializeField] private int pierceCount = 1;

    [SerializeField] private Rigidbody2D body;

    private Vector2 moveDirection;
    private float moveSpeed;
    private bool isInitialized = false;

    private int remainPierceCount;

    private void Reset()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
    }

    private void Awake()
    {
        remainPierceCount = pierceCount;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if(enemyHealth == null)
        {
            return;
        }

        enemyHealth.TakeDamage(damageAmount);

        remainPierceCount--;

        if(remainPierceCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
