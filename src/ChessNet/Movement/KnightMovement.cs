using ChessNet.Calculation;

namespace ChessNet.Movement
{
    public class KnightMovement : IPieceMovement
    {
        private readonly SquareCalculator _calculator = new();
        private readonly int _pieceSquare;
        private readonly Color _pieceColor;

        public KnightMovement(int pieceSquare, Color pieceColor)
        {
            _pieceSquare = pieceSquare;
            _pieceColor = pieceColor;
        }

        public bool CanMove(int toSquare, Color toColor)
        {
            // todo: supposed toSquare is on board
            var isSameColor = _pieceColor == toColor; // common
            var isMovePossible = _calculator.AbsDeltaX(_pieceSquare, toSquare) * _calculator.AbsDeltaY(_pieceSquare, toSquare) == 2; // specific
            return !isSameColor && isMovePossible; // common
        }

        public bool IsCheckAfterMove(int toSquare, Color toColor)
        {
            // todo: implement
            return false;
        }
    }
}