namespace ChessNet.Extensions
{
    public static class Int32Extensions
    {
        /// <summary>
        /// Compares two signed 32-bit integers with no conditional branches.
        /// </summary>
        /// <param name="val1">A signed 32-bit integer value</param>
        /// <param name="val2">A signed 32-bit integer value</param>
        /// <returns>Returns 1 if arguments are equal; otherwise, 0.</returns>
        public static int BooleanCompare(this int val1, int val2)
        {
            var xor = val1 ^ val2; // 0 or nonzero
            var sign = unchecked(xor >> 31 | (int) ((uint) -xor >> 31)); // Math.Sign(int): 0 or 1 or -1
            var mask = sign >> 31; // ?
            var abs = unchecked((sign ^ mask) - mask); // 0 or 1
            return abs ^ 1; // 0 -> 1; 1 -> 0
        }
    }
}