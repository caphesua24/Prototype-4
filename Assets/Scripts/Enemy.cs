using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float speed = 3;
    protected GameObject player;
    protected Rigidbody body;
    private SpawnManager _spawnManager;

    private void Start()
    {
        player = GameObject.Find("Player");
        body = GetComponent<Rigidbody>();
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            _spawnManager.OnEnemyPrefabsDestroy();
        }
        else
        {
            Move();
        }
    }

    protected abstract void Move();
}