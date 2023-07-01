#region

using Data.GameDifficulty;
using Data.GameType;
using Data.PlayerPrefs;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#endregion

namespace Units.ProcessControl
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private ModeSettingPanel _modeSettingPanel;

        private void Start()
        {
            MainPanelRegisterAction();
        }

        private void MainPanelRegisterAction()
        {
            UnityAction generalEventsForDefault = BackToMainPanel;
            generalEventsForDefault += StartGame;
            generalEventsForDefault += () => RegisterGameType(GameTypes.DEFAULT_GAME);
            _mainPanel.RegisterStartGameDefaultButtonListener(() => StartGameClick(generalEventsForDefault));

            UnityAction generalEventsForCustom = BackToMainPanel;
            generalEventsForCustom += OpenFileBrowser;
            generalEventsForCustom += () => RegisterGameType(GameTypes.CUSTOM_GAME);
            _mainPanel.RegisterStartGameCustomButtonListener(() => StartGameClick(generalEventsForCustom));

            _mainPanel.RegisterExitButtonListener(ExitGame);
        }

        private void StartGameClick(UnityAction generalEvent)
        {
            _mainPanel.SetActive(false);
            _modeSettingPanel.SetActive(true);
            _modeSettingPanel.RemoveListeners();

            _modeSettingPanel.RegisterHardcoreButtonListener(
                () => RegisterGameDifficulties(GameDifficulties.HARDCORE_MODE),
                generalEvent);

            _modeSettingPanel.RegisterNormalButtonListener(
                () => RegisterGameDifficulties(GameDifficulties.NORMAL_MODE),
                generalEvent);

            _modeSettingPanel.RegisterEasyButtonListener(
                () => RegisterGameDifficulties(GameDifficulties.EASY_MODE),
                generalEvent);

            _modeSettingPanel.RegisterBackButtonListener(BackToMainPanel);
        }

        private void BackToMainPanel()
        {
            _mainPanel.SetActive(true);
            _modeSettingPanel.SetActive(false);
        }

        private async void OpenFileBrowser()
        {
            var path = await FileBrowser.FileBrowser.GetFilePatchFromFileBrowser();

            if (path == null) return;

            PlayerPrefs.SetString(KeysForPlayerPrefs.IMAGE_PATH_KEY, path);
            StartGame();
        }

        private void RegisterGameType(string gameType)
        {
            PlayerPrefs.SetString(KeysForPlayerPrefs.GAME_TYPE_KEY, gameType);
        }

        private void RegisterGameDifficulties(string gameDifficulties)
        {
            PlayerPrefs.SetString(KeysForPlayerPrefs.DIFFICULTY_LEVEL_KEY, gameDifficulties);
        }

        private void StartGame()
        {
            SceneManager.LoadScene("FoldingThePuzzle");
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}