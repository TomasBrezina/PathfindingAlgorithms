using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    class ClearWalls : WallGenerator
    {
        public ClearWalls(Enviroment env) : base(env) { }
        public override void Step()
        {
            foreach (Node node in Env.Nodes)
            {
                if (node.Type == NodeType.Wall) node.SetType(NodeType.Empty);
            }
            IsFinished = true;
        }
    }
}
