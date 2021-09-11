using System.Linq;
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
    }
}