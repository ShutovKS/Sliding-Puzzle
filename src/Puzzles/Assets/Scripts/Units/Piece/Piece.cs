#region

using UnityEngine;

#endregion

namespace Units.Piece
{
    public class Piece
    {
        public Piece(Vector2Int targetPosition, Vector2Int currentPosition)
        {
            TargetPosition = targetPosition;
            CurrentPosition = currentPosition;
        }

        public Vector2Int TargetPosition { get; }
        public Vector2Int CurrentPosition { get; set; }
    }
}