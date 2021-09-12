using System;
using System.Collections.Generic;
using ChessNet.Movement;

namespace ChessNet
{
    public class MoveProvider
    {
        public IReadOnlyList<int> ProvideLegalMoves(Piece piece, Color pieceColor, int square, ChessEngine engine)
        {
            /*
             * iterate over all squares:
             *  move is legal when specified piece may move and checkmate does not occur after that move
             */
            
            var cmd = GetPieceCommand(piece, pieceColor, square, engine);
            var moves = new List<int>(64);
            for (var i = 0; i < 64; i++)
            {
                var color = engine.UnsafeGetPieceEntry(i).Color;
                if (cmd.CanMove(i, color) && !cmd.IsCheckAfterMove(i, color))
                    moves.Add(i);
            }

            return moves.AsReadOnly();
        }

        private IPieceMovement GetPieceCommand(Piece piece, Color pieceColor, int square, ChessEngine engine)
        {
            return piece switch
            {
                // todo: implement for other pieces
                Piece.Knight => new KnightMovement(square, pieceColor),
                Piece.Bishop => new BishopMovement(square, pieceColor, engine),
                Piece.Rook => new RookMovement(square, pieceColor, engine),
                _ => throw new NotImplementedException(nameof(GetPieceCommand))
            };
        }
    }
}