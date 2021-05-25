using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PathfindingAlgorithms
{
    public abstract class GridEnvironment : Environment
    {
        public (int,int) Shape;

        public GridEnvironment(Canvas canv, (int, int) shape) : base(canv)
        {
            Shape = shape; // 2d shape of a grid
        }
        
        public abstract (int, int) IndexToCoords(int i);
        public abstract int CoordsToIndex(int x, int y);
    }
}
