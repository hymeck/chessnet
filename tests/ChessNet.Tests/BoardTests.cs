using Shouldly;
using Xunit;

namespace ChessNet.Tests
{
    public class BoardTests
    {
        [Fact]
        public void IsOn()
        {
            var board = new Board();
            
            var result = board.IsOnBoard(Square.A1);
            result.ShouldBe(true);
            
            result = board.IsOnBoard(Square.Empty);
            result.ShouldBe(false);
            
            result = board.IsOnBoard((Square)(-11));
            result.ShouldBe(false);
            
            result = board.IsOnBoard((Square)100);
            result.ShouldBe(false);
        }
        
        [Fact]
        public void IsEmptySquare()
        {
            var board = new Board();
            var result = board.IsEmptySquare(Square.Empty);
            result.ShouldBe(true);

            result = board.IsEmptySquare((Square) (-13));
            result.ShouldBe(true);
            
            result = board.IsEmptySquare((Square) (100));
            result.ShouldBe(true);
        }
    }
}