using UnityEngine;

public class ContactDamageArea : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float damageInterval = 0.5f;

    private float nextDamageTime;

    /// <summary>
    /// 오브젝트가 Trigger 충돌 상태를 유지하는 동안 계속 호출되는 함수.
    /// </summary>
    /// <param name="collision">충돌한 대상의 Collider2D 정보.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            return;
        }

        if(Time.time < nextDamageTime)
        {
            return;
        }

        playerHealth.TakeDamage(damageAmount);
        nextDamageTime = Time.time + damageInterval;
    }
}
