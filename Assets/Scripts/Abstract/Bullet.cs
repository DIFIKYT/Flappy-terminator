using UnityEngine;

public abstract class Bullet : Interactable
{
    [SerializeField] private float _speed;

    protected float Speed => _speed;
}