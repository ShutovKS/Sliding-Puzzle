#region

using System.Threading.Tasks;
using Data.PuzzleInformation;
using Infrastructure.ProjectStateMachine.Core;
using Services.Factories.UIFactory;
using UI.FoldingThePuzzle;
using Units.Image;
using Units.Piece;
using Units.PuzzleGenerator;
using Units.Time;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Infrastructure.ProjectStateMachine.States
{
    public class FoldingThePuzzleState : IState<Bootstrap>, IEnter<PuzzleInformation>, IExit, IInitialize
    {
        public FoldingThePuzzleState(Bootstrap initializer, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            Initializer = initializer;
        }

        public Bootstrap Initializer { get; }

        private readonly IUIFactory _uiFactory;
        private FoldingThePuzzlePuzzlesUI _foldingThePuzzlePuzzlesUI;
        private PiecesListTwoDimensional _piecesListTwoDimensional;
        private Stopwatch _timerWatch;

        private UnityAction<string> _onTimerUpdate;
        private Vector2Int[,] _currentPositions;
        private Vector2Int _emptyPosition;
        private Texture2D _texture2D;
        private Texture2D[,] _textures2D;
        private int _partsAmount;
        private bool _isStopTimer;

        public async Task OnInitialize()
        {
            await CreatedUI();
            _foldingThePuzzlePuzzlesUI.GameOverUI.OnBackClicked += ExitInMainMenu;
        }

        public void OnEnter(PuzzleInformation puzzleInformation)
        {
            _partsAmount = puzzleInformation.ElementsCount;
            _texture2D = puzzleInformation.Texture2D;
            _textures2D = null;

            if (puzzleInformation.Texture2D != null)
            {
                _textures2D = ImageCutter.CutImage(_texture2D, ImageCutterType.NumberOfParts, _partsAmount, _partsAmount);
            }

            _emptyPosition = new Vector2Int(_partsAmount - 1, 0);
            _currentPositions = GenerationPositions.GetRandomPositions(_partsAmount, _emptyPosition);
            _foldingThePuzzlePuzzlesUI.GameplayUI.SetActive(true);

            SetUpUI();

            InitialisePiecesListTwoDimensional();

            CreatedParts();

            _foldingThePuzzlePuzzlesUI.IsEnabled = true;

            TimerStart();
        }

        public void OnExit()
        {
            TimerStop();
            Clear();
            _foldingThePuzzlePuzzlesUI.IsEnabled = false;
        }

        #region UI

        private async Task CreatedUI()
        {
            _foldingThePuzzlePuzzlesUI = await _uiFactory.Created<FoldingThePuzzlePuzzlesUI>();
        }

        private void SetUpUI()
        {
            _foldingThePuzzlePuzzlesUI.GameOverUI.SetImage(_texture2D);
            _foldingThePuzzlePuzzlesUI.GameOverUI.SetActive(false);

            _foldingThePuzzlePuzzlesUI.ImageSampleUI.SetImageSample(_texture2D);

            _foldingThePuzzlePuzzlesUI.MenuUI.OnBackClicked += ExitInMainMenu;
            _foldingThePuzzlePuzzlesUI.MenuUI.OnResetClicked += ResetParts;
            _onTimerUpdate += _foldingThePuzzlePuzzlesUI.MenuUI.UpdateTimer;
        }

        #endregion

        #region Part

        private void CreatedParts()
        {
            _foldingThePuzzlePuzzlesUI.GameplayUI.CreatedParts(_partsAmount);
            _foldingThePuzzlePuzzlesUI.GameplayUI.RegisteringButtonsEvents(MovePart);

            if (_textures2D != null)
            {
                _foldingThePuzzlePuzzlesUI.GameplayUI.FillWithPartsOfCutsImages(_textures2D, _currentPositions);
            }
            else
            {
                _foldingThePuzzlePuzzlesUI.GameplayUI.FillWithPartsOfCutsNumbers(_currentPositions);
            }

            RemovePart(_emptyPosition);
        }

        private void ResetParts()
        {
            RemoveAllParts();
            InitialisePiecesListTwoDimensional();
            CreatedParts();
            _timerWatch.Reset();
        }

        private void MovePart(Vector2Int currentPosition)
        {
            if (_piecesListTwoDimensional.TryMovePiece(currentPosition, out var newPosition))
            {
                _foldingThePuzzlePuzzlesUI.GameplayUI.MovePart(currentPosition, newPosition, _partsAmount);
            }
        }

        private void RemoveAllParts()
        {
            var position = new Vector2Int(0, 0);
            for (var y = 0; y < _partsAmount; y++)
            {
                for (var x = 0; x < _partsAmount; x++)
                {
                    RemovePart(position);
                    position.x++;
                }

                position.x = 0;
                position.y++;
            }
        }

        private void RemovePart(Vector2Int position)
        {
            _foldingThePuzzlePuzzlesUI.GameplayUI.RemovePart(position);
            _piecesListTwoDimensional.RemovePiece(position);
        }

        #endregion

        #region Other

        private void InitialisePiecesListTwoDimensional()
        {
            _piecesListTwoDimensional = new PiecesListTwoDimensional(_partsAmount, _partsAmount);

            for (var y = 0; y < _partsAmount; y++)
            for (var x = 0; x < _partsAmount; x++)
            {
                var currentPosition = _currentPositions[y, x];

                var targetPosition = new Vector2Int(x, y);
                var piece = new Piece(targetPosition, currentPosition);
                _piecesListTwoDimensional.AddPiece(piece);
            }

            if (_piecesListTwoDimensional.IsAllPartsInPlace == false)
            {
                _piecesListTwoDimensional.OnAllPartsInPlace += OnAllPartsInPlace;
            }
            else
            {
                OnExit();
                OnEnter(new PuzzleInformation(null, _texture2D, _partsAmount));
            }
        }

        private void ExitInMainMenu()
        {
            Initializer.StateMachine.SwitchState<MainMenuState>();
        }

        private void OnAllPartsInPlace()
        {
            _foldingThePuzzlePuzzlesUI.GameplayUI.SetActive(false);
            _foldingThePuzzlePuzzlesUI.GameOverUI.SetActive(true);
        }

        private void Clear()
        {
            _piecesListTwoDimensional = null;

            _foldingThePuzzlePuzzlesUI.GameplayUI.Clear();

            _foldingThePuzzlePuzzlesUI.MenuUI.OnBackClicked -= ExitInMainMenu;
            _foldingThePuzzlePuzzlesUI.MenuUI.OnResetClicked -= ResetParts;
            _foldingThePuzzlePuzzlesUI.MenuUI.Clear();
        }

        private void TimerStart()
        {
            _isStopTimer = false;
            _timerWatch = new Stopwatch();
            _timerWatch?.Start();
            TimerUpdate();
        }

        private void TimerStop()
        {
            _timerWatch?.Stop();
            _isStopTimer = true;
        }

        private async void TimerUpdate()
        {
            while (_isStopTimer == false)
            {
                _onTimerUpdate?.Invoke(_timerWatch.GetElapsedTime().ToString(@"mm\:ss"));
                await Task.Delay(1000);
            }
        }

        #endregion
    }
}