using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Remover _) || other.TryGetComponent(out Player _))
            NotifyCollision();
    }

    protected override void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}