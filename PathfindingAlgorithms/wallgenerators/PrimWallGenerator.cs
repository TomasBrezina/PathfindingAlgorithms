using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    // Prim's Minimum Spanning Tree algorithm (similar to Dijstras)
    class PrimWallGenerator : WallGenerator
    {
        private Random random;

        private Node[] Pred;
        private float[] Dist;
        private SimplePriorityQueue<Node> PriorityQueue;

        private (int, int)[] Neighbours;
        private (int, int)[] Between;
 
        public PrimWallGenerator(Enviroment env) : base(env) 
        {
            if (env is HexagonEnviroment)
            {
                Neighbours = new (int, int)[6] { (-2, 0), (-1, -2), (+1, -2), (+2, 0), (+1, +2), (-1, +2) };
            }
            else if (env is GridEnviroment)
            {
                Neighbours = new (int, int)[4] { (-2, 0), (0, -2), (2, 0), (0, 2) };
                Between = new (int, int)[4] { (-1, 0), (0, -1), (1, 0), (0, 1) }; // because grid node has 8 edges
            }

            random = new Random();

            PriorityQueue = new SimplePriorityQueue<Node>();
            Dist = new float[env.Nodes.Count];
            Pred = new Node[env.Nodes.Count];

            // Initialize distances to infinity and set every node to wall
            for (int i = 0; i < env.Nodes.Count; i++)
            {
                if (env.Nodes[i].Type == NodeType.Empty) env.Nodes[i].SetType(NodeType.Wall);
                Dist[i] = float.MaxValue;
            }

            // Add random starting node to the queue
            Node startNode = env.Nodes[random.Next(env.Nodes.Count)];
            PriorityQueue.Enqueue(startNode, 0);
            Dist[startNode.ID] = 0; // Distance from StartNode is 0
        }

        public override void Step()
        {
        }
    }
}
