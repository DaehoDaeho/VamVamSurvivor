using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyChaser enemyPrefab;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnInterval = 2.0f;

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
            // 적을 생성.
            SpawnEnemy();
            spawnTimer = 0.0f;
        }
    }

    void SpawnEnemy()
    {
        EnemyChaser newEnemy = Instantiate(enemyPrefab, spawnPoint[spawnIndex].position, Quaternion.identity);

        if(newEnemy != null)
        {
            newEnemy.SetTarget(playerTransform);
            UpdateSpawnIndex();
        }
    }

    void UpdateSpawnIndex()
    {
        //spawnIndex++;
        //if(spawnIndex >= spawnPoint.Length)
        //{
        //    spawnIndex = 0;
        //}

        spawnIndex = (spawnIndex + 1) % spawnPoint.Length;
    }
}