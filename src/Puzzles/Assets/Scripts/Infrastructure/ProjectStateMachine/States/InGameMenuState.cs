#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.PuzzleInformation;
using Infrastructure.ProjectStateMachine.Core;
using Services.Factories.AbstractFactory;
using Services.Factories.UIFactory;
using Services.LoadPuzzlesCatalogData;
using UI.InGameMenu;

#endregion

namespace Infrastructure.ProjectStateMachine.States
{
    public class InGameMenuState : IState<Bootstrap>, IEnter<int>, IExit, IInitialize
    {
        public InGameMenuState(Bootstrap initializer, IUIFactory uiFactory, IAbstractFactory abstractFactory,
            ILoadPuzzlesCatalogData loadPuzzlesCatalogData)
        {
            Initializer = initializer;
            _uiFactory = uiFactory;
            _abstractFactory = abstractFactory;
            _loadPuzzlesCatalogData = loadPuzzlesCatalogData;
        }

        public Bootstrap Initializer { get; }
        private readonly IUIFactory _uiFactory;
        private readonly IAbstractFactory _abstractFactory;
        private readonly ILoadPuzzlesCatalogData _loadPuzzlesCatalogData;

        private Dictionary<string, PuzzleInformation> _puzzlesInformation;
        private InGameMenuUI _inGameMenuUI;

        public async Task OnInitialize()
        {
            await CreatedUI();
            _inGameMenuUI.OnBackClicked += BackInMainMenu;
            _inGameMenuUI.PuzzlesScroll.OnPuzzleClicked += StartGameImage;
        }

        public void OnEnter(int number)
        {
            if (number != 0)
            {
                StartGameNumber(number);
            }
            else
            {
                OpenPuzzlesInfoUI();
            }
        }

        public void OnExit()
        {
            ClosePuzzlesInfoUI();
            _inGameMenuUI.IsEnabled = false;
        }

        private async Task CreatedUI()
        {
            _inGameMenuUI = await _uiFactory.Created<InGameMenuUI>();
        }

        private void OpenPuzzlesInfoUI()
        {
            if (_puzzlesInformation != null)
            {
                _inGameMenuUI.PuzzlesScroll.OpenPanel();
            }
            else
            {
                var puzzleInformations = GetPuzzlesInfo();
                _puzzlesInformation = puzzleInformations.ToDictionary(info => info.Id, info => info);
                _inGameMenuUI.PuzzlesScroll.CreatedPanel(_abstractFactory, puzzleInformations);
            }

            _inGameMenuUI.IsEnabled = true;
        }

        private void ClosePuzzlesInfoUI()
        {
            _inGameMenuUI.PuzzlesScroll.CloseOpenPanel();
        }

        private PuzzleInformation[] GetPuzzlesInfo()
        {
            var puzzlesInformation = _loadPuzzlesCatalogData.GetPuzzlesInformation();

            return puzzlesInformation.ToArray();
        }

        private void StartGameImage(string key)
        {
            var puzzleInformation = _puzzlesInformation[key];
            puzzleInformation.ElementsCount = 2;

            Initializer.StateMachine.SwitchState<FoldingThePuzzleState, PuzzleInformation>(puzzleInformation);
        }

        private void StartGameNumber(int number)
        {
            var puzzleInformation = new PuzzleInformation(null, null, number);
            
            Initializer.StateMachine.SwitchState<FoldingThePuzzleState, PuzzleInformation>(puzzleInformation);
        }

        private void BackInMainMenu()
        {
            Initializer.StateMachine.SwitchState<MainMenuState>();
        }
    }
}