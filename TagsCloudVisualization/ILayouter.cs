using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    interface ILayouter
    {
        IEnumerable<Rectangle> PutRectangles(IEnumerable<Size> sizes);
    }
}
