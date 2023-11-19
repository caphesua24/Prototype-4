using System.Collections;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject powerupIndicator;
    public ParticleSystem smokeParticle;
    public bool hasPowerup;
    public int powerUpDuration = 5;

    private const float Speed = 500;
    private const float TurboSpeed = 5000;
    private const float NormalStrength = 10; // how hard to hit enemy without powerup
    private const float PowerupStrength = 25; // how hard to hit enemy with powerup

    private Rigidbody _playerRb;
    private GameObject _focalPoint;

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }

    private void Update()
    {
        var speed = Speed;
        if (Input.GetKey(KeyCode.Space))
        {
            PlaySmokeParticle();
            speed = TurboSpeed;
        }
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        _playerRb.AddForce(_focalPoint.transform.forward * (verticalInput * speed * Time.deltaTime));

        // Set powerup indicator position to beneath player
        var playerPosition = transform.position;
        powerupIndicator.transform.position = playerPosition + new Vector3(0, -0.6f, 0);
        smokeParticle.transform.position = new Vector3(playerPosition.x, -0.5f, playerPosition.z);
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void PlaySmokeParticle()
    {
        smokeParticle.Play();
        StartCoroutine(SmokeParticleDeactivateRoutine());
    }

    IEnumerator SmokeParticleDeactivateRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        smokeParticle.Stop();
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * PowerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * NormalStrength, ForceMode.Impulse);
            }
        }
    }
}