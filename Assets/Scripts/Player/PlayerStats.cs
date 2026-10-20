using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float regenerationInterval = 5.0f;

    private int bonusMaximumHealth;
    private int regenerationAmount;
    private float nextRegenerationTime;

    private void Reset()
    {
        if(playerHealth == null)
        {
            playerHealth = GetComponent<PlayerHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(regenerationAmount <= 0.0f)
        {
            return;
        }

        if(Time.time < nextRegenerationTime)
        {
            return;
        }

        playerHealth.Heal(regenerationAmount);
        nextRegenerationTime = Time.time + regenerationInterval;
    }

    public void AddMaximumHealth(int amount)
    {
        bonusMaximumHealth += amount;
        playerHealth.IncreaseMaxHealth(amount);
    }

    public void AddRegeneration(int amount)
    {
        regenerationAmount += amount;
        nextRegenerationTime = Time.time + regenerationInterval;
    }
}
