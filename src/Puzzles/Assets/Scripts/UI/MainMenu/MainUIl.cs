using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Button _startGameDefaultButton;
        [SerializeField] private Button _startGameCustomButton;
        [SerializeField] private Button _exitButton;

        public void RegisterStartGameDefaultButtonListener(UnityAction action)
        {
            _startGameDefaultButton.onClick.AddListener(action);
        }

        public void RegisterStartGameCustomButtonListener(UnityAction action)
        {
            _startGameCustomButton.onClick.AddListener(action);
        }

        public void RegisterExitButtonListener(UnityAction action)
        {
            _exitButton.onClick.AddListener(action);
        }

        public void RemoveListeners()
        {
            _startGameDefaultButton.onClick.RemoveAllListeners();
            _startGameCustomButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        public void PanelSetActive(bool isActive)
        {
            _mainPanel.SetActive(isActive);
        }
    }
}