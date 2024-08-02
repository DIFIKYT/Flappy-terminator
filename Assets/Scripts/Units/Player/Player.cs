using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerCollisionHandler))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerShooter))]
public class Player : Character
{
    private PlayerMover _playerMover;
    private PlayerCollisionHandler _playerCollisionHandler;
    private PlayerInput _playerInput;
    private PlayerAnimation _playerAnimation;
    private PlayerShooter _playerShooter;

    public event Action GameOver;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
        _playerInput = GetComponent<PlayerInput>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerShooter = GetComponent<PlayerShooter>();
    }

    private void OnEnable()
    {
        _playerCollisionHandler.CollisionDetected += IdentifyCollision;
        _playerInput.JumpButtonPressed += Jump;
        _playerInput.ShootButtonPressed += Shoot;
    }

    private void OnDisable()
    {
        _playerCollisionHandler.CollisionDetected -= IdentifyCollision;
        _playerInput.JumpButtonPressed -= Jump;
        _playerInput.ShootButtonPressed -= Shoot;
    }

    private void IdentifyCollision(IInteractable interactable)
    {
        if (interactable is PlayerBullet)
            return;

        GameOver?.Invoke();
    }

    private void Jump()
    {
        _playerMover.ChangeRotation();
    }

    private void Shoot()
    {
        _playerShooter.Shoot(this);
    }

    public void Reset()
    {
        _playerMover.Reset();
    }
}