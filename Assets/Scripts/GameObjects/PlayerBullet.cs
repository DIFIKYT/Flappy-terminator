using System;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public event Action<PlayerBullet> CollisionDetected;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Remover _) || other.TryGetComponent(out Enemy _))
            CollisionDetected?.Invoke(this);
    }

    protected override void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }
}