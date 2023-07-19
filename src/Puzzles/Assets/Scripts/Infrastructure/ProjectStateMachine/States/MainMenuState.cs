#region

using System;
using System.Threading.Tasks;
using Infrastructure.ProjectStateMachine.Core;
using Services.Factories.UIFactory;
using UI.MainMenu;
using UnityEngine;

#endregion

namespace Infrastructure.ProjectStateMachine.States
{
    public class MainMenuState : IState<Bootstrap>, IEnter, IExit
    {
        public MainMenuState(Bootstrap initializer, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            Initializer = initializer;
        }

        private readonly IUIFactory _uiFactory;

        public async void OnEnter()
        {
            await CreatedUI();
        }

        public void OnExit()
        {
            Clear();
            DestroyUI();
        }

        public Bootstrap Initializer { get; }

        private async Task CreatedUI()
        {
            GameObject mainMenuInstance;
            if (_uiFactory.MainMenuScreen == null)
            {
                mainMenuInstance = await _uiFactory.CreatedMainMenuScreen();
            }
            else
            {
                mainMenuInstance = _uiFactory.MainMenuScreen;
                _uiFactory.MainMenuScreen.SetActive(true);
            }

            if (!mainMenuInstance.TryGetComponent<MainMenuUI>(out var mainMenuUI))
                throw new Exception("MainMenuUI not found");

            mainMenuUI.RegisterStartButtonListener(StartGame);
            mainMenuUI.RegisterSettingButtonListener(Setting);
            mainMenuUI.RegisterExitButtonListener(ExitFromGame);

            return;

            void StartGame()
            {
                Initializer.StateMachine.SwitchState<InGameMenuState>();
            }

            void Setting()
            {
                Debug.Log("Setting");
            }

            void ExitFromGame()
            {
#if UNITY_EDITOR
                Debug.Log("Exit from game");
#else
                Application.Quit();
#endif
            }
        }

        private void DestroyUI()
        {
            _uiFactory.MainMenuScreen.SetActive(false);
        }

        private void Clear()
        {
            _uiFactory.MainMenuScreen.GetComponent<MainMenuUI>().Clear();
        }
    }
}