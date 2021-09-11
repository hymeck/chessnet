namespace ChessNet.Converters
{
    public class SquareConverter
    {
        public string ToInt32String(Square square) => ((int) square).ToString();

        public Square FromInt32(int square) =>
            // todo: check range more efficiently (may be in branchless fashion)
            square is >= 0 and <= 63
                ? (Square) square 
                : Square.Empty;

        public Square FromString(string square) =>
            int.TryParse(square, out var numericSquare) 
                ? FromInt32(numericSquare)
                : Square.Empty;

        public Square FromCartesian(int squareX, int squareY)
        {
            if (squareX is < 0 or > 63 || squareY is < 0 or > 63)
                return Square.Empty;
            var squareValueConverter = new SquareValueConverter();
            return (Square)squareValueConverter.To1DPosition(squareX, squareY);
        }
    }
}