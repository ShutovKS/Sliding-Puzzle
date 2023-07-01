#region

using System;
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
        [SerializeField] private PartsPanel _partsPanel;

        private void Awake()
        {
            var gameType = PlayerPrefs.GetString(KeysForPlayerPrefs.GAME_TYPE_KEY);
            var pathToImage = PlayerPrefs.GetString(KeysForPlayerPrefs.IMAGE_PATH_KEY);
            var difficultyLevel = PlayerPrefs.GetString(KeysForPlayerPrefs.DIFFICULTY_LEVEL_KEY);
            var elementsAmount = NumberOfItemsPerDifficulties.OfItemsPerMode[difficultyLevel];
            var emptyPosition = new Vector2Int(elementsAmount - 1, 0);

            var currentPositions = GenerationPositions.GetRandomPositions(elementsAmount, emptyPosition);

            var piecesListTwoDimensional = InitialisePiecesListTwoDimensional(currentPositions, elementsAmount);

            _partsPanel.CreatedPiecesVisual(elementsAmount);

            switch (gameType)
            {
                case GameTypes.DEFAULT_GAME:
                    _partsPanel.FillWithNumbers(currentPositions);

                    break;
                case GameTypes.CUSTOM_GAME:
                    var texture2D = ImageLoader.GetImageByPath(pathToImage);

                    var textures2D = ImageCutter.CutImage(
                        texture2D,
                        ImageCutterType.NumberOfParts,
                        elementsAmount,
                        elementsAmount);

                    _partsPanel.FillWithPiecesOfCutsImages(textures2D, currentPositions);
                    _partsPanel.CreatedFullImage(texture2D);
                    break;
            }

            piecesListTwoDimensional.RegisterOnAllPartsInPlace(
                () =>
                {
                    _partsPanel.FullImagePanelSetActive(true);
                    _partsPanel.PartsPanelSetActive(false);
                });

            _partsPanel.FullImagePanelSetActive(false);
            _partsPanel.PartsPanelSetActive(true);
            _partsPanel.RegisteringButtonsEvents(
                currentPosition =>
                {
                    if (!piecesListTwoDimensional.TryMovePiece(currentPosition, out var newPosition)) return;

                    _partsPanel.MovePrice(currentPosition, newPosition, elementsAmount);
                });

            _partsPanel.RemovePrice(emptyPosition);
            piecesListTwoDimensional.RemovePiece(emptyPosition);
        }

        private static PiecesListTwoDimensional InitialisePiecesListTwoDimensional(Vector2Int[,] currentsPositions,
            int elementsAmount)
        {
            var piecesListTwoDimensional = new PiecesListTwoDimensional(elementsAmount, elementsAmount);

            for (var y = 0; y < elementsAmount; y++)
            for (var x = 0; x < elementsAmount; x++)
            {
                var currentPosition = currentsPositions[y, x];

                var targetPosition = new Vector2Int(x, y);
                var piece = new Piece.Piece(targetPosition, currentPosition);
                piecesListTwoDimensional.AddPiece(piece);
            }

            return piecesListTwoDimensional;
        }
    }
}