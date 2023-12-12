public class EnemyChasingPlayer : Enemy
{
    protected override void Move()
    {
        var lookDirection = (player.transform.position - transform.position).normalized;
        body.AddForce(lookDirection * speed);
    }
}