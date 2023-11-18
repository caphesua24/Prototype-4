using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private const float SpawnRange = 9;

    private void Start()
    {
        var spawnPosition = new Vector3(
            Random.Range(-SpawnRange, SpawnRange),
            0.1f,
            Random.Range(-SpawnRange, SpawnRange)
        );
        Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
    }

    private void Update()
    {
    }
}