using System;
using ChessNet.Converters;

namespace ChessNet.Calculation
{
    public class SquareCalculator
    {
        private readonly SquareValueConverter _converter = new();

        public int DeltaX(int from, int to) =>
            _converter.GetX(from) - _converter.GetX(to);
        public int DeltaY(int from, int to) =>
            _converter.GetY(from) - _converter.GetY(to);
        public int AbsDeltaX(int from, int to) =>
            Math.Abs(DeltaX(from, to));
        public int AbsDeltaY(int from, int to) =>
            Math.Abs(DeltaY(from, to));
    }
}