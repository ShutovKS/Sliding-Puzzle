#region

using Data.GameDifficulty;
using Data.GameType;
using Data.PlayerPrefs;
using UI.FoldingThePuzzle;
using Units.Image;
using Units.Piece;
using Units.PuzzleGenerator;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Units.ProcessControl
{
    public class FoldingThePuzzleController : MonoBehaviour
    {
        [SerializeField] private PartsUI _partsUI;
        [SerializeField] private FullImageUI _fullImageUI;
        [SerializeField] private ButtonsUI _buttonsUI;
        private Vector2Int[,] _currentPositions;
        private string _difficultyLevel;
        private Vector2Int _emptyPosition;
        private string _gameType;
        private int _partsAmount;

        private PiecesListTwoDimensional _piecesListTwoDimensional;

        private void Awake()
        {
            _difficultyLevel = PlayerPrefs.GetString(KeysForPlayerPrefs.DIFFICULTY_LEVEL_KEY);
            _gameType = PlayerPrefs.GetString(KeysForPlayerPrefs.GAME_TYPE_KEY);
            _partsAmount = NumberOfItemsPerDifficulties.GetPartsCountPerMode[_difficultyLevel];
            _emptyPosition = new Vector2Int(_partsAmount - 1, 0);
            _currentPositions = GenerationPositions.GetRandomPositions(_partsAmount, _emptyPosition);

            CreatedParts();

            _buttonsUI.RegisterExitButton(ExitInMainMenu);
            _buttonsUI.RegisterResetButton(ResetParts);
        }

        private void CreatedParts()
        {
            _piecesListTwoDimensional = InitialisePiecesListTwoDimensional();
            _piecesListTwoDimensional.RegisterOnAllPartsInPlace(EnableFullImagePanel);

            _partsUI.CreatedParts(_partsAmount);
            _partsUI.RegisteringButtonsEvents(MovePart);

            switch (_gameType)
            {
                case GameTypes.DEFAULT_GAME:
                    _partsUI.FillWithPartsOfCutsNumbers(_currentPositions);

                    break;
                case GameTypes.CUSTOM_GAME:
                    var pathToImage = PlayerPrefs.GetString(KeysForPlayerPrefs.IMAGE_PATH_KEY);
                    var texture2D = ImageLoader.GetImageByPath(pathToImage);
                    var textures2D = ImageCutter.CutImage(
                        texture2D,
                        ImageCutterType.NumberOfParts,
                        _partsAmount,
                        _partsAmount);

                    _partsUI.FillWithPartsOfCutsImages(textures2D, _currentPositions);
                    _fullImageUI.CreatedFullImage(texture2D);
                    break;
            }

            RemovePart(_emptyPosition);
        }

        private PiecesListTwoDimensional InitialisePiecesListTwoDimensional()
        {
            var listTwoDimensional = new PiecesListTwoDimensional(_partsAmount, _partsAmount);

            for (var y = 0; y < _partsAmount; y++)
            for (var x = 0; x < _partsAmount; x++)
            {
                var currentPosition = _currentPositions[y, x];

                var targetPosition = new Vector2Int(x, y);
                var piece = new Piece.Piece(targetPosition, currentPosition);
                listTwoDimensional.AddPiece(piece);
            }

            return listTwoDimensional;
        }

        private void MovePart(Vector2Int currentPosition)
        {
            if (_piecesListTwoDimensional.TryMovePiece(currentPosition, out var newPosition))
            {
                _partsUI.MovePart(currentPosition, newPosition, _partsAmount);
            }
        }

        private void EnableFullImagePanel()
        {
            _fullImageUI.PanelSetActive(true);
            _partsUI.PanelSetActive(false);
        }

        private void RemovePart(Vector2Int position)
        {
            _partsUI.RemovePart(position);
            _piecesListTwoDimensional.RemovePiece(position);
        }

        private void ResetParts()
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

            CreatedParts();
        }

        private void ExitInMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}