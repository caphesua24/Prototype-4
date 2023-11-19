using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3;
    private GameObject _player;
    private Rigidbody _rigidbody;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _rigidbody = GetComponent<Rigidbody>();
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        var lookDirection = (_player.transform.position - transform.position).normalized;
        _rigidbody.AddForce(lookDirection * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            _spawnManager.OnEnemyPrefabsDestroy();
        }
    }
}