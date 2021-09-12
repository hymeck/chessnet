using ChessNet.Converters;
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
            
            moves.Count.ShouldBe(8);
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
    }
}