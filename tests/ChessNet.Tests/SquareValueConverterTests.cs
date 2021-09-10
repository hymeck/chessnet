using ChessNet.Converters;
using Shouldly;
using Xunit;

namespace ChessNet.Tests
{
    public class SquareValueConverterTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(15, 7, 1)]
        [InlineData(20, 4, 2)]
        public void ToCartesianPosition(int square, int expectedX, int expectedY)
        {
            var squareValueConverter = new SquareValueConverter();
            var (x, y) = squareValueConverter.ToCartesianPosition(square);
            x.ShouldBe(expectedX);
            y.ShouldBe(expectedY);
        }
        
        [Theory]
        [InlineData(7, 1, 15)]
        [InlineData(4, 2, 20)]
        [InlineData(0, 0, 0)]
        public void To1DPosition(int x, int y, int expected)
        {
            var squareValueConverter = new SquareValueConverter();
            var actual = squareValueConverter.To1DPosition((x, y)); // pass tuple for coverage
            actual.ShouldBe(expected);
        }
    }
}