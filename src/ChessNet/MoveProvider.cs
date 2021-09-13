using System.Collections.Generic;
using ChessNet.Movement;

namespace ChessNet
{
    public class MoveProvider
    {
        public IReadOnlyList<(int square, Move move)> ProvideLegalMoves(Piece piece, Color pieceColor, int square, ChessEngine engine)
        {
            /*
             * iterate over all squares:
             *  move is legal when specified piece may move and checkmate does not occur after that move
             */
            
            var cmd = GetPieceCommand(piece, pieceColor, square, engine);
            var moves = new List<(int square, Move move)>(64);
            for (var to = 0; to < 64; to++)
            {
                var color =  (int) engine.UnsafeGetPieceEntry(to).Color;
                var move = cmd.CanMove(to, color);
                var moveAfterCheck = cmd.CanMoveWithCheckAfterMove(to, color);
                if ((move & Move.Illegal) != 0 && moveAfterCheck != 1)
                    moves.Add((to, move));
            }

            return moves.AsReadOnly();
        }

        private IPieceMovement GetPieceCommand(Piece piece, Color pieceColor, int square, ChessEngine engine)
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