using UnityEngine;

public class ExpGem : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private Rigidbody2D body;

    private int experienceAmount;
    private bool isMoving = false;
    private Transform target;
    private Vector2 moveDirection;

    public void Initialize(int amount)
    {
        experienceAmount = amount;
        isMoving = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerExperience playerExperience = collision.GetComponent<PlayerExperience>();

        if(playerExperience == null)
        {
            return;
        }

        Debug.Log("EXP + " + experienceAmount);

        playerExperience.AddExperience(experienceAmount);

        Destroy(gameObject);
    }

    private void Update()
    {
        if(isMoving == false)
        {
            return;
        }

        if (target == null)
        {
            return;
        }

        moveDirection = (target.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (isMoving == false)
        {
            return;
        }

        if(body == null)
        {
            return;
        }

        body.linearVelocity = moveDirection * moveSpeed;
    }

    public void StartMoveToPlayer(Transform newTarget)
    {
        if(isMoving == true)
        {
            return;
        }
        target = newTarget;
        isMoving = true;
    }
}
