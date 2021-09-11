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
            var picked = engine.PickPiece(-10, 44);
            picked.ShouldBe(false);
        }
        
        [Fact]
        public void PickPiece_EmptyFromBoard()
        {
            var engine = new ChessEngine();
            var picked = engine.PickPiece(0, 0);
            picked.ShouldBe(false);
        }
    }
}