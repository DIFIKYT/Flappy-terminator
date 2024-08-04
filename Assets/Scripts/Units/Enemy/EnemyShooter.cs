using UnityEngine;

public class EnemyShooter : MonoBehaviour
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