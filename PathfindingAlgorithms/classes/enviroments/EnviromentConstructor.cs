using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PathfindingAlgorithms
{
    public class EnviromentConstructor
    {
        public Type EnvType;
        public (int, int) Shape;

        public Enviroment GetEnv(Canvas canv)
        {
            if (EnvType == typeof(HexagonGridEnviroment))
            {
                return new HexagonGridEnviroment(canv, Shape);
            } else if (EnvType == typeof(SquareGridEnviroment))
            {
                return new SquareGridEnviroment(canv, Shape);
            } else
            {
                return null;
            }
        }
    }
}
