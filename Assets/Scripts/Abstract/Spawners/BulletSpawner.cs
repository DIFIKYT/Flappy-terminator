using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner<T> : MonoBehaviour where T : Bullet
{
    [SerializeField] private T _bulletPrefab;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    private ObjectPool<T> _pool;
    private List<T> _createdBullets = new();

    protected T BulletPrefab => _bulletPrefab;
    protected List<T> CreatedBullets => _createdBullets;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
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
        foreach (T bullet in _createdBullets)
        {
            if (bullet.gameObject.activeSelf)
            {
                _pool.Release(bullet);
            }
        }

        _createdBullets.Clear();
        _pool.Clear();
    }

    protected virtual T CreateBullet()
    {
        T bullet = Instantiate(_bulletPrefab, transform);
        _createdBullets.Add(bullet);
        return bullet;
    }

    protected virtual void OnDestroyBullet(T bullet)
    {
        Destroy(bullet.gameObject);
    }

    protected void ReturnToPool(T bullet)
    {
        _pool.Release(bullet);
    }

    public T GetBullet(Character character)
    {
        T bullet = _pool.Get();
        bullet.transform.position = character.transform.position;
        bullet.transform.rotation = character.transform.rotation;
        return bullet;
    }

    private void OnGetBullet(T bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(T bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}