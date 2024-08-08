using System;
using System.Collections;
using UnityEngine;

public class Player : Character
{
    private PlayerMover _playerMover;
    private CollisionHandler _collisionHandler;
    private PlayerInput _playerInput;
    private PlayerShooter _playerShooter;
    private float _shootReloadTime = 2;
    private bool _canShoot = true;

    public event Action GameOver;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _playerInput = GetComponent<PlayerInput>();
        _playerShooter = GetComponent<PlayerShooter>();
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
            _playerShooter.Shoot(this);
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        var shootReloadTime = new WaitForSeconds(_shootReloadTime);
        _canShoot = false;

        while (true)
        {
            yield return shootReloadTime;
            _canShoot = true;
        }
    }
}