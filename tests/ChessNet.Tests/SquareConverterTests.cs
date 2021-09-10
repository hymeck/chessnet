using ChessNet.Converters;
using Shouldly;
using Xunit;

namespace ChessNet.Tests
{
    public class SquareConverterTests
    {
        [Fact]
        public void ToNumericString_A8()
        {
            var squareConverter = new SquareConverter();
            var square = Square.A8;

            var strSquare = squareConverter.ToInt32String(square);
            strSquare.ShouldBe("0", "Square numeric value should be 0");
        }


        [Fact]
        public void FromInt32_NumberIsOutOfValidRange()
        {
            var squareConverter = new SquareConverter();
            var square = squareConverter.FromInt32(-5);
            square.ShouldBe(Square.Empty);
        }
        
        [Fact]
        public void FromInt32_NumberInValidRange()
        {
            var squareConverter = new SquareConverter();
            var square = squareConverter.FromInt32(0);
            square.ShouldBe(Square.A8);
            
            square = squareConverter.FromInt32(64);
            square.ShouldBe(Square.Empty);
        }
        
        [Fact]
        public void FromNumericString_InvalidString()
        {
            var squareConverter = new SquareConverter();
            var square = squareConverter.FromString("ChessNet");
            square.ShouldBe(Square.Empty);
        }
        
        [Fact]
        public void FromNumericString_ValidString()
        {
            var squareConverter = new SquareConverter();
            var square = squareConverter.FromString("0");
            square.ShouldBe(Square.A8);
        }
    }
}
