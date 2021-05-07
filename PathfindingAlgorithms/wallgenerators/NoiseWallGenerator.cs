using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    // Randomly generate walls in a graph
    public class NoiseWallGenerator : WallGenerator
    {
        // Number between 0 and 100 - percentage probability of node being a wall
        private int Density;
        private int Index;
        private Random Random;
        public NoiseWallGenerator(Enviroment env, int density) : base(env)
        {
            Density = density;
            Index = 0;
            Random = new Random();
        }
        public override void Step()
        {
            bool skip = false;
            if (!IsFinished)
            {
                Node node = Env.Nodes[Index];
                if (node.Type == NodeType.Empty && Random.Next(100) <= Density)
                {
                    node.SetType(NodeType.Wall);
                }
                else skip = true;
                Index++;
                if (Index >= Env.Nodes.Count) IsFinished = true;
                if (skip) Step();
            }
        }         
    }
}
