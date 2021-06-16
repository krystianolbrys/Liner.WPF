using System.Collections.Generic;
using System.Linq;

namespace Liner.Core.Domain
{
    public class Path
    {
        public ICollection<TwoPointLine> Lines { get; private set; }

        public Path()
        {
            Lines = new List<TwoPointLine>();
        }

        public void AddLine(TwoPointLine line) => Lines.Add(line);

        public bool IsPathFound => Lines.Any();
    }
}
