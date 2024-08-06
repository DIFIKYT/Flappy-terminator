using System;
using System.Collections;
using UnityEngine;

public class Enemy : Character, IInteractable
{
    [SerializeField] private float _shootDelay;

    private EnemyCollisionHandler _collisionHandler;
    private EnemyShooter _enemyShooter;

    public event Action<Enemy> CollisionRemoverDetected;

    public EnemyShooter EnemyShooter => _enemyShooter;

    private void Awake()
    {
        _collisionHandler = GetComponent<EnemyCollisionHandler>();
        _enemyShooter = GetComponent<EnemyShooter>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += IdentifyCollision;
        StartCoroutine(ShootCoroutine());
    }

    private void IdentifyCollision(IInteractable interactable)
    {
        if (interactable is Remover || interactable is PlayerBullet)
            CollisionRemoverDetected?.Invoke(this);
    }

    private IEnumerator ShootCoroutine()
    {
        var delay = new WaitForSeconds(_shootDelay);
        yield return null;

        while (true)
        {
            _enemyShooter.Shoot(this);
            yield return delay;
        }
    }
}