#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data.PlayerPrefs;
using UI.FoldingThePuzzle;
using Units.Image;
using Units.Piece;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = System.Random;

#endregion

namespace Units.ProcessControl
{
    public class FoldingThePuzzleControl : MonoBehaviour
    {
        [SerializeField] private PartsPanel _partsPanel;

        private void Awake()
        {
            var pathToImage = PlayerPrefs.GetString(KeysForPlayerPrefs.IMAGE_PATH_KEY);
            var gameModeType = PlayerPrefs.GetString(KeysForPlayerPrefs.DIFFICULTY_LEVEL_KEY);
            var elementsAmount = NumberOfItemsPerMode.OfItemsPerMode[gameModeType];

            var currentsPositions = GenerationRandomPositions(elementsAmount);
            
            var piecesListTwoDimensional = InitialisePiecesListTwoDimensional(currentsPositions, elementsAmount);

            var texture2D = ImageLoader.GetImageByPath(pathToImage);

            var textures2D = ImageCutter.CutImage(
                texture2D,
                ImageCutterType.NumberOfParts,
                elementsAmount,
                elementsAmount);

            _partsPanel.CreatedPiecesVisual(elementsAmount);
            _partsPanel.FillWithPiecesOfCutsImages(textures2D, currentsPositions);
            _partsPanel.CreatedFullImage(texture2D);
            _partsPanel.FullImagePanelSetActive(false);
            _partsPanel.PartsPanelSetActive(true);
            _partsPanel.RegisteringButtonsEvents(
                currentPosition =>
                {
                    if (!piecesListTwoDimensional.TryMovePiece(currentPosition, out var newPosition)) return;

                    _partsPanel.MovePrice(currentPosition, newPosition, elementsAmount);
                });

            _partsPanel.RemovePrice(new Vector2Int(elementsAmount - 1, elementsAmount - 1));
            piecesListTwoDimensional.RemovePiece(new Vector2Int(elementsAmount - 1, elementsAmount - 1));

            piecesListTwoDimensional.RegisterOnAllPartsInPlace(
                () =>
                {
                    _partsPanel.FullImagePanelSetActive(true);
                    _partsPanel.PartsPanelSetActive(false);
                });
        }

        #region Initialization

        private Vector2Int[,] GenerationRandomPositions(int size)
        {
            var random = new Random();
            var positions = new Vector2Int[size, size];

            for (var y = 0; y < size; y++)
            for (var x = 0; x < size; x++)
            {
                positions[y, x] = new Vector2Int(x, y);
            }

            var countSort = (int)Math.Pow(size * size, 2);
            
            (int x, int y) oldPositions = (size - 1, size - 1);
            
            for (var i = 0; i < countSort; i++)
            {
                (int x, int y) newPosition = (0, 0);
                if (random.Next(0, 2) == 1)
                {
                    newPosition.x = random.Next(0, 2) == 1 ? 1 : -1;
                }
                else
                {
                    newPosition.y = random.Next(0, 2) == 1 ? 1 : -1;
                }
            
                newPosition.x += oldPositions.x;
                newPosition.y += oldPositions.y;
            
                if (newPosition.x < 0 ||
                    newPosition.y < 0 ||
                    newPosition.x >= size ||
                    newPosition.y >= size)
                    continue;
            
                (positions[oldPositions.y, oldPositions.x], positions[newPosition.y, newPosition.x]) = (
                    positions[newPosition.y, newPosition.x], positions[oldPositions.y, oldPositions.x]);
            
                oldPositions = newPosition;
            }

            return positions;
        }

        private PiecesListTwoDimensional InitialisePiecesListTwoDimensional(Vector2Int[,] currentsPositions,
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

        #endregion
    }
}