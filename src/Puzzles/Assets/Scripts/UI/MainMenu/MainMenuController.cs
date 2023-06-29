using Data.PlayerPrefs;
using Units.FileBrowser;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainPanelController _mainPanelController;
        [SerializeField] private GameplaySettingPanelController _gameplaySettingPanelController;

        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _gameplaySettingPanel;

        private void Start()
        {
            MainPanelRegisterAction();
        }

        #region Main Panel

        private void MainPanelRegisterAction()
        {
            _mainPanelController.SetUp(StartGameDefault, StartGameCustom, ExitGame);
        }

        private void StartGameDefault()
        {
            _mainPanel.SetActive(false);
            _gameplaySettingPanel.SetActive(true);
            _gameplaySettingPanelController.RemoveListeners();

            UnityAction genericActions = null;
            genericActions += StartGame;

            _gameplaySettingPanelController.RegisterHardcoreButtonListener(HardcoreMode, genericActions);
            _gameplaySettingPanelController.RegisterNormalButtonListener(NormalMode, genericActions);
            _gameplaySettingPanelController.RegisterEasyButtonListener(EasyMode, genericActions);
            _gameplaySettingPanelController.RegisterBackButtonListener(BackToMainPanel);
        }

        private void StartGameCustom()
        {
            _mainPanel.SetActive(false);
            _gameplaySettingPanel.SetActive(true);
            _gameplaySettingPanelController.RemoveListeners();

            UnityAction genericActions = null;
            genericActions += OpenFileBrowser;
            genericActions += StartGame;

            _gameplaySettingPanelController.RegisterHardcoreButtonListener(HardcoreMode, genericActions);
            _gameplaySettingPanelController.RegisterNormalButtonListener(NormalMode, genericActions);
            _gameplaySettingPanelController.RegisterEasyButtonListener(EasyMode, genericActions);
            _gameplaySettingPanelController.RegisterBackButtonListener(BackToMainPanel);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        #endregion

        #region Gameplay Setting Panel

        private void HardcoreMode()
        {
            PlayerPrefs.SetString(KeysForPlayerPrefs.GAME_MODE_KEY, GameModesTypes.HARDCORE_MODE);
        }

        private void NormalMode()
        {
            PlayerPrefs.SetString(KeysForPlayerPrefs.GAME_MODE_KEY, GameModesTypes.NORMAL_MODE);
        }

        private void EasyMode()
        {
            PlayerPrefs.SetString(KeysForPlayerPrefs.GAME_MODE_KEY, GameModesTypes.EASY_MODE);
        }

        private void BackToMainPanel()
        {
            _mainPanel.SetActive(true);
            _gameplaySettingPanel.SetActive(false);
        }

        #endregion

        #region Other

        private async void OpenFileBrowser()
        {
            var path = await FileBrowser.GetFilePatchFromFileBrowser();

            if (path == null) return;

            PlayerPrefs.SetString(KeysForPlayerPrefs.IMAGE_PATH_KEY, path);
            StartGame();
        }

        private void StartGame()
        {
            SceneManager.LoadScene("FoldingThePuzzle");
        }

        #endregion
    }
}