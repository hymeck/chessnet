using ChessNet.Calculation;

namespace ChessNet.Movement
{
    public class KnightMovement : IPieceMovement
    {
        private readonly SquareCalculator _calculator = new();
        private readonly int _pieceSquare;
        private readonly int _pieceColor;

        public KnightMovement(int pieceSquare, int pieceColor)
        {
            _pieceSquare = pieceSquare;
            _pieceColor = pieceColor;
        }

        public Move CanMove(int toSquare, int toColor)
        {
            // illegal, no capture, capture:
            //  illegal - invalid move or check occurs after move;
            //  no capture - _toColor is neither white nor black
            //  capture - _pieceColor does not equal toColor
            
            if ((_pieceColor & toColor) != 0) // the same color
                return Move.Illegal;

            // -- specific --
            var ax = _calculator.AbsDeltaX(_pieceSquare, toSquare);
            var ay = _calculator.AbsDeltaY(_pieceSquare, toSquare);
            if (ax * ay != 2) // move does not matches with rule of piece movement -> illegal
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
    }
}