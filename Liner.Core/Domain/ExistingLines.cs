using System;
using System.Collections.Generic;

namespace Liner.Core.Domain
{
    public class ExistingLines
    {
        public ICollection<TwoPointLine> TwoPointLines { get; private set; }

        public ExistingLines(ICollection<TwoPointLine> twoPointLines)
        {
            TwoPointLines = twoPointLines ?? throw new ArgumentNullException(nameof(twoPointLines));
        }
    }
}
