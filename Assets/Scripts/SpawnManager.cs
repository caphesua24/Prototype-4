using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject powerUpPrefab;
    public int enemyCount;
    public bool powerUpAvailable;
    public Vector3 powerUpPosition;
    public int waveNumber = 1;
    private const float SpawnRange = 9;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnEnemyWave(waveNumber);
        RandomSpawnPowerUp();
    }

    public void OnPowerUpCollect()
    {
        powerUpAvailable = false;
        RandomSpawnPowerUp();
    }

    public void OnEnemyPrefabsDestroy()
    {
        enemyCount--;
        if (enemyCount > 0 || _playerController.IsGameOver()) return;

        waveNumber++;
        SpawnEnemyWave(waveNumber);
    }

    private void RandomSpawnPowerUp()
    {
        StartCoroutine(SpawnPowerUpRoutine());
    }

    private IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        var powerUp = Instantiate(powerUpPrefab, RandomSpawnPosition(), powerUpPrefab.transform.rotation);
        powerUpPosition = powerUp.transform.position;
        powerUpAvailable = true;
    }

    private void SpawnEnemyWave(int numberOfEnemy)
    {
        for (var i = 0; i < numberOfEnemy; i++)
        {
            var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(prefab, RandomSpawnPosition(), prefab.transform.rotation);
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