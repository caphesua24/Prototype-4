using UnityEngine;

public class EnemyX : MonoBehaviour
{
    private const float Speed = 20;
    private Rigidbody _enemyRb;
    private GameObject _playerGoal;

    // Start is called before the first frame update
    private void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _playerGoal = GameObject.Find("Player Goal");
    }

    // Update is called once per frame
    private void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (_playerGoal.transform.position - transform.position).normalized;
        _enemyRb.AddForce(lookDirection * (Speed * Time.deltaTime));
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
}