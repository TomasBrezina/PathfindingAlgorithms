using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PathfindingAlgorithms
{
    public abstract class EnviromentConstructor
    {
        public abstract Enviroment Construct(Canvas canv);
    }
    public class HexagonGridEnviromentConstructor : EnviromentConstructor
    {
        public (int, int) Shape;
        public override Enviroment Construct(Canvas canv)
        {
            return new HexagonGridEnviroment(canv, Shape);
        }
    }
    public class SquareGridEnviromentConstructor : EnviromentConstructor
    {
        public (int, int) Shape;
        public override Enviroment Construct(Canvas canv)
        {
            return new SquareGridEnviroment(canv, Shape);
        }
    }
    public class PointEnviromentConstructor : EnviromentConstructor
    {
        public (int, int) Shape;
        public double Radius;
        public override Enviroment Construct(Canvas canv)
        {
            return new PointEnviroment(canv, Shape, Radius);
        }
    }
}
