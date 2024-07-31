using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyCollisionHandler : MonoBehaviour
{
    public event Action<Interactable> CollisionDetected;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Interactable interactable))
            CollisionDetected?.Invoke(interactable);
    }
}