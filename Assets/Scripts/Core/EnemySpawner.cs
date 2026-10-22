using UnityEngine;

public enum EnemyIndex
{
    Normal,
    Fast,
    Heavy
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyCatalogSO enemyCatalog;

    [SerializeField] private EnemyChaser[] enemyPrefabs;
    [SerializeField] private int[] enemyChance = { 65, 25, 10 };

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnInterval = 2.0f;

    [SerializeField] private float minSpawnDistance = 6.0f;
    [SerializeField] private float maxSpawnDistance = 9.0f;

    [SerializeField] private int maxEnemyCount = 1;

    private int currentEnemyCount = 0;

    private float spawnTimer;
    private int spawnIndex = 0;

    private WaveInfo currentWaveData;

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
            // 적을 생성.
            SpawnEnemy();
            spawnTimer = 0.0f;
        }
    }

    void SpawnEnemy()
    {
        if(maxEnemyCount > 0 && currentEnemyCount >= maxEnemyCount)
        {
            return;
        }

        // 생성 위치 계산.
        Vector2 spawnPosition = GetSpawnPosition();

        // 생성할 적을 선택하는 처리.
        EnemyChaser selectedPrefab = GetEnemyPrefab();

        if(selectedPrefab == null)
        {
            return;
        }

        EnemyChaser newEnemy = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        if(newEnemy != null)
        {
            newEnemy.SetTarget(playerTransform);
        }

        if (maxEnemyCount > 0)
        {
            currentEnemyCount++;
        }
    }

    EnemyChaser GetEnemyPrefab()
    {
        if(currentWaveData == null || enemyCatalog == null)
        {
            return null;
        }

        // 적의 등장 가중치의 총합을 저장할 변수.
        int totalWeight = 0;
        foreach(EnemyWeight entry in currentWaveData.enemies)
        {
            if(entry.weight > 0)
            {
                totalWeight += entry.weight;
            }
        }

        int randomValue = Random.Range(0, totalWeight);
        int accumulatedWeight = 0;

        foreach (EnemyWeight entry in currentWaveData.enemies)
        {
            if(entry.weight <= 0)
            {
                continue;
            }

            accumulatedWeight = accumulatedWeight + entry.weight;
            if(randomValue < accumulatedWeight)
            {
                GameObject obj = enemyCatalog.FindPrefab(entry.enemyId);
                EnemyChaser enemyChaser = obj.GetComponent<EnemyChaser>();
                return enemyChaser;
            }
        }

        return null;
    }

    public void SetWaveData(WaveInfo newWaveData)
    {
        currentWaveData = null;
        int totalWeight = 0;

        foreach(EnemyWeight entry in newWaveData.enemies)
        {
            totalWeight += entry.weight;
        }

        if(totalWeight <= 0)
        {
            return;
        }

        currentWaveData = newWaveData;
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 playerPosition = playerTransform.position;

        return playerPosition + (randomDirection * randomDistance);
    }

    public void SetSpawnInterval(float newSpawnInterval)
    {
        spawnInterval = newSpawnInterval;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, minSpawnDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerTransform.position, maxSpawnDistance);
    }
}