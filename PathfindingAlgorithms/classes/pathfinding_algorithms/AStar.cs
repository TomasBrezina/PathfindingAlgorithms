using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    class AStar : PathfindingAlgorithm
    {
        private Node[] Pred;
        private float[] Dist;
        private SimplePriorityQueue<Node,(float,float)> PriorityQueue;
        private Func<Node, Node, float> HeuristicDist;

        public class TupleComparer : IComparer<(float,float)>
        {
            public int Compare((float, float) a, (float, float) b)
            {
                if (a.Item1 > b.Item1) return 1;
                else if (a.Item1 < b.Item1) return -1;
                else {
                    if (a.Item2 > b.Item2) return 1;
                    else if (a.Item2 < b.Item2) return -1;
                    else return 0;
                }
            }
        }
        public AStar(Node startNode, Node endNode, List<Node> graph, Func<Node,Node,float> heuristics) : base(startNode, endNode, graph)
        {
            HeuristicDist = heuristics;

            PriorityQueue = new SimplePriorityQueue<Node,(float,float)>(new TupleComparer());
            Dist = new float[Graph.Count];
            Pred = new Node[Graph.Count];
            
            // Initialize distances to infinity
            for (int i = 0; i < Graph.Count; i++)
            {
                Dist[i] = float.MaxValue;
            }

            // Add start node to the queue
            PriorityQueue.Enqueue(startNode, (0, 0));
            Dist[startNode.ID] = 0; // Distance from StartNode is 0
        }
        private (float, float) GetPriority(Node n1)
        {
            var hd = HeuristicDist(n1, EndNode);
            return ((hd + Dist[n1.ID]), hd);
        }

        public override void Step()
        {
            switch (PathExists)
            {
                // If dont know if path exists
                case Exist.Unknown:
                    SearchStep();
                    break;
                // If reached End Node and sure about the path
                case Exist.True:
                    PathStep();
                    break;
                // If algorithm finished search and didnt find a path
                case Exist.False:
                    break;
            }
        }
        private void SearchStep()
        {
            if (PriorityQueue.Count > 0) // While not empty
            {
                Node node = PriorityQueue.Dequeue(); node.SetState(NodeState.Visited); // visit node
                // if reached end
                if (node.Type == NodeType.End) 
                {
                    PathExists = Exist.True; 
                    Path.Add(node);
                    return;
                }
                foreach (Edge edge in node.Edges)
                {
                    Node nextNode = edge.Node;
                    if (nextNode.Type != NodeType.Wall && nextNode.State != NodeState.Visited)
                    {
                        float newDist = Dist[node.ID] + edge.Weight;
                        if (newDist < Dist[nextNode.ID]) {
                            Dist[nextNode.ID] = newDist;  //update distance
                            Pred[nextNode.ID] = node;
                            (float, float) priority = GetPriority(nextNode);
                            if (PriorityQueue.Contains(nextNode)) PriorityQueue.UpdatePriority(nextNode, priority); //update priority
                            else PriorityQueue.Enqueue(nextNode, priority);
                            nextNode.SetState(NodeState.Revealed);
                        }
                    }
                }
            }
            else if (PathExists == Exist.Unknown) PathExists = Exist.False;
        }
        private void PathStep()
        {
            if (PathExists == Exist.True)
            {
                int id = Path.Last().ID;
                if (id != StartNode.ID)
                {
                    Path.Add(Pred[id]);
                }
                else PathFound = true;
            }
        }
    }
}
