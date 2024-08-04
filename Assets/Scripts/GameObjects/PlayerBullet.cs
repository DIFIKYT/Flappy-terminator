using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Remover _) || other.TryGetComponent(out Enemy _))
            NotifyCollision();
    }

    protected override void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }
}