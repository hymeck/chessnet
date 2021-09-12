using System;

namespace ChessNet
{
    public readonly struct PieceEntry : IEquatable<PieceEntry>
    {
        public static readonly PieceEntry Empty = new(Piece.Empty, Color.Empty);
        
        public readonly Piece Piece;
        public readonly Color Color;

        private PieceEntry(Piece piece, Color color)
        {
            Piece = piece;
            Color = color;
        }

        public bool Equals(PieceEntry other) => Piece == other.Piece && Color == other.Color;

        public override bool Equals(object obj) => 
            obj is PieceEntry other && Equals(other);
        
        public override int GetHashCode() => HashCode.Combine(Piece, Color);

        public static bool operator ==(PieceEntry left, PieceEntry right) =>
            left.Piece == right.Piece && left.Color == right.Color;
        
        public static bool operator !=(PieceEntry left, PieceEntry right) =>
            left.Piece != right.Piece || left.Color != right.Color;

        public bool IsEmpty => this == Empty;
        
        public static PieceEntry WhiteKing() => new(Piece.King, Color.White);
        public static PieceEntry BlackKing() => new(Piece.King, Color.Black);
        
        public static PieceEntry WhiteKnight() => new(Piece.Knight, Color.White);
        public static PieceEntry BlackKnight() => new(Piece.Knight, Color.Black);
        
        public static PieceEntry WhiteRook() => new(Piece.Rook, Color.White);
        public static PieceEntry BlackRook() => new(Piece.Rook, Color.Black);
        
        public static PieceEntry WhiteBishop() => new(Piece.Bishop, Color.White);
        public static PieceEntry BlackBishop() => new(Piece.Bishop, Color.Black);
        
        public static PieceEntry WhiteQueen() => new(Piece.Queen, Color.White);
        public static PieceEntry BlackQueen() => new(Piece.Queen, Color.Black);
        
        public static PieceEntry WhitePawn() => new(Piece.Pawn, Color.White);
        public static PieceEntry BlackPawn() => new(Piece.Pawn, Color.Black);
    }
}