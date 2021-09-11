using System;
using ChessNet.Converters;

namespace ChessNet.Calculation
{
    public class SquareCalculator
    {
        public readonly SquareValueConverter Converter = new();

        public int DeltaX(int from, int to) => Converter.GetX(from) - Converter.GetX(to);
        
        public int DeltaY(int from, int to) => Converter.GetY(from) - Converter.GetY(to);
        public int AbsDeltaX(int from, int to) => Math.Abs(DeltaX(from, to));
        
        public int AbsDeltaY(int from, int to) => Math.Abs(DeltaY(from, to));

        public bool AreOnDiagonalLine(int from, int to)
        {
            var adx = AbsDeltaX(from, to);
            return adx != 0 && adx == AbsDeltaY(from, to);
        }
        
        public int SignX(int from, int to) => Math.Sign(DeltaX(from, to));
        
        public int SignY(int from, int to) => Math.Sign(DeltaY(from, to));

        public (int signX, int signY) Signs(int from, int to) => (SignX(from, to), SignY(from, to));
    }
}