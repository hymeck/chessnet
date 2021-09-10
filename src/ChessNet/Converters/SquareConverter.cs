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
    }
}