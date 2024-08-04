using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private PlayerBulletSpawner _playerBulletSpawner;

    public void Shoot(Player player)
    {
        _playerBulletSpawner.GetBullet(player);
    }
}