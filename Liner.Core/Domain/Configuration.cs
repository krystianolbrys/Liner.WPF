namespace Liner.Core.Domain
{
    public class Configuration
    {
        public Configuration(int width, int height, int lineMarginInPixels)
        {
            Width = width;
            Height = height;
            LineMarginInPixels = lineMarginInPixels;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int LineMarginInPixels { get; private set; }
    }
}
