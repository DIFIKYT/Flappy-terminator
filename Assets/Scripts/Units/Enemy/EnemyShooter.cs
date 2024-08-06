using UnityEngine;

public class EnemyShooter : Shooter
{
    private EnemyBulletSpawner _enemyBulletSpawner;

    public void SetBulletSpawner(EnemyBulletSpawner bulletSpawner)
    {
        _enemyBulletSpawner = bulletSpawner;
    }

    public void Shoot(Enemy enemy)
    {
        _enemyBulletSpawner.GetBullet(enemy);
    }
}