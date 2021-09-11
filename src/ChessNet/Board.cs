namespace ChessNet
{
    public class Board
    {
        internal bool IsOnBoard(int square) => square >= 0 && square <= 63;
        public bool IsOnBoard(Square square) => IsOnBoard((int) square);
        public bool IsEmptySquare(Square square) => !IsOnBoard(square);
    }
}