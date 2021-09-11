namespace ChessNet
{
    public class Board
    {
        public bool IsOnBoard(Square square) => (int) square >= 0 && (int) square <= 63;
        public bool IsEmptySquare(Square square) => !IsOnBoard(square);
    }
}