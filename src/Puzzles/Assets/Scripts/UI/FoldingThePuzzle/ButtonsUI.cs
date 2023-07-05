#region

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace UI.FoldingThePuzzle
{
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
}