using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    /*
    Breadth First Search
    Unweighted
    O(v+e)
    */
    public class BFS : PathfindingAlgorithm
    {
        private Node[] Pred;
        private Queue<Node> Queue;

        public BFS(Node startNode, Node endNode, List<Node> graph) : base(startNode, endNode, graph)
        {
            // Initialization
            Queue = new Queue<Node>();
            Pred = new Node[graph.Count];

            // Add start node to the queue
            Queue.Enqueue(startNode);
            startNode.SetState(NodeState.Visited);
        }
        public override void Step()
        {
            switch(PathExists)
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
        // Searching trought graph
        private void SearchStep()
        {
            if (Queue.Count > 0) // if not empty
            {
                Node node = Queue.Dequeue();
                node.SetState(NodeState.Visited);
                foreach (Edge edge in node.Edges)
                {
                    Node nextNode = edge.Node;
                    if (nextNode.Type != NodeType.Wall && nextNode.State == NodeState.Unseen)
                    {
                        nextNode.SetState(NodeState.Revealed);
                        Pred[nextNode.ID] = node; // Set predecessor 
                        Queue.Enqueue(nextNode); // Add node to queue
                        if (nextNode.Type == NodeType.End)
                        {
                            PathExists = Exist.True; // If reached end node
                            Path.Add(nextNode); // Add end node to path
                        }
                    }
                }   
            }
            else if (PathExists == Exist.Unknown) PathExists = Exist.False;
        }
        // Backtracking for path if reached both start and end node
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
