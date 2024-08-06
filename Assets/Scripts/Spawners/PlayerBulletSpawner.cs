public class PlayerBulletSpawner : BulletSpawner<PlayerBullet>
{
    protected override PlayerBullet CreateBullet()
    {
        PlayerBullet bullet = Instantiate(BulletPrefab, transform);
        CreatedBullets.Add(bullet);
        bullet.CollisionDetected += ReturnToPool;
        return bullet;
    }

    protected override void OnDestroyBullet(PlayerBullet bullet)
    {
        bullet.CollisionDetected -= ReturnToPool;
        Destroy(bullet.gameObject);
    }
}