using ChessNet.Converters;

namespace ChessNet
{
    public class ChessEngine
    {
        public readonly SquareConverter SquareConverter = new();
        public readonly Board Board = new();
        public readonly PieceHolder PieceHolder = new();

        public Color CurrentMoveColor = Color.White;
        
        public bool PickPiece(Square pieceSquare)
        {
            var pieceEntry = this[pieceSquare];
            
            if (pieceEntry.Color != CurrentMoveColor)
                return false;
            
            // rest logic

            return true;
        }

        public bool PickPiece(int pieceX, int pieceY) => PickPiece(SquareConverter.FromCartesian(pieceX, pieceY));

        public PieceEntry this[Square square] => 
            Board.IsOnBoard(square) ? GetPieceEntryFromBoardSquare(square) : PieceEntry.Empty;

        private PieceEntry GetPieceEntryFromBoardSquare(Square square) => 
            PieceHolder.GetPieceEntry((int) square);
    }
}