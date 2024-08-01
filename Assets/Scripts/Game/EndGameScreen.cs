using System;
using UnityEngine;

public class EndGameScreen : Window
{
    public event Action RestartButtonClicked;

    private void Awake()
    {
        Open();
    }

    public override void Close()
    {
        Debug.Log("EndGame Close");
        gameObject.SetActive(false);
        ActionButton.interactable = false;
    }

    public override void Open()
    {
        Debug.Log("EndGame Open");
        gameObject.SetActive(true);
        ActionButton.interactable = true;
    }

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}
