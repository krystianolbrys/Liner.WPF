using System;
using System.Collections.Generic;
using System.Linq;

namespace Liner.Core.Domain
{
    public class ExistingLines
    {
        public ICollection<TwoPointLine> TwoPointLines { get; private set; }

        public ExistingLines(ICollection<TwoPointLine> twoPointLines)
        {
            TwoPointLines = twoPointLines ?? throw new ArgumentNullException(nameof(twoPointLines));
        }

        public bool IsEmpty => !TwoPointLines.Any();
    }
}
