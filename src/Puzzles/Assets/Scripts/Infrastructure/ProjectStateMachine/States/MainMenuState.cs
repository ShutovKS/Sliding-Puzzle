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
            _uiFactory.DestroyMainMenuScreen();
        }

        public Bootstrap Initializer { get; }

        private async Task CreatedUI()
        {
            var mainMenuInstance = await _uiFactory.CreatedMainMenuScreen();

            if (mainMenuInstance.TryGetComponent<MainMenuUI>(out var mainMenuUI))
            {
                mainMenuUI.RegisterStartButtonListener(StartGame);
                mainMenuUI.RegisterSettingButtonListener(Setting);
                mainMenuUI.RegisterExitButtonListener(ExitFromGame);
            }
            else
            {
                throw new Exception("MainMenuUI not found");
            }

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
    }
}