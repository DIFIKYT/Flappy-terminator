using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action JumpKeyPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            JumpKeyPressed?.Invoke();
    }
}