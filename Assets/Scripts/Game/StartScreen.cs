using System;
using UnityEngine;

public class StartScreen : Window
{
    public event Action PlayButtonClicked;

    private void Awake()
    {
        Open();
    }

    public override void Close()
    {
        Debug.Log("StartGame Close");
        gameObject.SetActive(false);
        ActionButton.interactable = false;
    }

    public override void Open()
    {
        Debug.Log("StartGame Open");
        gameObject.SetActive(true);
        ActionButton.interactable = true;
    }

    protected override void OnButtonClick()
    {
        PlayButtonClicked?.Invoke();
    }
}