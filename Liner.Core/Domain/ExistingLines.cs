using System;
using System.Collections.Generic;
using System.Linq;

namespace Liner.Core.Domain
{
    public class ExistingLines
    {
        public ICollection<Line> Lines { get; private set; }

        public ExistingLines(ICollection<Line> lines)
        {
            Lines = lines ?? throw new ArgumentNullException(nameof(lines));
        }

        public bool IsEmpty => !Lines.Any();
    }
}
