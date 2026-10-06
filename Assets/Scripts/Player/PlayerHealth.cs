using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;
    [SerializeField] private Image playerHP;

    // 접근제한자 / 접근지정자.
    // public : 외부에서 제약 없이 접근 가능.
    // private : 외부에서 접근 불가능. 클래스 내부에서만 접근 가능.

    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
        UpdatePlayerHPUI();
    }

    public void TakeDamage(int damageAmount)
    {
        if(isDead == true)
        {
            return;
        }

        currentHealth -= damageAmount;
        //Debug.Log("Player HP: " + currentHealth);

        UpdatePlayerHPUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        //Debug.Log("플레이어 사망!!!");
    }

    void UpdatePlayerHPUI()
    {
        if(playerHP == null)
        {
            return;
        }

        // % 계산 : 현재 체력 / 최대 체력.
        playerHP.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
