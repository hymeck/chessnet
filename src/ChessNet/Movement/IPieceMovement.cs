namespace ChessNet.Movement
{
    public interface IPieceMovement
    {
        Move CanMove(int toSquare, int toColor);
        int CanMoveWithCheckAfterMove(int toSquare, int toColor);
    }
}