using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Remover remover) || other.TryGetComponent(out Player player))
            NotifyCollision();
    }

    protected override void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}