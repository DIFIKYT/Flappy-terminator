using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _moveRight;

    private Vector3 _moveDirection;

    public event Action<Bullet> CollisionDetected;

    private void OnEnable()
    {
        if (_moveRight)
            _moveDirection = Vector3.right;
        else
            _moveDirection = Vector3.left;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Remover _) || other.TryGetComponent(out Character _))
            CollisionDetected?.Invoke(this);
    }

    private void Update()
    {
        transform.Translate(_moveDirection * _speed * Time.deltaTime);
    }
}