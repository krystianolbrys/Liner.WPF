namespace Liner.Core.Domain
{
    public class Configuration
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public PixelsMargin LineMargin { get; private set; }

        public Configuration(int width, int height, PixelsMargin lineMargin)
        {
            Width = width;
            Height = height;
            LineMargin = lineMargin;
        }
    }
}
