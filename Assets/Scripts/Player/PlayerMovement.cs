using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D playerRigidbody;

    private Vector2 moveDirection;
    private float lastDirection = 1.0f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 inputDirection = new Vector2(horizontal, vertical);

        if(horizontal != 0.0f && horizontal != lastDirection)
        {
            lastDirection = horizontal;
        }

        moveDirection = inputDirection.normalized;

        FlipSprite();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = moveDirection * moveSpeed;
        playerRigidbody.linearVelocity = velocity;
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
