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

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
        _playerInput = GetComponent<PlayerInput>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        _playerInput.JumpKeyPressed += Jump;
    }

    private void OnDisable()
    {
        _playerInput.JumpKeyPressed -= Jump;
    }

    private void IdentifyCollision()
    {

    }

    private void Jump()
    {
        _playerMover.ChangeRotation();
    }
}