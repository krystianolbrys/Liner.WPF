using System;
using System.Collections.Generic;

namespace Liner.Core.Domain
{
    public class ExistingLines
    {
        public IReadOnlyCollection<TwoPointLine> TwoPointLines { get; private set; }

        public ExistingLines(IReadOnlyCollection<TwoPointLine> twoPointLines)
        {
            TwoPointLines = twoPointLines ?? throw new ArgumentNullException(nameof(twoPointLines));
        }
    }
}
