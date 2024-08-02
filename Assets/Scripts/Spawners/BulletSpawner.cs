using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private PlayerBullet _playerBulletPrefab;
    [SerializeField] private EnemyBullet _enemyBulletPrefab;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    private ObjectPool<Bullet> _playerBulletPool;
    private ObjectPool<Bullet> _enemyBulletPool;

    private void Awake()
    {
        _playerBulletPool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(_playerBulletPrefab),
            actionOnGet: OnGetBullet,
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: Destroy,
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );

        _enemyBulletPool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(_enemyBulletPrefab),
            actionOnGet: OnGetBullet,
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: Destroy,
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    public Bullet GetBullet(Character character)
    {
        return character is Player ? _playerBulletPool.Get() : _enemyBulletPool.Get();
    }

    public void ReturnBullet(Bullet bullet, bool isPlayerBullet)
    {
        if (isPlayerBullet)
            _playerBulletPool.Release(bullet);
        else
            _enemyBulletPool.Release(bullet);
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}