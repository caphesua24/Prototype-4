public class EnemyChasingPlayer : Enemy
{
    protected override void Move()
    {
        if (transform.position.y < 0) return;
        var lookDirection = (player.transform.position - transform.position).normalized;
        body.AddForce(lookDirection * speed);
    }
}