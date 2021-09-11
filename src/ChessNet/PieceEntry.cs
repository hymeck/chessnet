using System;

namespace ChessNet
{
    public readonly struct PieceEntry : IEquatable<PieceEntry>
    {
        public static readonly PieceEntry Empty = new(Piece.Empty, Color.Empty);
        
        public readonly Piece Piece;
        public readonly Color Color;

        public PieceEntry(Piece piece, Color color)
        {
            Piece = piece;
            Color = color;
        }

        public bool Equals(PieceEntry other) => (int) Piece == (int) other.Piece && (int) Color == (int) other.Color;

        public override bool Equals(object obj) => 
            obj is PieceEntry other && Equals(other);
        
        public override int GetHashCode() => HashCode.Combine((int) Piece, (int) Color);

        public static bool operator ==(PieceEntry left, PieceEntry right) =>
            (int) left.Piece == (int) right.Piece && (int) left.Color == (int) right.Color;
        
        public static bool operator !=(PieceEntry left, PieceEntry right) =>
            (int) left.Piece != (int) right.Piece || (int) left.Color != (int) right.Color;

        public bool IsEmpty => Piece == Piece.Empty && Color == Color.Empty;
    }
}