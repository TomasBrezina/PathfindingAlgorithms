using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PathfindingAlgorithms
{
    public abstract class Algorithm
    {
        protected Node StartNode;
        protected int V; // Number of nodes

        public Path Path;
        public bool PathExists = false;
        public bool PathFound = false;

        public Algorithm(Node startNode, int v)
        {
            StartNode = startNode;
            V = v;
            Path = new Path();
        }
        public abstract void Step();
    }

    /*
    Breadth First Search
    Unweighted
    O(v+e)
    */
    public class BFS : Algorithm
    {
        private Node[] Pred;
        private Queue<Node> Queue;

        public BFS(Node startNode, int v) : base(startNode, v)
        {
            // Initialization
            Queue = new Queue<Node>();
            Pred = new Node[v];

            // Add start node to the queue
            Queue.Enqueue(startNode);
            startNode.SetState(NodeState.Visited);
        }
        public override void Step()
        {
            if(!PathExists) SearchStep(); 
            else PathStep(); // If End node is already visited
        }
        // Searching trought graph
        private void SearchStep()
        {
            if (Queue.Count > 0) // While not empty
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
                            PathExists = true; // If reached end node
                            Path.Add(nextNode); // Add end node to path
                        }
                    }
                }
            }
        }
        // Backtracking for path if reached both start and end node
        private void PathStep()
        {
            if(PathExists)
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
