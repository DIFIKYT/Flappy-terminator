using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    private BulletSpawner _bulletSpawner;

    private void Awake()
    {
        if (FindAnyObjectByType<BulletSpawner>() != null)
            _bulletSpawner = FindAnyObjectByType<BulletSpawner>();
    }

    public void Shoot(Enemy enemy)
    {
        _bulletSpawner.GetBullet(enemy);
    }
}