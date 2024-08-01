using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemiesPrefabs;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _maxSpawnCoordinateY;
    [SerializeField] private float _minSpawnCoordinateY;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: CreateEnemy,
            actionOnGet: OnGetEnemy,
            actionOnRelease: OnReleaseEnemy,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Count)]);
        enemy.CollisionRemoverDetected += ReturnToPool;
        return enemy;
    }

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.transform.position = GetSpawnPosition();
        enemy.gameObject.SetActive(true);
    }

    private void OnReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(Enemy enemy)
    {
        enemy.CollisionRemoverDetected -= ReturnToPool;
        Destroy(enemy.gameObject);
    }

    private void ReturnToPool(Enemy enemy)
    {
        _pool.Release(enemy);
    }

    private Vector3 GetSpawnPosition()
    {
        return new Vector3(transform.position.x, Random.Range(_minSpawnCoordinateY, _maxSpawnCoordinateY), transform.position.z);
    }

    private IEnumerator SpawnCoroutine()
    {
        var spawnDelay = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            if (_pool != null)
                _pool.Get();

            yield return spawnDelay;
        }
    }

    public void Reset()
    {
        if (_pool != null)
        {
            _pool.Clear();

            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
                OnDestroyEnemy(enemy);
        }
    }
}