using System.Collections.Generic;

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
    }
}
