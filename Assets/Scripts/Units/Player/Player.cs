using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : Character
{
    [SerializeField] private float _shootReloadTime = 1;

    private CollisionHandler _collisionHandler;
    private PlayerShooter _playerShooter;
    private PlayerMover _playerMover;
    private PlayerInput _playerInput;
    private bool _canShoot = true;

    public event Action GameOver;

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _playerShooter = GetComponent<PlayerShooter>();
        _playerMover = GetComponent<PlayerMover>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += IdentifyCollision;
        _playerInput.JumpButtonPressed += Jump;
        _playerInput.ShootButtonPressed += Shoot;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= IdentifyCollision;
        _playerInput.JumpButtonPressed -= Jump;
        _playerInput.ShootButtonPressed -= Shoot;
    }

    public void Reset()
    {
        _playerMover.Reset();
    }

    private void IdentifyCollision(IInteractable interactable)
    {
        GameOver?.Invoke();
    }

    private void Jump()
    {
        _playerMover.ChangeRotationAndspeed();
    }

    private void Shoot()
    {
        if (_canShoot)
        {
            StartCoroutine(Reload());
            _playerShooter.Shoot(this);
        }
    }

    private IEnumerator Reload()
    {
        var shootReloadTime = new WaitForSeconds(_shootReloadTime);
        _canShoot = false;
        yield return shootReloadTime;
        _canShoot = true;
    }
}