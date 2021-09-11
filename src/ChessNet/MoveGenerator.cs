using System.Collections.Generic;

namespace ChessNet
{
    public class MoveGenerator
    {
        private readonly MoveProvider _provider = new();

        public IReadOnlyList<int> GeneratePossibleMoves(PieceOnSquare pieceOnSquare, ChessEngine chessEngine) =>
            _provider
                .ProvideLegalMoves(pieceOnSquare.PieceEntry.Piece, 
                    pieceOnSquare.PieceEntry.Color, (int)pieceOnSquare.Square, chessEngine);
    }
}