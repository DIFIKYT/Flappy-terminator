using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;

    public void Shoot(Player player)
    {
        _bulletSpawner.GetBullet(player);
    }
}