namespace ChessNet.Converters
{
    public class SquareValueConverter
    {
        // https://stackoverflow.com/questions/11040646/faster-modulus-in-c-c
        public int GetX(int square) => square & 7; // square % 8
        public int GetY(int square) => square >> 3; // square / 8
        
        public (int x, int y) ToCartesianPosition(int square) =>
            (GetX(square), GetY(square));

        public int To1DPosition(int x, int y) => (y << 3) + x;

        public int To1DPosition((int x, int y) cartesianSquare) => 
            To1DPosition(cartesianSquare.x, cartesianSquare.y);
    }
}