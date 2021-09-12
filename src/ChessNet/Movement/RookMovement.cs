using System.Collections.Generic;
using ChessNet.Calculation;

namespace ChessNet.Movement
{
    public class RookMovement : IPieceMovement
    {
        private readonly SquareCalculator _calculator = new();
        private readonly int _pieceSquare;
        private readonly Color _pieceColor;
        private readonly ChessEngine _engine;
        
        // todo: idk how does it work yet
        private static readonly Dictionary<(int x, int y), int> Steps = new(4)
        {
            {(+1, 0), -1}, // left
            {(0, +1), -8}, // top
            {(-1, 0), +1}, // right
            {(0, -1), +8} // bottom
        };

        public RookMovement(int pieceSquare, Color pieceColor, ChessEngine engine)
        {
            _pieceSquare = pieceSquare;
            _pieceColor = pieceColor;
            _engine = engine;
        }

        public bool CanMove(int toSquare, Color toColor)
        {
            // todo: supposed toSquare is on board
            var isSameColor = _pieceColor == toColor; // common
            return !isSameColor 
                   && (_calculator.AreOnFile(_pieceSquare, toSquare) || _calculator.AreOnRank(_pieceSquare, toSquare))
                   && CanMove(_pieceSquare, toSquare);
        }

        public bool IsCheckAfterMove(int toSquare, Color toColor)
        {
            // todo: implement
            return false;
        }
        
        private bool CanMove(int from, int to)
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