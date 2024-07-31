using System;
using UnityEngine;

[RequireComponent(typeof(EnemyCollisionHandler))]
[RequireComponent(typeof(EnemyAnimation))]
public class Enemy : MonoBehaviour
{
    private EnemyCollisionHandler _collisionHandler;
    private EnemyAnimation _enemyAnimation;

    public event Action<Enemy> CollisionRemoverDetected;

    private void Awake()
    {
        _collisionHandler = GetComponent<EnemyCollisionHandler>();
        _enemyAnimation = GetComponent<EnemyAnimation>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += IdentifyCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= IdentifyCollision;
    }

    private void IdentifyCollision(Interactable interactable)
    {
        if (interactable.TryGetComponent(out Remover remover))
            CollisionRemoverDetected?.Invoke(this);
    }
}