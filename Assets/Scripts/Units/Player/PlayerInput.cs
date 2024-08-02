using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action JumpButtonPressed;
    public event Action ShootButtonPressed;

    private void Update()
    {
        if(Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpButtonPressed?.Invoke();
            }

            if(Input.GetKeyUp(KeyCode.Q))
            {
                ShootButtonPressed?.Invoke();
            }
        }
    }
}