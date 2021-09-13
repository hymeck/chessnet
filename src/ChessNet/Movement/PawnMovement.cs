using System;
using ChessNet.Calculation;
using ChessNet.Extensions;

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
            
            // if ((_pieceColor & toColor) != 0) // the same color
            if (_pieceColor.BooleanCompare(toColor) == 1) // the same color
                return Move.Illegal;

            // -- specific --
            var white = _pieceColor.BooleanCompare((int) Color.White);
            var emptySquare = toColor.BooleanCompare((int) Color.Empty);
            
            // todo: damn, branches
            var step =  white == 1 ? 1 : -1;
            var startRank = white == 1 ? 6 : 1;
            
            var dx = _calculator.DeltaX(_pieceSquare, toSquare);
            var dy = _calculator.DeltaY(_pieceSquare, toSquare);
            var adx = Math.Abs(dx);

            var dxEqualZero = dx.BooleanCompare(0);
            var dyEqualStep = dy.BooleanCompare(step);

            var forward = emptySquare & dxEqualZero & dyEqualStep; // 0 or 1

            var notEmptySquare = emptySquare ^ 1;
            var adxEqualOne = adx.BooleanCompare(1);
            
            var capture = notEmptySquare & adxEqualOne & dyEqualStep; // 0 or 1

            var dyEqualDoubleStep = dy.BooleanCompare(step << 1);
            var rankEqualY = _calculator.Converter.GetY(_pieceSquare).BooleanCompare(startRank);
            
            var push = emptySquare & rankEqualY & dxEqualZero & dyEqualDoubleStep; // 0 or 1
            // todo: bleat
            if (push == 1)
            {
                var (x, y) = _calculator.Converter.ToCartesianPosition(_pieceSquare);
                var nextSquare = _calculator.Converter.To1DPosition(x, y - step);
                var nextColor = (int) _engine.UnsafeGetPieceEntry(nextSquare).Color;
                var emptyColor = nextColor.BooleanCompare((int) Color.Empty);
                push = emptyColor;
            }
            
            // en passant
            var eSquare = (int) _engine.EnPassantTargetSquare;
            var toEqualESquare = toSquare.BooleanCompare(eSquare);
            var enPassant = toEqualESquare & adxEqualOne & dyEqualStep;
            
            if ((forward | capture | push | enPassant) == 0)
                return Move.Illegal;
            // -- specific --
            
            // todo: "how do we can optimize that?" (c) xD
            if (forward == 1 || push == 1)
            {
                var lastFileReached = toSquare.BooleanCompare(0) | toSquare.BooleanCompare(7);
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

        public int CanMoveWithCheckAfterMove(int toSquare, int toColor)
        {
            return _engine.CanCurrentKingBeCaptured(toSquare, toColor);
        }
    }
}