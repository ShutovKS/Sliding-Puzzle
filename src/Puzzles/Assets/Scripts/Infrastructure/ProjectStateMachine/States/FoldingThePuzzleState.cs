#region

using System.Diagnostics;
using System.Threading.Tasks;
using Data.PuzzleInformation;
using Infrastructure.ProjectStateMachine.Core;
using Services.Factories.UIFactory;
using UI.FoldingThePuzzle;
using Units.Image;
using Units.Piece;
using Units.PuzzleGenerator;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

#endregion

namespace Infrastructure.ProjectStateMachine.States
{
    public class FoldingThePuzzleState : IState<Bootstrap>, IEnter<PuzzleInformation>, IExit
    {
        public FoldingThePuzzleState(Bootstrap initializer, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            Initializer = initializer;
        }

        private readonly IUIFactory _uiFactory;
        private Vector2Int[,] _currentPositions;
        private Vector2Int _emptyPosition;
        private GameObject _foldingThePuzzleInstance;
        private FoldingThePuzzleGameOverUI _gameOverUI;

        private FoldingThePuzzleImageSampleUI _imageSampleUI;
        private bool _isStopTimer;
        private FoldingThePuzzleMenuUI _menuUI;

        private UnityAction<string> _onTimerUpdate;
        private int _partsAmount;
        private PiecesListTwoDimensional _piecesListTwoDimensional;
        private FoldingThePuzzlePuzzlesUI _puzzlesUI;
        private Texture2D _texture2D;
        private Texture2D[,] _textures2D;

        private Stopwatch _timerWatch;

        public async void OnEnter(PuzzleInformation puzzleInformation)
        {
            _partsAmount = puzzleInformation.ElementsCount;
            _texture2D = puzzleInformation.Image;

            _textures2D = ImageCutter.CutImage(
                _texture2D,
                ImageCutterType.NumberOfParts,
                _partsAmount,
                _partsAmount);

            _emptyPosition = new Vector2Int(_partsAmount - 1, 0);
            _currentPositions = GenerationPositions.GetRandomPositions(_partsAmount, _emptyPosition);

            InitialisePiecesListTwoDimensional();

            await CreatedUI();
            CreatedParts();
            TimerStart();
        }

        public void OnExit()
        {
            TimerStop();
            Clear();
            DestroyUI();
        }

        public Bootstrap Initializer { get; }

        #region UI

        private async Task CreatedUI()
        {
            if (_uiFactory.FoldingThePuzzle == null)
            {
                _foldingThePuzzleInstance = await _uiFactory.CreatedFoldingThePuzzle();
            }
            else
            {
                _uiFactory.FoldingThePuzzle.SetActive(true);
                _foldingThePuzzleInstance = _uiFactory.FoldingThePuzzle;
            }

            _imageSampleUI = _foldingThePuzzleInstance.GetComponentInChildren<FoldingThePuzzleImageSampleUI>();
            _gameOverUI = _foldingThePuzzleInstance.GetComponentInChildren<FoldingThePuzzleGameOverUI>();
            _puzzlesUI = _foldingThePuzzleInstance.GetComponentInChildren<FoldingThePuzzlePuzzlesUI>();
            _menuUI = _foldingThePuzzleInstance.GetComponentInChildren<FoldingThePuzzleMenuUI>();

            SetUpGameOverUI();
            SetUpImageSampleUI();
            SetUpMenuUI();
        }

        private void DestroyUI()
        {
            _foldingThePuzzleInstance.SetActive(false);
        }

        private void SetUpGameOverUI()
        {
            _gameOverUI.SetImage(_texture2D);
            _gameOverUI.RegisterExitListener(ExitInMainMenu);
            _gameOverUI.SetActiveFullImagePanel(false);
        }

        private void SetUpImageSampleUI()
        {
            _imageSampleUI.SetImageSample(_texture2D);
        }

        private void SetUpMenuUI()
        {
            _menuUI.RegisterExitListener(ExitInMainMenu);
            _menuUI.RegisterResetListener(ResetParts);
            _onTimerUpdate += _menuUI.UpdateTimer;
        }

        private void OnAllPartsInPlace()
        {
            TimerStop();
            _gameOverUI.SetActiveFullImagePanel(true);
        }

        #endregion

        #region Part

        private void CreatedParts()
        {
            _puzzlesUI.CreatedParts(_partsAmount);
            _puzzlesUI.RegisteringButtonsEvents(MovePart);
            _puzzlesUI.FillWithPartsOfCutsImages(_textures2D, _currentPositions);
            RemovePart(_emptyPosition);
        }

        private void ResetParts()
        {
            RemoveAllParts();
            InitialisePiecesListTwoDimensional();
            CreatedParts();
            _timerWatch.Restart();
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
            _puzzlesUI.RemovePart(position);
            _piecesListTwoDimensional.RemovePiece(position);
        }

        private void MovePart(Vector2Int currentPosition)
        {
            if (_piecesListTwoDimensional.TryMovePiece(currentPosition, out var newPosition))
            {
                _puzzlesUI.MovePart(currentPosition, newPosition, _partsAmount);
            }
        }

        #endregion

        #region Other

        private void InitialisePiecesListTwoDimensional()
        {
            _piecesListTwoDimensional = new PiecesListTwoDimensional(_partsAmount, _partsAmount);
            _piecesListTwoDimensional.RegisterOnAllPartsInPlace(OnAllPartsInPlace);

            for (var y = 0; y < _partsAmount; y++)
            for (var x = 0; x < _partsAmount; x++)
            {
                var currentPosition = _currentPositions[y, x];

                var targetPosition = new Vector2Int(x, y);
                var piece = new Piece(targetPosition, currentPosition);
                _piecesListTwoDimensional.AddPiece(piece);
            }
        }

        private void ExitInMainMenu()
        {
            Initializer.StateMachine.SwitchState<MainMenuState>();
        }

        private void Clear()
        {
            _piecesListTwoDimensional = null;
            _imageSampleUI.Clear();
            _gameOverUI.Clear();
            _puzzlesUI.Clear();
            _menuUI.Clear();
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
                _onTimerUpdate?.Invoke(_timerWatch.Elapsed.ToString(@"mm\:ss"));
                await Task.Delay(1000);
            }
        }

        #endregion
    }
}