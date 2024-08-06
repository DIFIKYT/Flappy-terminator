public class EnemyBulletSpawner : BulletSpawner<EnemyBullet>
{
    protected override EnemyBullet CreateBullet()
    {
        EnemyBullet bullet = Instantiate(BulletPrefab, transform);
        CreatedBullets.Add(bullet);
        bullet.CollisionDetected += ReturnToPool;
        return bullet;
    }

    protected override void OnDestroyBullet(EnemyBullet bullet)
    {
        bullet.CollisionDetected -= ReturnToPool;
        Destroy(bullet.gameObject);
    }
}