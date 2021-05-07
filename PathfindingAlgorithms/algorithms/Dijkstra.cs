using Priority_Queue;
using System.Collections.Generic;

namespace PathfindingAlgorithms
{
    class Dijkstra : Algorithm
    {
        private Node[] Pred;
        private float[] Dist;
        private SimplePriorityQueue<Node> PriorityQueue;

        public Dijkstra(Node startNode, List<Node> graph) : base(startNode, graph)
        {
            PriorityQueue = new SimplePriorityQueue<Node>();
            Dist = new float[Graph.Count];
            Pred = new Node[Graph.Count];

            // Initialize distances to infinity
            for (int i = 0; i < Graph.Count; i++)
            {
                Dist[i] = float.MaxValue;
            }

            // Add start node to the queue
            PriorityQueue.Enqueue(startNode, 0);
            Dist[startNode.ID] = 0; // Distance from StartNode is 0
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
                // If algorithm finished search and didnt found a path
                case Exist.False:
                    break;
            }
        }
        private void SearchStep()
        {
            if (PriorityQueue.Count > 0) // While not empty
            {
                Node node = PriorityQueue.Dequeue();
                node.SetState(NodeState.Visited);
                if (node.Type == NodeType.End)
                {
                    PathExists = Exist.True;
                    Path.Add(node);
                }
                foreach (Edge edge in node.Edges)
                {
                    Node nextNode = edge.Node;
                    if (nextNode.Type != NodeType.Wall && nextNode.State != NodeState.Visited)
                    {
                        float newDist = Dist[node.ID] + edge.Weight;
                        if (newDist < Dist[nextNode.ID])
                        {
                            Pred[nextNode.ID] = node;
                            Dist[nextNode.ID] = newDist; // Set the new distance from StartNode
                            PriorityQueue.Enqueue(nextNode, newDist); // Update priority 
                            nextNode.SetState(NodeState.Revealed);
                        }        
                    }
                }
            } else if (PathExists == Exist.Unknown) PathExists = Exist.False;
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
