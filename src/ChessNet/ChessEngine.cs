using System.Collections.Generic;
using ChessNet.Converters;

namespace ChessNet
{
    public class ChessEngine
    {
        public readonly SquareConverter SquareConverter = new();
        public readonly Board Board = new();
        public readonly PieceHolder PieceHolder = new();
        public readonly MoveGenerator _moveGenerator = new();

        private int _whiteKingSquare;
        private int _blackKingSquare;
        public Square WhiteKingSquare => (Square) _whiteKingSquare;
        public Square BlackKingSquare => (Square) _blackKingSquare;

        public Color CurrentMoveColor = Color.White;

        public ChessEngine()
        {
            InitPieces();
        }
        
        public bool CanPickPiece(Square pieceSquare, out PieceEntry pickedPiece)
        {
            var pieceEntry = this[pieceSquare];
            pickedPiece = pieceEntry;
            return pieceEntry.Color == CurrentMoveColor;
        }

        public bool CanPickPiece(int piecePosition, out PieceEntry pickedPiece) => CanPickPiece(SquareConverter.FromInt32(piecePosition), out pickedPiece);

        public bool CanPickPiece(int pieceX, int pieceY, out PieceEntry pickedPiece) => CanPickPiece(SquareConverter.FromCartesian(pieceX, pieceY), out pickedPiece);

        public PieceEntry this[Square square] => 
            Board.IsOnBoard(square) ? GetPieceEntryFromBoardSquare(square) : PieceEntry.Empty;
        
        // unsafe
        internal PieceEntry UnsafeGetPieceEntry(int square) => PieceHolder.GetPieceEntry(square);

        private PieceEntry GetPieceEntryFromBoardSquare(Square square) => 
            PieceHolder.GetPieceEntry((int) square);

        private void InitPieces()
        {
            var wk = 63;
            var bk = 0;
            PieceHolder.Entries = new Dictionary<int, PieceEntry>(64)
            {
                {19, PieceEntry.WhiteKnight()},
                {wk, PieceEntry.WhiteKing()},
                {bk, PieceEntry.BlackKing()},
                {10, PieceEntry.WhiteBishop()},
                {11, PieceEntry.WhiteRook()},
                {9, PieceEntry.WhiteQueen()},
            };

            _whiteKingSquare = wk;
            _blackKingSquare = bk;
        }
        
        

        public IReadOnlyList<int> GeneratePossibleMoves(Square square)
        {
            return CanPickPiece(square, out var pickedPiece)
                ? _moveGenerator
                    .GeneratePossibleMoves(new PieceOnSquare(square, pickedPiece), this)
                : new List<int>(0);
        }

        public int GetKingSquare(Color pieceEntryColor) => 
            pieceEntryColor == Color.White ? _whiteKingSquare : _blackKingSquare;
    }
}