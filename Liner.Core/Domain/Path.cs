using System.Collections.Generic;

namespace Liner.Core.Domain
{
    public class Path
    {
        private ICollection<Line> _lines;

        public Path()
        {
            _lines = new List<Line>();
        }
    }
}
