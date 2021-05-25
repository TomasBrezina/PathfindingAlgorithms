using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PathfindingAlgorithms
{
    public abstract class EnvironmentConstructor
    {
        public abstract Environment Construct(Canvas canv);
    }
    public class HexagonGridEnvironmentConstructor : EnvironmentConstructor
    {
        public (int, int) Shape;
        public override Environment Construct(Canvas canv)
        {
            return new HexagonGridEnvironment(canv, Shape);
        }
    }
    public class SquareGridEnvironmentConstructor : EnvironmentConstructor
    {
        public (int, int) Shape;
        public override Environment Construct(Canvas canv)
        {
            return new SquareGridEnvironment(canv, Shape);
        }
    }
    public class PointEnvironmentConstructor : EnvironmentConstructor
    {
        public (int, int) Shape;
        public double Radius;
        public override Environment Construct(Canvas canv)
        {
            return new PointEnvironment(canv, Shape, Radius);
        }
    }
}
