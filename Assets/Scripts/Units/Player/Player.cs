using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerCollisionHandler))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerAnimation))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerCollisionHandler _playerCollisionHandler;
    private PlayerInput _playerInput;
    private PlayerAnimation _playerAnimation;

    public event Action GameOver;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
        _playerInput = GetComponent<PlayerInput>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        _playerCollisionHandler.CollisionDetected += IdentifyCollision;
        _playerInput.JumpKeyPressed += Jump;
    }

    private void OnDisable()
    {
        _playerCollisionHandler.CollisionDetected -= IdentifyCollision;
        _playerInput.JumpKeyPressed -= Jump;
    }

    private void IdentifyCollision(Interactable interactable)
    {
        if (interactable is PlayerBullet)
            return;

        GameOver?.Invoke();
    }

    private void Jump()
    {
        _playerMover.ChangeRotation();
    }

    public void Reset()
    {
        _playerMover.Reset();
    }
}