using System.Collections.Generic;
using System.Linq;
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
        
        private int _enpassantSquare = (int) Square.Empty;
        public Square EnPassantTargetSquare => (Square) _enpassantSquare;

        public Color CurrentMoveColor = Color.White;

        public ChessEngine(Dictionary<Square, PieceEntry> pieces = null)
        {
            InitPieces(pieces);
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

        private void InitPieces(Dictionary<Square, PieceEntry> pieces)
        {
            // todo: validate input
            
            var whiteKing = PieceEntry.WhiteKing();
            var blackKing = PieceEntry.BlackKing();
            var entries = pieces == null || pieces.Count < 3
                ? new Dictionary<int, PieceEntry>(64)
                {
                    {19, PieceEntry.WhiteKnight()},
                    {63, whiteKing},
                    {0, blackKing},
                    {10, PieceEntry.WhiteBishop()},
                    {11, PieceEntry.WhiteRook()},
                    {9, PieceEntry.WhiteQueen()},
                }
                : pieces
                    .ToDictionary(kvp => (int) kvp.Key, kvp => kvp.Value);

            _whiteKingSquare = entries.FirstOrDefault(x => x.Value == whiteKing).Key;
            _blackKingSquare = entries.FirstOrDefault(x => x.Value == blackKing).Key;
            
            PieceHolder.Entries = entries;
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