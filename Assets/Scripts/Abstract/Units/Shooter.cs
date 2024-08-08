using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] protected BulletSpawner<Bullet> _bulletSpawner;

    public void Shoot(Character character)
    {
        _bulletSpawner.GetBullet(character);
    }
}