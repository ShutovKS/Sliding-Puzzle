#region

using UnityEngine;

#endregion

namespace Units.Image
{
    public static class ImageSeparation
    {
        public static Texture2D[,] GetSlicedImagePartially(
            Texture2D texture2D,
            ImageSeparationType type = ImageSeparationType.Automatic,
            int[] parts = null)
        {
            parts ??= new[] { 3, 3 };

            return type switch
            {
                ImageSeparationType.Automatic => SeparationAutomatic(texture2D),
                ImageSeparationType.NumberOfParts => SeparationToNumberOfParts(texture2D, parts[0], parts[1]),
                ImageSeparationType.PartSize => SeparationToPartSize(texture2D, parts[0], parts[1]),
                _ => null
            };
        }

        private static Texture2D[,] SeparationAutomatic(Texture2D sourceImage)
        {
            var imageWidth = sourceImage.width;
            var imageHeight = sourceImage.height;

            var puzzlePieceWidth = imageWidth / 3;
            var puzzlePieceHeight = imageHeight / 3;

            var numPiecesX = imageWidth / puzzlePieceWidth;
            var numPiecesY = imageHeight / puzzlePieceHeight;

            var puzzlePieces = Separation(sourceImage, puzzlePieceWidth, puzzlePieceHeight, numPiecesX, numPiecesY);

            return puzzlePieces;
        }

        private static Texture2D[,] SeparationToNumberOfParts(Texture2D sourceImage, int numPiecesX, int numPiecesY)
        {
            var imageWidth = sourceImage.width;
            var imageHeight = sourceImage.height;

            var puzzlePieceWidth = imageWidth / numPiecesX;
            var puzzlePieceHeight = imageHeight / numPiecesY;

            var puzzlePieces = Separation(sourceImage, puzzlePieceWidth, puzzlePieceHeight, numPiecesX, numPiecesY);

            return puzzlePieces;
        }

        private static Texture2D[,] SeparationToPartSize(Texture2D sourceImage, int puzzlePieceWidth, int puzzlePieceHeight)
        {
            var imageWidth = sourceImage.width;
            var imageHeight = sourceImage.height;

            var numPiecesX = imageWidth / puzzlePieceWidth;
            var numPiecesY = imageHeight / puzzlePieceHeight;

            var puzzlePieces = Separation(sourceImage, puzzlePieceWidth, puzzlePieceHeight, numPiecesX, numPiecesY);

            return puzzlePieces;
        }

        private static Texture2D[,] Separation(Texture2D sourceImage, int puzzlePieceWidth, int puzzlePieceHeight,
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