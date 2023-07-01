#region

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace UI.MainMenu
{
    public class ModeSettingPanel : MonoBehaviour
    {
        [SerializeField] private Button _hardcoreButton;
        [SerializeField] private Button _normalButton;
        [SerializeField] private Button _easyButton;
        [SerializeField] private Button _backButton;

        [SerializeField] private GameObject _modeSettingPanel;

        public void RegisterHardcoreButtonListener(params UnityAction[] actions)
        {
            foreach (var action in actions)
            {
                _hardcoreButton.onClick.AddListener(action);
            }
        }

        public void RegisterNormalButtonListener(params UnityAction[] actions)
        {
            foreach (var action in actions)
            {
                _normalButton.onClick.AddListener(action);
            }
        }

        public void RegisterEasyButtonListener(params UnityAction[] actions)
        {
            foreach (var action in actions)
            {
                _easyButton.onClick.AddListener(action);
            }
        }

        public void RegisterBackButtonListener(params UnityAction[] actions)
        {
            foreach (var action in actions)
            {
                _backButton.onClick.AddListener(action);
            }
        }

        public void RemoveListeners()
        {
            _hardcoreButton.onClick.RemoveAllListeners();
            _normalButton.onClick.RemoveAllListeners();
            _easyButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }

        public void SetActive(bool isActive)
        {
            _modeSettingPanel.SetActive(isActive);
        }
    }
}