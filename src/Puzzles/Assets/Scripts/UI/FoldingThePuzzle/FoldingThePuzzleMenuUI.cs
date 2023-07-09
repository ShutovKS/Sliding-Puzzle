using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.FoldingThePuzzle
{
    public class FoldingThePuzzleMenuUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _exitButton;

        public void RegisterResetListener(UnityAction action)
        {
            _resetButton.onClick.AddListener(action);
        }

        public void RegisterExitListener(UnityAction action)
        {
            _exitButton.onClick.AddListener(action);
        }

        public void UpdateTimer(string text)
        {
            _timerText.text = text;
        }
    }
}