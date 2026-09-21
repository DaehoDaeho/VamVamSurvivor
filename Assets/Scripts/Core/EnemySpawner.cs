using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyChaser enemyPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnInterval = 2.0f;

    [SerializeField] private float minSpawnDistance = 6.0f;
    [SerializeField] private float maxSpawnDistance = 9.0f;

    private float spawnTimer;
    private int spawnIndex = 0;

    // Update is called once per frame
    void Update()
    {
        CountSpawnTime();
    }

    void CountSpawnTime()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            // 利阑 积己.
            SpawnEnemy();
            spawnTimer = 0.0f;
        }
    }

    void SpawnEnemy()
    {
        // 积己 困摹 拌魂.
        Vector2 spawnPosition = GetSpawnPosition();

        EnemyChaser newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        if(newEnemy != null)
        {
            newEnemy.SetTarget(playerTransform);
        }
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 playerPosition = playerTransform.position;

        return playerPosition + (randomDirection * randomDistance);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, minSpawnDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerTransform.position, maxSpawnDistance);
    }
}