using System;

namespace ChessNet
{
    [Flags]
    public enum Move
    {
        Empty     = 0b000000,
        Illegal   = 0b000001,
        NoCapture = 0b000010,
        Capture   = 0b000100,
        Castling  = 0b001000 | NoCapture,
        EnPassant = 0b010000 | Capture,
        Promotion = 0b100000 | NoCapture,
    }
}