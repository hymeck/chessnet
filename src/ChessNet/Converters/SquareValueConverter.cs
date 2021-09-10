namespace ChessNet.Converters
{
    public class SquareValueConverter
    {
        // https://stackoverflow.com/questions/11040646/faster-modulus-in-c-c
        public (int x, int y) ToCartesianPosition(int square) =>
            // (square % 8, square / 8)
            (square & 7, square >> 3);

        public int To1DPosition(int x, int y) => (y << 3) + x;

        public int To1DPosition((int x, int y) cartesianSquare) => 
            To1DPosition(cartesianSquare.x, cartesianSquare.y);
    }
}