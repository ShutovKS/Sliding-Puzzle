using System.Threading.Tasks;
using Data.AssetsAddressablesConstants;
using Data.PuzzleInformation;
using Infrastructure.ProjectStateMachine.Core;
using Services.Factories.AbstractFactory;
using Services.Factories.UIFactory;
using UI.InGameMenu;
using UnityEngine;

namespace Infrastructure.ProjectStateMachine.States
{
    public class InGameMenu : IState<Bootstrap>, IEnter, IExit
    {
        public InGameMenu(Bootstrap initializer, IUIFactory uiFactory, IAbstractFactory abstractFactory)
        {
            _uiFactory = uiFactory;
            _abstractFactory = abstractFactory;
            Initializer = initializer;
        }

        public Bootstrap Initializer { get; }
        private readonly IUIFactory _uiFactory;
        private readonly IAbstractFactory _abstractFactory;

        public async void OnEnter()
        {
            await CreatedUI();
        }

        public void OnExit()
        {
            _uiFactory.DestroyInGameMenuScreen();
        }

        private async Task CreatedUI()
        {
            var ui = await _uiFactory.CreatedInGameMenuScreen();
            if (ui.TryGetComponent<InGameMenuUI>(out var inGameMenuUI))
            {
                var puzzlesInformation = LoadPuzzlesInformation();

                foreach (var puzzleInformation in puzzlesInformation)
                {
                    var panel = await _abstractFactory.CreateInstance<GameObject>(
                        AssetsAddressablesConstants.PUZZLE_INFORMATION_SCREEN);

                    panel.GetComponent<PuzzleInformationUI>().SetUp(
                        puzzleInformation.Image,
                        puzzleInformation.Name,
                        puzzleInformation.ElementsCount,
                        () => OpenGame(puzzleInformation)
                    );

                    inGameMenuUI.AddPanelToScroll(panel.GetComponent<RectTransform>());
                }

                inGameMenuUI.RegisterBackButtonListener(BackInMainMenu);
            }
            else throw new System.Exception("InGameMenuUI not found");
        }

        private PuzzleInformation[] LoadPuzzlesInformation()
        {
            var puzzleInformation = Resources.LoadAll<PuzzleInformation>($"PuzzlesInformation");

            return puzzleInformation;
        }

        private void OpenGame(PuzzleInformation puzzleInformation)
        {
            Initializer.StateMachine.SwitchState<FoldingThePuzzleState, PuzzleInformation>(puzzleInformation);
        }

        private void BackInMainMenu()
        {
            Initializer.StateMachine.SwitchState<MainMenuState>();
        }
    }
}