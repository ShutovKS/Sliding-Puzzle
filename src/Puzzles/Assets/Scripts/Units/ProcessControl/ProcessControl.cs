#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Units.Piece;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = System.Random;

#endregion

namespace Units.ProcessControl
{
    public class ProcessControl
    {
        public ProcessControl(string pathToImage, int height, int width)
        {
            var currentsPositions = GenerationPositionForPieces(height, width);

            currentsPositions = RandomPositionsSort(currentsPositions);

            var buttonsInstanceDictionary = CreatedPiecesVisual(height, width);

            var piecesListTwoDimensional = CreatedPiecesListTwoDimensional(currentsPositions, height, width);

            var textures2D = GetImageCutterByPath(pathToImage, height, width);

            var buttonsInstance = buttonsInstanceDictionary.Select(pair => pair.Key).ToArray();

            FillWithPiecesOfCutsImages(buttonsInstance, textures2D, currentsPositions, height, width);

            RegisterButtonEvents(buttonsInstanceDictionary, piecesListTwoDimensional, height, width);

            piecesListTwoDimensional.RegisterOnAllPartsInPlace(
                () =>
                {
                    Debug.Log("Win");
                    foreach (var (gameObject, _) in buttonsInstanceDictionary)
                    {
                        gameObject.SetActive(true);
                    }
                });
        }

        #region Initialization

        private static Vector2Int[,] GenerationPositionForPieces(int height, int width)
        {
            var position = new Vector2Int(0, 0);
            var positions = new Vector2Int[height, width];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    positions[y, x] = position;
                    position.x++;
                }

                position.x = 0;
                position.y++;
            }

            return positions;
        }

        private static Vector2Int[,] RandomPositionsSort(Vector2Int[,] positions)
        {
            var random = new Random();
            var rowCount = positions.GetLength(0);
            var colCount = positions.GetLength(1);

            var count = (int)Math.Pow(rowCount * colCount, 1.5);

            for (var i = 0; i < count; i++)
            {
                var flag = 0;
                (int x, int y) index1 = (0, 0);
                while (flag < 5)
                {
                    flag++;
                    index1 = (random.Next(0, rowCount), random.Next(0, colCount));

                    if (index1 == (rowCount - 1, colCount - 1)) continue;

                    break;
                }
                
                if (flag == 5) continue;
                Debug.Log(index1);
                flag = 0;
                (int x, int y) index2 = (0, 0);
                while (flag < 5)
                {
                    flag++;
                    index2 = (random.Next(index1.x - 1, index1.x + 2), random.Next(index1.y - 1, index1.y + 2));

                    if (index2 == index1 ||
                        index1 == (rowCount - 1, colCount - 1) ||
                        index2.x < 0 || index2.x >= rowCount ||
                        index2.y < 0 || index2.y >= colCount)
                        continue;

                    break;
                }

                if (flag == 5) continue;
                Debug.Log(index2);

                (positions[index1.y, index1.x], positions[index2.y, index2.x]) =
                    (positions[index2.y, index2.x], positions[index1.y, index1.x]);
            }

            return positions;
        }

        private static Dictionary<GameObject, Vector2Int> CreatedPiecesVisual(int height, int width)
        {
            var resources = new DefaultControls.Resources();

            var canvas = DefaultControls.CreatePanel(resources);
            Object.Destroy(canvas.GetComponent<UnityEngine.UI.Image>());
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

            var panelForButtons = DefaultControls.CreatePanel(resources);
            panelForButtons.transform.SetParent(canvas.transform);
            panelForButtons.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

            var buttonsInstanceDictionary = new Dictionary<GameObject, Vector2Int>(height * width);

            GameObject oldButtonInstance = null;
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var position = new Vector2Int(x, y);

                var buttonGo = DefaultControls.CreateButton(resources);

                buttonGo.transform.SetParent(canvas.transform);

                var rectTransform = buttonGo.GetComponent<RectTransform>();

                rectTransform.anchorMin = new Vector2(
                    (float)position.x / width,
                    (float)position.y / height);

                rectTransform.anchorMax = new Vector2(
                    (float)(position.x + 1) / width,
                    (float)(position.y + 1) / height);

                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);

                buttonsInstanceDictionary.Add(buttonGo, position);

                buttonGo.GetComponentInChildren<Text>().text = $"{position.x} {position.y}";

                oldButtonInstance = buttonGo;
            }

            if (oldButtonInstance != null) oldButtonInstance.SetActive(false);
            return buttonsInstanceDictionary;
        }

        private static PiecesListTwoDimensional CreatedPiecesListTwoDimensional(
            Vector2Int[,] currentsPositions, int height, int width)
        {
            var piecesListTwoDimensional = new PiecesListTwoDimensional(height, width);

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var currentPosition = currentsPositions[y, x];
                if (currentPosition.x == width - 1 && currentPosition.y == height - 1) continue;


                var targetPosition = new Vector2Int(x, y);
                var piece = new Piece.Piece(targetPosition, currentPosition);
                piecesListTwoDimensional.AddPiece(piece);
            }

            return piecesListTwoDimensional;
        }

        private static Texture2D[,] GetImageCutterByPath(string pathToImage, int height, int width)
        {
            var texture2D = new Texture2D(2, 2);
            var bytes = File.ReadAllBytes(pathToImage);
            texture2D.LoadImage(bytes);
            var textures2D = new Texture2D[height, width];
            var size = new Vector2Int(texture2D.width / width, texture2D.height / height);
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var position = new Vector2Int(x * size.x, y * size.y);
                var texture = new Texture2D(size.x, size.y);
                texture.SetPixels(texture2D.GetPixels(position.x, position.y, size.x, size.y));
                texture.Apply();
                textures2D[y, x] = texture;
            }

            return textures2D;
        }

        private static void FillWithPiecesOfCutsImages(
            IReadOnlyList<GameObject> buttonsGo,
            Texture2D[,] textures2D,
            Vector2Int[,] currentsPositions,
            int height, int width)
        {
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var vectorCurrentPosition = currentsPositions[y, x];
                var currentPosition = vectorCurrentPosition.y * width + vectorCurrentPosition.x;
                var texture = textures2D[y, x];
                var sprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));

                buttonsGo[currentPosition].GetComponent<UnityEngine.UI.Image>().sprite = sprite;
            }
        }

        private static void RegisterButtonEvents(
            Dictionary<GameObject, Vector2Int> buttonsGo,
            PiecesListTwoDimensional piecesListTwoDimensional,
            int height, int width)
        {
            foreach (var (gameObject, _) in buttonsGo)
            {
                gameObject.GetComponent<Button>().onClick.AddListener(
                    () =>
                    {
                        MovePrice(
                            gameObject,
                            height,
                            width,
                            buttonsGo,
                            piecesListTwoDimensional);
                    });
            }
        }

        #endregion

        #region Tools

        private static void MovePrice(
            GameObject button,
            int height, int width,
            IDictionary<GameObject, Vector2Int> buttonsGo,
            PiecesListTwoDimensional piecesListTwoDimensional)
        {
            buttonsGo.TryGetValue(button, out var currentPosition);
            if (!piecesListTwoDimensional.TryMovePiece(
                    currentPosition,
                    out var newPosition,
                    out var targetPosition)) return;

            var rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(
                (float)newPosition.x / width,
                (float)newPosition.y / height);

            rectTransform.anchorMax = new Vector2(
                (float)(newPosition.x + 1) / width,
                (float)(newPosition.y + 1) / height);

            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);

            button.GetComponentInChildren<Text>().text = $"{newPosition}\n{targetPosition}";

            buttonsGo[button] = newPosition;
        }

        #endregion
    }
}