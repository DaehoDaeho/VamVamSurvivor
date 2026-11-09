using UnityEditor;
using UnityEngine;

public class BossWaveController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject elitePrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform[] specialSpawnPoints;
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private float specialWaveTime = 45.0f;
    [SerializeField] private float bossSpawnTime = 90.0f;

    private float elapsedTime;
    private bool specialWaveStarted;
    private bool bossSpawned;
    
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= specialWaveTime && specialWaveStarted == false)
        {
            specialWaveStarted = true;
            // 특수웨이브 발동.
            SpawnSpecialWave();
        }

        if(elapsedTime >= bossSpawnTime && bossSpawned == false)
        {
            bossSpawned = true;
            // 보스 생성.
            SpawnBoss();
        }
    }

    void SpawnSpecialWave()
    {
        if(specialSpawnPoints == null)
        {
            return;
        }

        foreach(Transform spawnPoint in specialSpawnPoints)
        {
            if(spawnPoint == null)
            {
                continue;
            }

            // 엘리트 적 생성.
            SpawnEnemy(elitePrefab, spawnPoint.position);
        }
    }

    void SpawnBoss()
    {
        if(bossSpawnPoint == null)
        {
            return;
        }

        // 보스 생성.
        SpawnEnemy(bossPrefab, bossSpawnPoint.position);
    }

    void SpawnEnemy(GameObject prefab, Vector3 position)
    {
        if(prefab == null || playerTransform == null)
        {
            return;
        }

        GameObject newEnemy = Instantiate(prefab, position, Quaternion.identity);

        EnemyChaser enemyChaser = newEnemy.GetComponent<EnemyChaser>();
        if(enemyChaser != null)
        {
            enemyChaser.SetTarget(playerTransform);
        }

        EliteEnemyBehavior eliteBehavior = newEnemy.GetComponent<EliteEnemyBehavior>();
        if(eliteBehavior != null)
        {
            eliteBehavior.SetTarget(playerTransform);
        }

        // 보스 행동 스크립트를 가져와서 추적 대상 설정.
        BossEnemyBehavior bossBehavior = newEnemy.GetComponent<BossEnemyBehavior>();
        if(bossBehavior != null)
        {
            bossBehavior.SetTarget(playerTransform);
        }
    }
}
