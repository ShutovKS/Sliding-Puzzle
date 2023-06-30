using Data.PlayerPrefs;
using Units.FileBrowser;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private ModeSettingPanel _modeSettingPanel;
        
        private void Start()
        {
            MainPanelRegisterAction();
        }

        #region Main Panel

        private void MainPanelRegisterAction()
        {
            _mainPanel.RegisterStartGameDefaultButtonListener(StartGameDefault);
            _mainPanel.RegisterStartGameCustomButtonListener(StartGameCustom);
            _mainPanel.RegisterExitButtonListener(ExitGame);
        }

        private void StartGameDefault()
        {
            _mainPanel.SetActive(false);
            _modeSettingPanel.SetActive(true);
            _modeSettingPanel.RemoveListeners();


            _modeSettingPanel.RegisterHardcoreButtonListener(HardcoreMode, StartGame);
            _modeSettingPanel.RegisterNormalButtonListener(NormalMode, StartGame);
            _modeSettingPanel.RegisterEasyButtonListener(EasyMode, StartGame);
            _modeSettingPanel.RegisterBackButtonListener(BackToMainPanel);
        }

        private void StartGameCustom()
        {
            _mainPanel.SetActive(false);
            _modeSettingPanel.SetActive(true);
            _modeSettingPanel.RemoveListeners();

            _modeSettingPanel.RegisterHardcoreButtonListener(HardcoreMode, OpenFileBrowser);
            _modeSettingPanel.RegisterNormalButtonListener(NormalMode, OpenFileBrowser);
            _modeSettingPanel.RegisterEasyButtonListener(EasyMode, OpenFileBrowser);
            _modeSettingPanel.RegisterBackButtonListener(BackToMainPanel);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        #endregion

        #region Gameplay Setting Panel

        private void HardcoreMode()
        {
            PlayerPrefs.SetString(KeysForPlayerPrefs.DIFFICULTY_LEVEL_KEY, GameModesTypes.HARDCORE_MODE);
        }

        private void NormalMode()
        {
            PlayerPrefs.SetString(KeysForPlayerPrefs.DIFFICULTY_LEVEL_KEY, GameModesTypes.NORMAL_MODE);
        }

        private void EasyMode()
        {
            PlayerPrefs.SetString(KeysForPlayerPrefs.DIFFICULTY_LEVEL_KEY, GameModesTypes.EASY_MODE);
        }

        private void BackToMainPanel()
        {
            _mainPanel.SetActive(true);
            _modeSettingPanel.SetActive(false);
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