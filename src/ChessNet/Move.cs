using System;

namespace ChessNet
{
    [Flags]
    public enum Move
    {
        Illegal   = 0b00000, // 0
        NoCapture = 0b00001, // 1
        Capture   = 0b00010, // 2
        Castling  = 0b00101, // 4 + 1
        EnPassant = 0b01010, // 8 + 2
        Promotion = 0b10001, // 16 + 1
    }
}