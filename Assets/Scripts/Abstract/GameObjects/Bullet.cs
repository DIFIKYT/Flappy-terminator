using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed;

    protected float Speed => _speed;

    protected abstract void OnTriggerEnter2D(Collider2D other);
    protected abstract void Update();
}