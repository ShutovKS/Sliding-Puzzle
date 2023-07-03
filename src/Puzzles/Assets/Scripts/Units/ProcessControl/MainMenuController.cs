#region

using Data.GameDifficulty;
using Data.GameType;
using Data.PlayerPrefs;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

#endregion

namespace Units.ProcessControl
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainUI _mainUI;
        [SerializeField] private ModeSettingUI _modeSettingUI;

        private void Start()
        {
            MainPanelRegisterAction();
        }

        private void MainPanelRegisterAction()
        {
            UnityAction generalEventsForDefault = BackToMainPanel;
            generalEventsForDefault += StartGame;
            generalEventsForDefault += () => RegisterGameType(GameTypes.DEFAULT_GAME);
            _mainUI.RegisterStartGameDefaultButtonListener(() => StartGameClick(generalEventsForDefault));

            UnityAction generalEventsForCustom = BackToMainPanel;
            generalEventsForCustom += OpenFileBrowser;
            generalEventsForCustom += () => RegisterGameType(GameTypes.CUSTOM_GAME);
            _mainUI.RegisterStartGameCustomButtonListener(() => StartGameClick(generalEventsForCustom));

            _mainUI.RegisterExitButtonListener(ExitGame);
        }

        private void StartGameClick(UnityAction generalEvent)
        {
            _mainUI.PanelSetActive(false);
            _modeSettingUI.PanelSetActive(true);
            _modeSettingUI.RemoveListeners();

            _modeSettingUI.RegisterHardcoreButtonListener(
                () => RegisterGameDifficulties(GameDifficulties.HARDCORE_MODE),
                generalEvent);

            _modeSettingUI.RegisterNormalButtonListener(
                () => RegisterGameDifficulties(GameDifficulties.NORMAL_MODE),
                generalEvent);

            _modeSettingUI.RegisterEasyButtonListener(
                () => RegisterGameDifficulties(GameDifficulties.EASY_MODE),
                generalEvent);

            _modeSettingUI.RegisterBackButtonListener(BackToMainPanel);
        }

        private void BackToMainPanel()
        {
            _mainUI.PanelSetActive(true);
            _modeSettingUI.PanelSetActive(false);
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