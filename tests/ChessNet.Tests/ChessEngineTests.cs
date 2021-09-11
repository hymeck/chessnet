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
            var picked = engine.CanPickPiece(-10, 44);
            picked.ShouldBe(false);
        }
        
        [Fact]
        public void PickPiece_EmptyFromBoard()
        {
            var engine = new ChessEngine();
            var picked = engine.CanPickPiece(0, 0);
            picked.ShouldBe(false);
        }
        
        [Fact]
        public void PickPiece_TakeKnight()
        {
            var engine = new ChessEngine();
            var picked = engine.CanPickPiece(19);
            picked.ShouldBe(true);
        }
    }
}