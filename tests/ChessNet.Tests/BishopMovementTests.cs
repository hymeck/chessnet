using ChessNet.Movement;
using Shouldly;
using Xunit;

namespace ChessNet.Tests
{
    public class BishopMovementTests
    {
        [Fact]
        public void CanMove()
        {
            var engine = new ChessEngine();
            IPieceMovement movement = new BishopMovement(10, Color.White, engine);
            var result = movement.CanMove(24, Color.Empty);
            result.ShouldBe(true);
        }
    }
}