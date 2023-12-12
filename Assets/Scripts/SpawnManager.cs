using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject powerUpPrefab;
    public bool PowerUpAvailable { get; private set; }
    public Vector3 PowerUpPosition { get; private set; }

    private PlayerController _playerController;
    private const float SpawnRange = 9;
    private int _enemyCount;
    private int _waveNumber = 1;

    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnEnemyWave(_waveNumber);
        RandomSpawnPowerUp();
    }

    public void OnPowerUpCollect()
    {
        PowerUpAvailable = false;
        RandomSpawnPowerUp();
    }

    public void OnEnemyPrefabsDestroy()
    {
        _enemyCount--;
        if (_enemyCount > 0 || _playerController.IsGameOver()) return;

        _waveNumber++;
        SpawnEnemyWave(_waveNumber);
    }

    private void RandomSpawnPowerUp()
    {
        StartCoroutine(SpawnPowerUpRoutine());
    }

    private IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        var powerUp = Instantiate(powerUpPrefab, RandomSpawnPosition(), powerUpPrefab.transform.rotation);
        PowerUpPosition = powerUp.transform.position;
        PowerUpAvailable = true;
    }

    private void SpawnEnemyWave(int numberOfEnemy)
    {
        for (var i = 0; i < numberOfEnemy; i++)
        {
            var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(prefab, RandomSpawnPosition(), prefab.transform.rotation);
            _enemyCount++;
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