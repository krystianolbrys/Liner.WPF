using System.Collections.Generic;

namespace Liner.Core.Domain
{
    public class Path
    {
        public ICollection<Line> Lines { get; private set; }

        public Path()
        {
            Lines = new List<Line>();
        }

        public void AddLine(Line line) => Lines.Add(line);
    }
}
