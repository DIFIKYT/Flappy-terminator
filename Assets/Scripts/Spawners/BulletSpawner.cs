using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BulletSpawner<T> : MonoBehaviour where T : Bullet
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
            createFunc: Create,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: Destroy,
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

    public T GetBullet(Character character)
    {
        T bullet = _pool.Get();
        bullet.transform.position = character.transform.position;
        bullet.transform.rotation = character.transform.rotation;
        return bullet;
    }

    private T Create()
    {
        T bullet = Instantiate(BulletPrefab, transform);
        CreatedBullets.Add(bullet);
        bullet.CollisionDetected += ReturnToPool;
        return bullet;
    }

    private void OnGet(T bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnRelease(T bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void Destroy(T bullet)
    {
        bullet.CollisionDetected -= ReturnToPool;
        Destroy(bullet.gameObject);
    }

    private void ReturnToPool(Bullet bullet)
    {
        if (bullet is T tBullet)
        {
            _pool.Release(tBullet);
        }
    }
}