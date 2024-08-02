using UnityEngine;

public abstract class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed;

    protected float Speed => _speed;
}