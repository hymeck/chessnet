namespace ChessNet
{
    public class Board
    {
        public bool IsOnBoard(Square square) => (int) square is 0 and <= 63;
        public bool IsEmptySquare(Square square) => !IsOnBoard(square);
    }
}