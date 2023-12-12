using UnityEngine;

public class EnemyDestroyingPowerUp : EnemyChasingPlayer
{
    protected override void Move()
    {
        if (transform.position.y < 0) return;
        
        // Check if a power-up item is available
        if (spawnManager.PowerUpAvailable)
        {
            // Move to destroy the Power-Up item
            var lookDirection = (spawnManager.PowerUpPosition - transform.position).normalized;
            body.AddForce(lookDirection * speed);
        }
        else
        {
            base.Move(); // Move to attack the player
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PowerUp")) return;

        Destroy(other.gameObject);
        spawnManager.OnPowerUpCollect();
    }
}