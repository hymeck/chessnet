using System.Collections.Generic;
using System.Linq;
using ChessNet.Converters;
using ChessNet.Movement;

namespace ChessNet
{
    public class ChessEngine
    {
        private readonly SquareConverter SquareConverter = new();
        public readonly Board Board = new();
        private readonly PieceHolder PieceHolder;
        private readonly MoveGenerator _moveGenerator = new();

        public Square WhiteKingSquare => (Square) PieceHolder.WhiteKing;
        public Square BlackKingSquare => (Square) PieceHolder.WhiteKing;
        
        private int _enpassantSquare = (int) Square.Empty;
        public Square EnPassantTargetSquare => (Square) _enpassantSquare;

        public Color CurrentMoveColor = Color.White;

        public ChessEngine(Dictionary<Square, PieceEntry> pieces = null)
        {
            PieceHolder = InitPieceHolder(pieces);
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

        private PieceHolder InitPieceHolder(Dictionary<Square, PieceEntry> pieces)
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

            return new PieceHolder(entries);
        }

        public IReadOnlyList<Square> GeneratePossibleMoves(Square square)
        {
            // todo: save valid state to not generate legal moves twice
            return CanPickPiece(square, out var pickedPiece)
                ? _moveGenerator
                    .GeneratePossibleMoves(new PieceOnSquare(square, pickedPiece), this)
                    .Select(x => (Square)x.square)
                    .ToList()
                : new List<Square>(0);
        }

        public int CanCurrentKingBeCaptured(int toSquare, int toColor)
        {
            var color = (Color) toColor == Color.Black ? Color.White : Color.Black;
            foreach (var (square, pieceEntry) in PieceHolder.GetPieces(color))
            {
                // todo: too many instantiations
                var movement = GetPieceMovement(pieceEntry.Piece, pieceEntry.Color, square, this);
                var move = movement.CanMove(toColor, toColor);
                if ((move & Move.Illegal) != 0)
                    return 1;
            }

            return 0;
        }
        
        private IPieceMovement GetPieceMovement(Piece piece, Color pieceColor, int square, ChessEngine engine)
        {
            var color = (int) pieceColor;
#pragma warning disable 8509
            return piece switch
#pragma warning restore 8509
            {
                // todo: implement for other pieces
                Piece.Knight => new KnightMovement(square, color, engine),
                Piece.Bishop => new BishopMovement(square, color, engine),
                Piece.Rook => new RookMovement(square, color, engine),
                Piece.Queen => new QueenMovement(square, color, engine),
                Piece.Pawn => new PawnMovement(square, color, engine),
                Piece.King => new KingMovement(square, color, engine)
            };
        }
    }
}