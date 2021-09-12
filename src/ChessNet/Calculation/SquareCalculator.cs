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

        public int AreOnDiagonalLine(int from, int to)
        {
            var adx = AbsDeltaX(from, to);
            return adx != 0 && adx == AbsDeltaY(from, to) ? 1 : 0;
        }
        
        public int SignX(int from, int to) => Math.Sign(DeltaX(from, to));
        
        public int SignY(int from, int to) => Math.Sign(DeltaY(from, to));

        public (int signX, int signY) Signs(int from, int to) => (SignX(from, to), SignY(from, to));
        
        public int AreOnFile(int from, int to) => AbsDeltaY(from, to) != 0 && AbsDeltaX(from, to) == 0 ? 1 : 0;
        
        public int AreOnRank(int from, int to) => AbsDeltaY(from, to) == 0 && AbsDeltaX(from, to) != 0 ? 1 : 0;
    }
}