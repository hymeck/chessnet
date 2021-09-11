namespace ChessNet
{
    public enum Color
    {
        Empty = 0,
        White = 1,
        Black = 2
    }

    public static class ColorExtensions
    {
        // https://www.youtube.com/watch?v=BoE5Y6Xkm6w
        public static string GetString(this Color color) =>
            color switch
            {
                Color.White => nameof(Color.White),
                Color.Black => nameof(Color.Black),
                Color.Empty => nameof(Color.Empty),
                _ => ""
            };
    }
}