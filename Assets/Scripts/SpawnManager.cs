using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public int enemyCount;
    public int waveNumber = 1;
    private const float SpawnRange = 9;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnEnemyWave(waveNumber);
        RandomSpawnPowerUp();
    }

    public void OnEnemyPrefabsDestroy()
    {
        enemyCount--;
        if (enemyCount > 0 || _playerController.IsGameOver()) return;

        waveNumber++;
        SpawnEnemyWave(waveNumber);
    }

    public void RandomSpawnPowerUp()
    {
        StartCoroutine(SpawnPowerUpRoutine());
    }

    private IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        Instantiate(powerUpPrefab, RandomSpawnPosition(), powerUpPrefab.transform.rotation);
    }

    private void SpawnEnemyWave(int numberOfEnemy)
    {
        for (var i = 0; i < numberOfEnemy; i++)
        {
            Instantiate(enemyPrefab, RandomSpawnPosition(), enemyPrefab.transform.rotation);
            enemyCount++;
        }
    }

    private static Vector3 RandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(-SpawnRange, SpawnRange),
            0.1f,
            Random.Range(-SpawnRange, SpawnRange)
        );
    }
}