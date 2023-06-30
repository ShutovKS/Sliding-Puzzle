#region

using UnityEngine;

#endregion

namespace Units.Image
{
    public static class ImageCutter
    {
        public static Texture2D[,] CutImage(
            Texture2D texture2D,
            ImageCutterType type = ImageCutterType.Automatic,
            int partsX = 3, int partsY = 3)
        {
            return type switch
            {
                ImageCutterType.Automatic => CutterAutomatic(texture2D),
                ImageCutterType.NumberOfParts => CutterToNumberOfParts(texture2D, partsX, partsY),
                ImageCutterType.PartSize => CutterToPartSize(texture2D, partsX, partsY),
                _ => null
            };
        }

        private static Texture2D[,] CutterAutomatic(Texture2D sourceImage)
        {
            var imageWidth = sourceImage.width;
            var imageHeight = sourceImage.height;

            var puzzlePieceWidth = imageWidth / 3;
            var puzzlePieceHeight = imageHeight / 3;

            var numPiecesX = imageWidth / puzzlePieceWidth;
            var numPiecesY = imageHeight / puzzlePieceHeight;

            var puzzlePieces = Cutter(sourceImage, puzzlePieceWidth, puzzlePieceHeight, numPiecesX, numPiecesY);

            return puzzlePieces;
        }

        private static Texture2D[,] CutterToNumberOfParts(Texture2D sourceImage, int numPiecesX, int numPiecesY)
        {
            var imageWidth = sourceImage.width;
            var imageHeight = sourceImage.height;

            var puzzlePieceWidth = imageWidth / numPiecesX;
            var puzzlePieceHeight = imageHeight / numPiecesY;

            var puzzlePieces = Cutter(sourceImage, puzzlePieceWidth, puzzlePieceHeight, numPiecesX, numPiecesY);

            return puzzlePieces;
        }

        private static Texture2D[,] CutterToPartSize(Texture2D sourceImage, int puzzlePieceWidth, int puzzlePieceHeight)
        {
            var imageWidth = sourceImage.width;
            var imageHeight = sourceImage.height;

            var numPiecesX = imageWidth / puzzlePieceWidth;
            var numPiecesY = imageHeight / puzzlePieceHeight;

            var puzzlePieces = Cutter(sourceImage, puzzlePieceWidth, puzzlePieceHeight, numPiecesX, numPiecesY);

            return puzzlePieces;
        }

        private static Texture2D[,] Cutter(Texture2D sourceImage, int puzzlePieceWidth, int puzzlePieceHeight,
            int numPiecesX, int numPiecesY)
        {
            var puzzlePieces = new Texture2D[numPiecesX, numPiecesY];

            for (var y = 0; y < numPiecesY; y++)
            {
                for (var x = 0; x < numPiecesX; x++)
                {
                    var pixels = sourceImage.GetPixels(
                        x * puzzlePieceWidth,
                        y * puzzlePieceHeight,
                        puzzlePieceWidth,
                        puzzlePieceHeight);

                    var puzzlePiece = new Texture2D(puzzlePieceWidth, puzzlePieceHeight);
                    puzzlePiece.SetPixels(pixels);
                    puzzlePiece.Apply();

                    puzzlePieces[x, y] = puzzlePiece;
                }
            }

            return puzzlePieces;
        }
    }
}