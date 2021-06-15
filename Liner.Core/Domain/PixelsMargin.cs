using System;

namespace Liner.Core.Domain
{
    public class PixelsMargin
    {
        public int Value { get; private set; }

        public PixelsMargin(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Margin ) and above allowed");
            }

            Value = value;
        }
    }
}
