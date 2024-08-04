using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBulletSpawner : MonoBehaviour
{
    [SerializeField] private PlayerBullet _playerBulletPrefab;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    private ObjectPool<PlayerBullet> _pool;
    private List<PlayerBullet> _createdBullets;

    private void Awake()
    {
        _pool = new ObjectPool<PlayerBullet>(
            createFunc: CreateBullet,
            actionOnGet: OnGetBullet,
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);
    }

    public void Reset()
    {
        foreach (PlayerBullet bullet in _createdBullets)
            _pool.Release(bullet);

        _createdBullets.Clear();
        _pool.Clear();
    }

    public PlayerBullet GetBullet(Player player)
    {
        PlayerBullet bullet = _pool.Get();
        bullet.transform.position = player.transform.position;
        bullet.transform.rotation = player.transform.rotation;
        return bullet;
    }

    private PlayerBullet CreateBullet()
    {
        PlayerBullet bullet = Instantiate(_playerBulletPrefab);
        bullet.CollisionDetected += ReturnToPool;
        _createdBullets.Add(bullet);
        return bullet;
    }

    private void OnGetBullet(PlayerBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(PlayerBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(PlayerBullet bullet)
    {
        bullet.CollisionDetected -= ReturnToPool;
        Destroy(bullet.gameObject);
    }

    private void ReturnToPool(PlayerBullet bullet)
    {
        _pool.Release(bullet);
    }
}