using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemySpawner : MonoBehaviour
{
    public GroundScanner groundScanner;
    public DoorTrigger doorTrigger;
    public List<EnemySpawnInfo> tier1Enemies, tier2Enemies, tier3Enemies;

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private List<Vector3> validSpawnPositions;

    public event Action onAllEnemiesDefeated;

    void Start()
    {
        validSpawnPositions = groundScanner.GetGroundPositions();
        SpawnEnemies(tier1Enemies);
        SpawnEnemies(tier2Enemies);
        SpawnEnemies(tier3Enemies);
    }

    void SpawnEnemies(List<EnemySpawnInfo> enemyList)
    {
        foreach (var info in enemyList)
        {
            int count = UnityEngine.Random.Range(info.minCount, info.maxCount + 1);
            for (int i = 0; i < count; i++)
            {
                Vector3 spawnPos = validSpawnPositions[UnityEngine.Random.Range(0, validSpawnPositions.Count)];
                GameObject enemy = Instantiate(info.enemyPrefab, spawnPos, Quaternion.identity);
                enemy.SetActive(true);

                var enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.OnEnemyDied += HandleEnemyDied;
                }

                spawnedEnemies.Add(enemy);
            }
        }
    }

    private void HandleEnemyDied(Enemy deadEnemy)
    {
        spawnedEnemies.Remove(deadEnemy.gameObject);

        if (spawnedEnemies.Count == 0)
        {
            if (doorTrigger != null)
            {
                doorTrigger.UnlockDoors();
            }

            onAllEnemiesDefeated?.Invoke();
        }
    }
}
