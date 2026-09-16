using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;

    private Rigidbody2D playerRigidbody;

    private Vector2 moveDirection;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 inputDirection = new Vector2(horizontal, vertical);

        moveDirection = inputDirection.normalized;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = moveDirection * moveSpeed;
        playerRigidbody.linearVelocity = velocity;
    }
}
