using UnityEngine;

public class SurvivalDifficultyManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float startSpawnInterval = 2.0f;

    [SerializeField] private float difficultyIncreaseInterval = 15.0f;

    [SerializeField] private float spawnIntervalDecrease = 0.2f;
    [SerializeField] private float minSpawnInterval = 0.5f;

    private float survivalTime;
    private float difficultyTimer;
    private int difficultyLevel = 1;
    private float currentSpawnInterval;

    private void Awake()
    {
        currentSpawnInterval = startSpawnInterval;
        if(enemySpawner != null)
        {
            enemySpawner.SetSpawnInterval(currentSpawnInterval);
        }

        Debug.Log("시작 난이도 레벨: " + difficultyLevel);
    }

    // Update is called once per frame
    void Update()
    {
        // 난이도 관련 처리.
        CountTime();
    }

    void CountTime()
    {
        survivalTime += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if(difficultyTimer < difficultyIncreaseInterval)
        {
            return;
        }

        // 난이도 상승 처리.
        IncreaseDifficulty();

        difficultyTimer = 0.0f;
    }

    void IncreaseDifficulty()
    {
        difficultyLevel++;

        currentSpawnInterval -= spawnIntervalDecrease;

        if(currentSpawnInterval < minSpawnInterval)
        {
            currentSpawnInterval = minSpawnInterval;
        }

        if(enemySpawner != null)
        {
            enemySpawner.SetSpawnInterval(currentSpawnInterval);
        }

        Debug.Log("생존 시간: " + survivalTime);
        Debug.Log("난이도 레벨: " + difficultyLevel);
        Debug.Log("생성 간격: " + currentSpawnInterval);
    }
}
