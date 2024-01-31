#region

using System;
using UnityEngine;
using Random = System.Random;

#endregion

namespace Units.PuzzleGenerator
{
    public static class GenerationPositions
    {
        private static readonly Random _random = new();

        public static Vector2Int[,] GetRandomPositions(int elementsAmount, Vector2Int emptyPosition)
        {
            var positions = new Vector2Int[elementsAmount, elementsAmount];

            for (var y = 0; y < elementsAmount; y++)
            for (var x = 0; x < elementsAmount; x++)
            {
                positions[y, x] = new Vector2Int(x, y);
            }

            var countSort = (int)Math.Pow(elementsAmount * elementsAmount, 2);

            var oldPositions = (emptyPosition.x, emptyPosition.y);

            for (var i = 0; i < countSort; i++)
            {
                (int x, int y) newPosition = (0, 0);
                if (_random.Next(0, 2) == 1)
                {
                    newPosition.x = _random.Next(0, 2) == 1 ? 1 : -1;
                }
                else
                {
                    newPosition.y = _random.Next(0, 2) == 1 ? 1 : -1;
                }

                newPosition.x += oldPositions.x;
                newPosition.y += oldPositions.y;

                if (newPosition.x < 0 ||
                    newPosition.y < 0 ||
                    newPosition.x >= elementsAmount ||
                    newPosition.y >= elementsAmount)
                    continue;

                (positions[oldPositions.y, oldPositions.x], positions[newPosition.y, newPosition.x]) = (
                    positions[newPosition.y, newPosition.x], positions[oldPositions.y, oldPositions.x]);

                oldPositions = newPosition;
            }

            return positions;
        }

        public static bool IsMixedUp(Vector2Int[,] currentPositions)
        {
            for (var y = 0; y < currentPositions.GetLength(0); y++)
            {
                for (var x = 0; x < currentPositions.GetLength(1); x++)
                {
                    if (currentPositions[y, x].x != x || currentPositions[y, x].y != y)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}