using System.Collections.Generic;
using ChessNet.Calculation;

namespace ChessNet.Movement
{
    public class BishopMovement : IPieceMovement
    {
        private readonly SquareCalculator _calculator = new();
        private readonly int _pieceSquare;
        private readonly Color _pieceColor;
        private readonly ChessEngine _engine;

        // todo: idk how does it work yet
        private static readonly Dictionary<(int x, int y), int> Steps = new(4)
        {
            {(+1, +1), -9}, // left-top
            {(-1, +1), -7}, // right-top
            {(+1, -1), +7}, // left-bottom
            {(-1, -1), +9} // right-bottom
        };

        public BishopMovement(int pieceSquare, Color pieceColor, ChessEngine chessEngine)
        {
            _pieceSquare = pieceSquare;
            _pieceColor = pieceColor;
            _engine = chessEngine;
        }

        public bool CanMove(int toSquare, Color toColor)
        {
            // todo: supposed toSquare is on board
            var isSameColor = _pieceColor == toColor; // common
            return !isSameColor && _calculator.AreOnDiagonalLine(_pieceSquare, toSquare) && CanMoveByDiagonal(_pieceSquare, toSquare);
        }

        public bool IsCheckAfterMove(int toSquare, Color toColor)
        {
            // todo: implement
            return false;
        }

        private bool CanMoveByDiagonal(int from, int to)
        {
            var steps = _calculator.Signs(from, to);
            var stepValue = Steps[steps];

            var tempFrom = from;
            while (_engine.Board.IsOnBoard(tempFrom))
            {
                tempFrom += stepValue;

                if (tempFrom == to)
                    return true;
                if (_engine.UnsafeGetPieceEntry(tempFrom).IsEmpty)
                    continue;

                return false;
            }

            return false;
        }
    }
}