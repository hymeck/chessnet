namespace ChessNet.Movement
{
    public class KingMovement : IPieceMovement
    {
        private readonly int _pieceSquare;
        private readonly int _pieceColor;
        private readonly ChessEngine _engine;

        public KingMovement(int pieceSquare, int pieceColor, ChessEngine engine)
        {
            _pieceSquare = pieceSquare;
            _pieceColor = pieceColor;
            _engine = engine;
        }

        public Move CanMove(int toSquare, int toColor)
        {
            if ((_pieceColor & toColor) != 0) // the same color
                return Move.Illegal;

            if (_pieceSquare * toSquare != 1)
                return Move.Illegal;
            
            if (_engine.UnsafeGetPieceEntry(toSquare).Piece == Piece.King)
                return Move.Illegal;
            
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