using System.Collections.Generic;
using System.Linq;

namespace ChessNet
{
    public class PieceHolder
    {
        // todo: redo
        private readonly Dictionary<int, PieceEntry> _entries;
        
        private int _whiteKing;
        private int _blackKing;

        public int WhiteKing => _whiteKing;
        public int BlackKing => _blackKing;

        public PieceHolder(Dictionary<int, PieceEntry> entries)
        {
            _entries = entries;
            var (w, b) = GetKings(entries);
            _whiteKing = w;
            _blackKing = b;
        }

        public PieceEntry GetPieceEntry(int square)
        {
            _entries.TryGetValue(square, out var entry);
            return entry;
        }

        public (int white, int black) GetKings(Dictionary<int, PieceEntry> entries)
        {
            var w = PieceEntry.WhiteKing();
            var b = PieceEntry.BlackKing();

            var white = (int) Square.Empty;
            var black = (int) Square.Empty;
            foreach (var (square, pieceEntry) in entries)
            {
                if (pieceEntry.IsEmpty)
                    continue;

                if (pieceEntry == w)
                    white = square;

                else
                    black = square;
            }
            
            return (white, black);
        }

        public Dictionary<int, PieceEntry> GetPieces(Color color)
        {
            // todo: plz, without LINQ
            return _entries
                .Where(kvp => kvp.Value.Color == Color.White)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public Dictionary<int, PieceEntry> GetWhite() => GetPieces(Color.White);

        public Dictionary<int, PieceEntry> GetBlack() => GetPieces(Color.Black);

    }
}