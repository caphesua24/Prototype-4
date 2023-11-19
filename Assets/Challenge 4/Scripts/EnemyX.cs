﻿using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody _enemyRb;
    private GameObject _playerGoal;
    private SpawnManagerX _spawnManager;

    // Start is called before the first frame update
    private void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _playerGoal = GameObject.Find("Player Goal");
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
        speed = CalculateSpeedForWave(_spawnManager.waveCount - 1);
    }

    // Update is called once per frame

    private void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (_playerGoal.transform.position - transform.position).normalized;
        _enemyRb.AddForce(lookDirection * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }
    }

    private float CalculateSpeedForWave(int waveCount)
    {
        return 10 + 10 * waveCount;
    }
}