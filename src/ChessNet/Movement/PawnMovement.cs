using System;
using ChessNet.Calculation;

namespace ChessNet.Movement
{
    public class PawnMovement : IPieceMovement
    {
        private readonly SquareCalculator _calculator = new();
        
        private readonly int _pieceSquare;
        private readonly int _pieceColor;
        private readonly ChessEngine _engine;

        public PawnMovement(int pieceSquare, int pieceColor, ChessEngine engine)
        {
            _pieceSquare = pieceSquare;
            _pieceColor = pieceColor;
            _engine = engine;
        }

        public Move CanMove(int toSquare, int toColor)
        {
            // illegal, no capture, capture:
            //  illegal - invalid move or check occurs after move;
            //  no capture - _toColor is neither white nor black
            //  capture - _pieceColor does not equal toColor
            
            if ((_pieceColor & toColor) != 0) // the same color
                return Move.Illegal;

            // -- specific --
            // todo: optimize by bitwise operations 
            var black = (_pieceColor & (int) Color.White) == 0 ? 1 : 0;
            var emptySquare = toColor == (int) Color.Empty ? 1 : 0;
            var step =  black == 1 ? 1 : -1;
            var initialRow = black == 1 ? 2 : 5;
            
            var dx = _calculator.DeltaX(_pieceSquare, toSquare);
            var dy = _calculator.DeltaY(_pieceSquare, toSquare);
            var adx = Math.Abs(dx);

            var dyXorStep = dy ^ step;
            var dyEqualStep = dyXorStep ^ 0;
            
            var adxXorOne = adx ^ 1;
            var adxEqualOne = adxXorOne ^ 0;

            var toY = _calculator.Converter.GetY(toSquare);

            var rankXorY = initialRow ^ toY;
            var rankEqualY = rankXorY & rankXorY;
            
            var notEmptySquare = emptySquare ^ emptySquare;

            var dxXorZero = dx ^ 0;
            var dxEqualZero = dxXorZero ^ dxXorZero;
            
            var forward = emptySquare & dxEqualZero & dyEqualStep; // 0 or 1
            var capture = notEmptySquare & adxEqualOne & dyEqualStep; // 0 or 1

            var doubleStep = step << 1;
            var dyXorDoubleStep = dy ^ doubleStep;
            var dyEqualDoubleStep = dyXorDoubleStep ^ doubleStep;
            
            var push = emptySquare & rankEqualY & dxEqualZero & dyEqualDoubleStep; // 0 or 1
            // todo: bleat
            if (push == 1)
            {
                var (x, y) = _calculator.Converter.ToCartesianPosition(_pieceSquare);
                var nextSquare = _calculator.Converter.To1DPosition(x, y + step);
                var nextColor = (int) _engine.UnsafeGetPieceEntry(nextSquare).Color;
                var emptyColor = nextColor ^ nextColor;
                push = emptyColor;
            }
            
            // en passant
            var eSquare = (int) _engine.EnPassantTargetSquare;
            var toXorESquare = toSquare ^ eSquare;
            var toEqualESquare = toXorESquare ^ toXorESquare;
            var enPassant = toEqualESquare & adxEqualOne & dyEqualStep;
            
            if ((forward | capture | push | enPassant) == 0)
                return Move.Illegal;
            // -- specific --
            
            var checkAfterMove = 0; // todo: implement check of check after moving
            if (checkAfterMove == 1) // illegal
                return Move.Illegal;

            var lastFileReached = ((toSquare ^ 1) ^ (toSquare ^ 1)) | ((toSquare ^ 7) ^ (toSquare ^ 7));
            if (forward == 1 || push == 1)
            {
                return lastFileReached == 1 
                    ? Move.Promotion 
                    : Move.NoCapture;
            }

            if (capture == 1)
                return Move.Capture;

            return enPassant == 1 
                ? Move.EnPassant 
                : Move.NoCapture;
        }
    }
}