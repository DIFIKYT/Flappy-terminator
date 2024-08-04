using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed;

    public event Action<Bullet> CollisitonDetected;

    protected float Speed => _speed;

    protected void NotifyCollision()
    {
        CollisitonDetected?.Invoke(this);
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);
    protected abstract void Update();
}