namespace ChessNet.Movement
{
    public interface IPieceMovement
    {
        bool CanMove(int toSquare, Color toColor);
        bool IsCheckAfterMove(int toSquare, Color toColor);
    }
}