using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    [SerializeField] private ExpGem expGemPrefab;
    [SerializeField] private int expAmount = 1;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float hitFeedbackInterval = 0.3f;
    [SerializeField] private Color hitColor = Color.white;

    private int currentHealth;

    private bool isDead;

    private float hitFeedbackTimer;
    private Color originColor;
    private bool isHit;

    private void Awake()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();            
        }

        if(spriteRenderer != null)
        {
            originColor = spriteRenderer.color;
        }       

        currentHealth = maxHealth;
        isDead = false;
        isHit = false;
    }

    private void Update()
    {
        if(isHit == false)
        {
            return;
        }

        hitFeedbackTimer += Time.deltaTime;
        if(hitFeedbackTimer >= hitFeedbackInterval)
        {
            isHit = false;
            spriteRenderer.color = originColor;
        }
    }

    /// <summary>
    /// 데미지 적용.
    /// </summary>
    /// <param name="damageAmount">데미지 양</param>
    public void TakeDamage(int damageAmount)
    {
        if(isDead == true)
        {
            return;
        }

        currentHealth -= damageAmount;

        HitFeedback();

        if (currentHealth <= 0)
        {
            // 사망처리.
            Die();
        }
    }

    void HitFeedback()
    {
        isHit = true;
        spriteRenderer.color = hitColor;
        hitFeedbackTimer = 0.0f;
    }

    void Die()
    {
        isDead = true;

        // 보석 생성.
        DropExpGem();

        Destroy(gameObject);
    }

    void DropExpGem()
    {
        if(expGemPrefab == null)
        {
            return;
        }

        ExpGem expGem = Instantiate(expGemPrefab, transform.position, Quaternion.identity);

        if(expGem != null)
        {
            expGem.Initialize(expAmount);
        }
    }
}
