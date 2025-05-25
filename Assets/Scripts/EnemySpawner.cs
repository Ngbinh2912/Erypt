using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GroundScanner groundScanner;
    public List<EnemySpawnInfo> tier1Enemies, tier2Enemies, tier3Enemies;

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private List<Vector3> validSpawnPositions;

    void Start()
    {
        // Lay cac vi tri hop le tren tilemap
        validSpawnPositions = groundScanner.GetGroundPositions();

        // Spawn cac loai quai theo danh sach
        SpawnEnemies(tier1Enemies);
        SpawnEnemies(tier2Enemies);
        SpawnEnemies(tier3Enemies);
    }

    void SpawnEnemies(List<EnemySpawnInfo> enemyList)
    {
        foreach (var info in enemyList)
        {
            int count = Random.Range(info.minCount, info.maxCount + 1);
            for (int i = 0; i < count; i++)
            {
                Vector3 spawnPos = validSpawnPositions[Random.Range(0, validSpawnPositions.Count)];
                GameObject enemy = Instantiate(info.enemyPrefab, spawnPos, Quaternion.identity);

                // Tam thoi tat quai, cho den khi nguoi choi qua cua moi kich hoat
                enemy.SetActive(false);

                // Luu vao danh sach
                spawnedEnemies.Add(enemy);
            }
        }
    }

    public void ActivateAllEnemies()
    {
        foreach (var enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                // Bat quai hien len
                enemy.SetActive(true);

                // Goi ham kich hoat hanh vi di chuyen cua quai (neu co component Enemy)
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.ActivateEnemy();
                }
            }
        }
    }
}
