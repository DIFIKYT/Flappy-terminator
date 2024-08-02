using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyCollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> CollisionDetected;
    public event Action<Enemy> EnemyDetected;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IInteractable interactable))
            CollisionDetected?.Invoke(interactable);
        if(other.TryGetComponent(out Enemy enemy))
            EnemyDetected?.Invoke(enemy);
    }
}