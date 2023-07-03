#region

using Data.GameDifficulty;
using Data.GameType;
using Data.PlayerPrefs;
using UI.FoldingThePuzzle;
using Units.Image;
using Units.Piece;
using Units.PuzzleGenerator;
using UnityEngine;

#endregion

namespace Units.ProcessControl
{
    public class FoldingThePuzzleController : MonoBehaviour
    {
        [SerializeField] private PartsUI _partsUI;
        [SerializeField] private FullImageUI _fullImageUI;

        private void Awake()
        {
            var difficultyLevel = PlayerPrefs.GetString(KeysForPlayerPrefs.DIFFICULTY_LEVEL_KEY);
            var partsAmount = NumberOfItemsPerDifficulties.GetPartsCountPerMode[difficultyLevel];
            var emptyPosition = new Vector2Int(partsAmount - 1, 0);
            var currentPositions = GenerationPositions.GetRandomPositions(partsAmount, emptyPosition);

            var piecesListTwoDimensional = InitialisePiecesListTwoDimensional(currentPositions);
            piecesListTwoDimensional.RegisterOnAllPartsInPlace(EnableFullImagePanel);

            _partsUI.CreatedParts(partsAmount);
            _partsUI.RegisteringButtonsEvents(MovePart);

            var gameType = PlayerPrefs.GetString(KeysForPlayerPrefs.GAME_TYPE_KEY);
            switch (gameType)
            {
                case GameTypes.DEFAULT_GAME:
                    _partsUI.FillWithPartsOfCutsNumbers(currentPositions);

                    break;
                case GameTypes.CUSTOM_GAME:
                    var pathToImage = PlayerPrefs.GetString(KeysForPlayerPrefs.IMAGE_PATH_KEY);
                    var texture2D = ImageLoader.GetImageByPath(pathToImage);
                    var textures2D = ImageCutter.CutImage(
                        texture2D,
                        ImageCutterType.NumberOfParts,
                        partsAmount,
                        partsAmount);

                    _partsUI.FillWithPartsOfCutsImages(textures2D, currentPositions);
                    _fullImageUI.CreatedFullImage(texture2D);
                    break;
            }

            RemovePart(emptyPosition);

            return;

            void MovePart(Vector2Int currentPosition)
            {
                if (piecesListTwoDimensional.TryMovePiece(currentPosition, out var newPosition))
                {
                    _partsUI.MovePart(currentPosition, newPosition, partsAmount);
                }
            }

            void EnableFullImagePanel()
            {
                _fullImageUI.PanelSetActive(true);
                _partsUI.PanelSetActive(false);
            }

            void RemovePart(Vector2Int position)
            {
                _partsUI.RemovePart(position);
                piecesListTwoDimensional.RemovePiece(position);
            }
        }

        private PiecesListTwoDimensional InitialisePiecesListTwoDimensional(Vector2Int[,] currentPositions)
        {
            var partsAmount = currentPositions.GetLength(0);
            var piecesListTwoDimensional = new PiecesListTwoDimensional(partsAmount, partsAmount);

            for (var y = 0; y < partsAmount; y++)
            for (var x = 0; x < partsAmount; x++)
            {
                var currentPosition = currentPositions[y, x];

                var targetPosition = new Vector2Int(x, y);
                var piece = new Piece.Piece(targetPosition, currentPosition);
                piecesListTwoDimensional.AddPiece(piece);
            }

            return piecesListTwoDimensional;
        }
    }
}