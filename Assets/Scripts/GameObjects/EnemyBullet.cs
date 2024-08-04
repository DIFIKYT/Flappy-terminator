using System;
using UnityEngine;

public class EnemyBullet : Bullet
{
    public event Action<EnemyBullet> CollisionDetected;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Remover _) || other.TryGetComponent(out Player _))
            CollisionDetected?.Invoke(this);
    }

    protected override void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}