#region

using System.Threading.Tasks;
using Infrastructure.ProjectStateMachine.Core;
using Services.Factories.UIFactory;
using UI.MainMenu;
using UnityEngine;

#endregion

namespace Infrastructure.ProjectStateMachine.States
{
    public class MainMenuState : IState<Bootstrap>, IEnter, IExit, IInitialize
    {
        public MainMenuState(Bootstrap initializer, IUIFactory uiFactory)
        {
            Initializer = initializer;
            _uiFactory = uiFactory;
        }

        public Bootstrap Initializer { get; }

        private readonly IUIFactory _uiFactory;
        private MainMenuUI _mainMenuUI;

        public async Task OnInitialize()
        {
            await CreatedMenu();
        }

        public void OnEnter()
        {
            _mainMenuUI.IsEnabled = true;
        }

        public void OnExit()
        {
            _mainMenuUI.IsEnabled = false;
        }

        private async Task CreatedMenu()
        {
            _mainMenuUI = await _uiFactory.Created<MainMenuUI>();
            _mainMenuUI.OnExitClicked = OnQuit;
            _mainMenuUI.OnInstructionsClicked = OnInstructions;
            _mainMenuUI.OnLevelClicked = OpenMenuInGame;
        }

        private static void OnQuit()
        {
#if UNITY_EDITOR
            Debug.Log("Выход из игры.");
#else
            Application.Quit();
#endif
        }

        private void OnInstructions()
        {
            Debug.Log($"Открытие меню.");
        }

        private void OpenMenuInGame(int number)
        {
            Initializer.StateMachine.SwitchState<InGameMenuState, int>(number);
        }
    }
}