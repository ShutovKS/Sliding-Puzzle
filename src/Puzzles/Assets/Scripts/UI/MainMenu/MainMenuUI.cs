#region

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel;

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _exitButton;

        public void RegisterStartButtonListener(UnityAction action)
        {
            _startButton.onClick.AddListener(action);
        }

        public void RegisterSettingButtonListener(UnityAction action)
        {
            _settingButton.onClick.AddListener(action);
        }

        public void RegisterExitButtonListener(UnityAction action)
        {
            _exitButton.onClick.AddListener(action);
        }

        public void RemoveAllListeners()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        public void PanelSetActive(bool isActive)
        {
            _mainPanel.SetActive(isActive);
        }
    }
}