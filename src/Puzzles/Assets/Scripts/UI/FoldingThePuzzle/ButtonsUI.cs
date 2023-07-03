using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonsUI : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _exitButton;

    public void RegisterExitButton(UnityAction action)
    {
        _exitButton.onClick.AddListener(action);
    }
    
    public void RegisterResetButton(UnityAction action)
    {
        _resetButton.onClick.AddListener(action);
    }
}