using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private PlayerBullet _playerBulletPrefab;
    [SerializeField] private EnemyBullet _enemyBulletPrefab;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    private Vector3 _bulletSpawnPosition;
    private Quaternion _bulletMoveDirection;
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
            createFunc: OnCreateBullet,
            actionOnGet: OnGetBullet,
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    public void Reset()
    {
        _playerBulletPool.Clear();
        _enemyBulletPool.Clear();

        foreach (Bullet bullet in FindObjectsOfType<Bullet>())
            OnDestroyBullet(bullet);
    }

    public Bullet GetBullet(Character character)
    {
        _bulletSpawnPosition = character.transform.position;
        _bulletMoveDirection = character.transform.rotation;
        return character is Player ? _playerBulletPool.Get() : _enemyBulletPool.Get();
    }

    private Bullet OnCreateBullet()
    {
        Bullet bullet = Instantiate(_enemyBulletPrefab, transform);
        bullet.CollisitonDetected += ReturnBullet;
        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.transform.position = _bulletSpawnPosition;
        bullet.transform.rotation = _bulletMoveDirection;
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        bullet.CollisitonDetected -= ReturnBullet;
        Destroy(bullet.gameObject);
    }

    private void ReturnBullet(Bullet bullet)
    {
        if (bullet is PlayerBullet)
            _playerBulletPool.Release(bullet);
        else
            _enemyBulletPool.Release(bullet);
    }
}