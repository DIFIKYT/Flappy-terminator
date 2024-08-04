using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletSpawner : MonoBehaviour
{
    [SerializeField] private EnemyBullet _enemyBulletPrefab;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    private ObjectPool<EnemyBullet> _pool;
    private List<EnemyBullet> _createdBullets;

    private void Awake()
    {
        _pool = new ObjectPool<EnemyBullet>(
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
        foreach (EnemyBullet bullet in _createdBullets)
            _pool.Release(bullet);

        _createdBullets.Clear();
        _pool.Clear();
    }

    public EnemyBullet GetBullet(Enemy enemy)
    {
        EnemyBullet bullet = _pool.Get();
        bullet.transform.position = enemy.transform.position;
        bullet.transform.rotation = enemy.transform.rotation;
        return bullet;
    }

    private EnemyBullet CreateBullet()
    {
        EnemyBullet bullet = Instantiate(_enemyBulletPrefab);
        bullet.CollisionDetected += ReturnToPool;
        _createdBullets.Add(bullet);
        return bullet;
    }

    private void OnGetBullet(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(EnemyBullet bullet)
    {
        bullet.CollisionDetected -= ReturnToPool;
        Destroy(bullet.gameObject);
    }

    private void ReturnToPool(EnemyBullet bullet)
    {
        _pool.Release(bullet);
    }
}