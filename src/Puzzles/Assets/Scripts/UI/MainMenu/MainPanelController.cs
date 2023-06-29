using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainPanelController : MonoBehaviour
    {
        [SerializeField] private Button _startGameDefaultButton;
        [SerializeField] private Button _startGameCustomButton;
        [SerializeField] private Button _exitButton;

        public void SetUp(UnityAction startGameDefault, UnityAction startGameCustom, UnityAction exitGame)
        {
            _startGameDefaultButton.onClick.AddListener(startGameDefault);
            _startGameCustomButton.onClick.AddListener(startGameCustom);
            _exitButton.onClick.AddListener(exitGame);
        }
    }
}