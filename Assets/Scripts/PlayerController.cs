using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject powerUpIndicator;
    
    private const float PowerUpStrength = 15;
    private const float PowerUpCountDownSeconds = 7;

    private bool _hasPowerUp;
    private bool _isGameOver;
    private GameObject _focalPoint;
    private Rigidbody _rigidbody;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _focalPoint = GameObject.Find("Focal Point");
        _rigidbody = GetComponent<Rigidbody>();
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        var verticalInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(_focalPoint.transform.forward * (verticalInput * speed));

        powerUpIndicator.gameObject.transform.position = transform.position - Vector3.up * 0.4f;

        if (transform.position.y < -10)
        {
            _isGameOver = true;
            Debug.Log("Game Over");
        }
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PowerUp")) return;
        
        Destroy(other.gameObject);
        
        OnPowerUpActivate();
    }

    private void OnCollisionEnter(Collision other)
    {
        var otherGameObject = other.gameObject;

        if (!otherGameObject.CompareTag("Enemy") || !_hasPowerUp) return;

        var awayFromPlayer = otherGameObject.transform.position - transform.position;
        var enemyRigidBody = otherGameObject.GetComponent<Rigidbody>();
        enemyRigidBody.AddForce(awayFromPlayer * PowerUpStrength, ForceMode.Impulse);
    }

    private IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(PowerUpCountDownSeconds);
        OnPowerUpDeactivate();
    }

    private void OnPowerUpActivate()
    {
        _spawnManager.OnPowerUpCollect();
        _hasPowerUp = true;
        StartCoroutine(PowerUpCountDownRoutine());
        powerUpIndicator.gameObject.SetActive(true);
    }

    private void OnPowerUpDeactivate()
    {
        _hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
}