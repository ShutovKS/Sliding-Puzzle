#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.PuzzleInformation;
using Infrastructure.ProjectStateMachine.Core;
using Services.Factories.UIFactory;
using Services.LoadPuzzlesCatalogData;
using UI.InGameMenu;

#endregion

namespace Infrastructure.ProjectStateMachine.States
{
    public class InGameMenuState : IState<Bootstrap>, IEnter<int>, IExit, IInitialize
    {
        public InGameMenuState(Bootstrap initializer, IUIFactory uiFactory, ILoadPuzzlesCatalogData loadPuzzlesCatalogData)
        {
            Initializer = initializer;
            _uiFactory = uiFactory;
            _loadPuzzlesCatalogData = loadPuzzlesCatalogData;
        }

        public Bootstrap Initializer { get; }
        private readonly IUIFactory _uiFactory;
        private readonly ILoadPuzzlesCatalogData _loadPuzzlesCatalogData;

        private Dictionary<string, PuzzleInformation> _puzzlesInformation;
        private InGameMenuUI _inGameMenuUI;

        private string _currentKey;
        private int _currentNumber;

        public async Task OnInitialize()
        {
            await CreatedUI();
            
            var puzzleInformations = GetPuzzlesInfo();
            _puzzlesInformation = puzzleInformations.ToDictionary(info => info.Id, info => info);
            _inGameMenuUI.PuzzlesScroll.CreatedPanel(puzzleInformations);
            
            _inGameMenuUI.OnBackClicked += BackInMainMenu;
            _inGameMenuUI.PuzzlesScroll.OnPuzzleClicked += ClickGameImage;
            _inGameMenuUI.NumberParts.OnBackClicked += () => _inGameMenuUI.NumberParts.SetActive(false);
            _inGameMenuUI.NumberParts.OnCompleteClicked += StartGameImage;
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
            _inGameMenuUI.NumberParts.SetActive(false);
            _currentKey = null;
            _currentNumber = 0;
            ClosePuzzlesInfoUI();
            _inGameMenuUI.IsEnabled = false;
        }

        private async Task CreatedUI()
        {
            _inGameMenuUI = await _uiFactory.Created<InGameMenuUI>();
        }

        private void OpenPuzzlesInfoUI()
        {
            _inGameMenuUI.PuzzlesScroll.OpenPanel();
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

        private void ClickGameImage(string key)
        {
            _currentKey = key;

            _inGameMenuUI.NumberParts.SetActive(true);
        }

        private void StartGameImage(int number)
        {
            _currentNumber = number;

            var puzzleInformation = _puzzlesInformation[_currentKey];
            puzzleInformation.ElementsCount = _currentNumber;

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