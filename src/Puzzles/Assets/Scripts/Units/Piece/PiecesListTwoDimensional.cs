#region

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Units.Piece
{
    public class PiecesListTwoDimensional
    {
        public PiecesListTwoDimensional(int height, int width)
        {
            _height = height;
            _width = width;

            _grid = new Piece[_height, _width];
            _partsOutOfPlace = new List<Piece>(_height * _width);
            for (var y = 0; y < _height; y++)
            for (var x = 0; x < _width; x++)
                _grid[y, x] = null;
        }

        private readonly int _height;
        private readonly int _width;
        private readonly Piece[,] _grid;
        private readonly List<Piece> _partsOutOfPlace;

        private UnityAction _onAllPartsInPlace;

        public bool TryMovePiece(Vector2Int currentPosition, out Vector2Int newPosition, out Vector2Int targetPosition)
        {
            var x = currentPosition.x;
            var y = currentPosition.y;
            if (_grid[y, x] == null) throw new Exception($"Piece not found {x}/{y}");

            targetPosition = _grid[y, x].TargetPosition;

            for (var tempY = y - 1; tempY <= y + 1; tempY++)
            for (var tempX = x - 1; tempX <= x + 1; tempX++)
            {
                if (tempX < 0 || tempX >= _width ||
                    tempY < 0 || tempY >= _height ||
                    tempX != x && tempY != y ||
                    tempX == x && tempY == y ||
                    _grid[tempY, tempX] != null) continue;

                _grid[tempY, tempX] = _grid[y, x];
                _grid[y, x] = null;
                newPosition = new Vector2Int(tempX, tempY);
                _grid[tempY, tempX].CurrentPosition = newPosition;

                CheckOnPiece(_grid[tempY, tempX]);

                return true;
            }

            newPosition = new Vector2Int(x, y);
            return false;
        }

        public void AddPiece(Piece piece)
        {
            var x = piece.CurrentPosition.x;
            var y = piece.CurrentPosition.y;
            if (_grid[y, x] != null) throw new Exception($"Piece already exists {y}/{x}");
            _grid[y, x] = piece;

            CheckOnPiece(piece);
        }

        public void RemovePiece([CanBeNull] Piece piece)
        {
            if (piece == null) throw new Exception("Piece is null");
            var x = piece.CurrentPosition.x;
            var y = piece.CurrentPosition.y;
            if (_grid[y, x] == null) throw new Exception("Piece not found");
            _grid[y, x] = null;
            _partsOutOfPlace.Remove(piece);
        }

        public void RegisterOnAllPartsInPlace(UnityAction action)
        {
            _onAllPartsInPlace += action;
        }

        private void CheckOnPiece(Piece piece)
        {
            if (piece.CurrentPosition == piece.TargetPosition)
            {
                _partsOutOfPlace.Remove(piece);
                CheckAllPartsInPlace();
            }
            else if (!_partsOutOfPlace.Contains(piece))
            {
                _partsOutOfPlace.Add(piece);
            }
        }

        private void CheckAllPartsInPlace()
        {
            if (_partsOutOfPlace.Count == 0)
                _onAllPartsInPlace?.Invoke();
        }
    }
}