public class EnemyShooter : Shooter
{
    public void SetBulletSpawner(BulletSpawner<Bullet> bulletSpawner)
    {
        _bulletSpawner = bulletSpawner;
    }
}