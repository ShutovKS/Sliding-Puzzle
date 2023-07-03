using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonsUI : MonoBehaviour
{
    [SerializeField] private Button _exitButton;

    public void RegisterExitButton(UnityAction action)
    {
        _exitButton.onClick.AddListener(action);
    }
}