using System.Collections.Generic;

namespace ChessNet
{
    public class PieceHolder
    {
        // todo: redo
        public readonly Dictionary<int, PieceEntry> Entries = new(64);

        public PieceEntry GetPieceEntry(int square)
        {
            Entries.TryGetValue(square, out var entry);
            return entry;
        }
    }
}