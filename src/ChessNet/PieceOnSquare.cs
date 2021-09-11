namespace ChessNet
{
    public readonly struct PieceOnSquare
    {
        public readonly Square Square;
        public readonly PieceEntry PieceEntry;

        public PieceOnSquare(Square square, PieceEntry pieceEntry)
        {
            Square = square;
            PieceEntry = pieceEntry;
        }
    }
}