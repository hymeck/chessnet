using System.Collections.Generic;
using ChessNet.Converters;
using ChessNet.Movement;
using Shouldly;
using Xunit;

namespace ChessNet.Tests
{
    public class ChessEngineTests
    {
        [Fact]
        public void PickPiece_OutOfBoard()
        {
            var engine = new ChessEngine();
            var picked = engine.CanPickPiece(-10, 44, out _);
            picked.ShouldBe(false);
        }
        
        [Fact]
        public void PickPiece_EmptyFromBoard()
        {
            var engine = new ChessEngine();
            var picked = engine.CanPickPiece(0, 0, out _);
            picked.ShouldBe(false);
        }
        
        [Fact]
        public void PickPiece_TakeKnight()
        {
            var engine = new ChessEngine();
            var picked = engine.CanPickPiece(19, out _);
            picked.ShouldBe(true);
        }
        
        [Fact]
        public void GeneratePossibleMoves_Knight()
        {
            var engine = new ChessEngine();
            var moves = engine
                .GeneratePossibleMoves(Square.D6);
            
            moves.Count.ShouldBe(7);
        }
        
        [Fact]
        public void GeneratePossibleMoves_Bishop()
        {
            var engine = new ChessEngine();
            var converter = new SquareConverter();
            var moves = engine
                .GeneratePossibleMoves(converter.FromInt32(10));
            
            moves.Count.ShouldBe(4);
        }
        
        [Fact]
        public void GeneratePossibleMoves_Rook()
        {
            var engine = new ChessEngine();
            var converter = new SquareConverter();
            var moves = engine
                .GeneratePossibleMoves(converter.FromInt32(11));
            
            moves.Count.ShouldBe(5);
        }
        
        [Fact]
        public void GeneratePossibleMoves_Queen()
        {
            var engine = new ChessEngine();
            var converter = new SquareConverter();
            var moves = engine
                .GeneratePossibleMoves(converter.FromInt32(9));
            
            moves.Count.ShouldBe(16);
        }
        
        
        [Theory]
        [InlineData(Square.E2, Square.E4)]
        [InlineData(Square.E2, Square.E3)]
        public void GeneratePossibleMoves_Pawn_Push_Forward_WithNoObstacles(Square from, Square to)
        {
            var pawn = PieceEntry.WhitePawn();
            var engine = new ChessEngine();
            var pawnMovement = new PawnMovement((int) from, (int) pawn.Color, engine);
            var canMove = pawnMovement.CanMove((int) to, (int) engine[to].Color);
            canMove.ShouldBe(Move.NoCapture);
        }
        
        [Theory]
        [InlineData(Square.E2, Square.A1)]
        [InlineData(Square.E2, Square.E3)]
        [InlineData(Square.E2, Square.E4)]
        [InlineData(Square.E2, Square.A3)]
        public void GeneratePossibleMoves_Pawn_IllegalMoves(Square from, Square to)
        {
            var pawn = PieceEntry.WhitePawn();
            
            var whiteKingSquare = Square.H1;
            var blackKingSquare = Square.A8;
            
            var whiteKing = PieceEntry.WhiteKing();
            var blackKing = PieceEntry.BlackKing();
            
            var pieces = new Dictionary<Square, PieceEntry>(64)
            {
                {whiteKingSquare, whiteKing},
                {blackKingSquare, blackKing},
                {from, pawn},
                {Square.E3, PieceEntry.BlackBishop()}
            };
            
            var engine = new ChessEngine(pieces);
            var pawnMovement = new PawnMovement((int) from, (int) pawn.Color, engine);
            var canMove = pawnMovement.CanMove((int) to, (int) engine[to].Color);
            canMove.ShouldBe(Move.Illegal);
        }
    }
}