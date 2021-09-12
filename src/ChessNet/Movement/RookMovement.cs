using System.Collections.Generic;
using ChessNet.Calculation;

namespace ChessNet.Movement
{
    public class RookMovement : IPieceMovement
    {
        private readonly SquareCalculator _calculator = new();
        private readonly int _pieceSquare;
        private readonly int _pieceColor;
        private readonly ChessEngine _engine;
        
        // todo: idk how does it work yet
        private static readonly Dictionary<(int x, int y), int> Steps = new(4)
        {
            {(+1, 0), -1}, // left
            {(0, +1), -8}, // top
            {(-1, 0), +1}, // right
            {(0, -1), +8} // bottom
        };

        public RookMovement(int pieceSquare, int pieceColor, ChessEngine engine)
        {
            _pieceSquare = pieceSquare;
            _pieceColor = pieceColor;
            _engine = engine;
        }

        public Move CanMove(int toSquare, int toColor)
        {
            // var isSameColor = _pieceColor == toColor; // common
            // return (Move)(!isSameColor && _calculator.AreOnDiagonalLine(_pieceSquare, toSquare) && CanMoveByDiagonal(_pieceSquare, toSquare)).CompareTo(true);
            // illegal, no capture, capture:
            //  illegal - invalid move or check occurs after move;
            //  no capture - _toColor is neither white nor black
            //  capture - _pieceColor does not equal toColor
            
            if ((_pieceColor & toColor) != 0) // the same color
                return Move.Illegal;

            // -- specific --
            if ((_calculator.AreOnFile(_pieceSquare, toSquare) 
                 | _calculator.AreOnRank(_pieceSquare, toSquare)) == 0)
                return Move.Illegal;
            
            if (!CanMoveI(_pieceSquare, toSquare)) // move does not matches with rule of piece movement -> illegal
                return Move.Illegal;
            // -- specific --
            
            var checkAfterMove = 0; // todo: implement check of check after moving
            if (checkAfterMove == 1) // illegal
                return Move.Illegal;
            
            
            if ((_pieceColor | toColor) == _pieceColor) // no capture
                return Move.NoCapture;
            
            // capture
            return Move.Capture;
        }
        
        private bool CanMoveI(int from, int to)
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