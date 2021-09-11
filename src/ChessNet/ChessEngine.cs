using System.Collections.Generic;
using ChessNet.Converters;

namespace ChessNet
{
    public class ChessEngine
    {
        public readonly SquareConverter SquareConverter = new();
        public readonly Board Board = new();
        public readonly PieceHolder PieceHolder = new();

        public Color CurrentMoveColor = Color.White;

        public ChessEngine()
        {
            InitPieces();
        }
        
        public bool CanPickPiece(Square pieceSquare)
        {
            var pieceEntry = this[pieceSquare];
            return pieceEntry.Color == CurrentMoveColor;
        }

        public bool CanPickPiece(int piecePosition) => CanPickPiece(SquareConverter.FromInt32(piecePosition));

        public bool CanPickPiece(int pieceX, int pieceY) => CanPickPiece(SquareConverter.FromCartesian(pieceX, pieceY));

        public PieceEntry this[Square square] => 
            Board.IsOnBoard(square) ? GetPieceEntryFromBoardSquare(square) : PieceEntry.Empty;

        private PieceEntry GetPieceEntryFromBoardSquare(Square square) => 
            PieceHolder.GetPieceEntry((int) square);

        private void InitPieces()
        {
            PieceHolder.Entries = new Dictionary<int, PieceEntry>(64)
            {
                {19, new PieceEntry(Piece.Knight, Color.White)}
            };
        }
    }
}