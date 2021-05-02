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
        protected Node EndNode;
        protected int V; // Number of nodes

        public Path Path;
        public bool PathExists = false;
        public bool PathFound = false;

        public Algorithm(Node startNode, Node endNode, int v)
        {
            StartNode = startNode;
            EndNode = endNode;
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
        private bool[] Visited;
        private Node[] Pred;
        private Queue<Node> Queue;

        public BFS(Node startNode, Node endNode, int v) : base(startNode, endNode, v)
        {
            // Initialization
            Queue = new Queue<Node>();
            Visited = new bool[v];
            Pred = new Node[v];

            // Fill values
            for (int i = 0; i < v; i++)
            {
                Visited[i] = false;
            }

            // Add start node to the queue
            Queue.Enqueue(startNode);
            Visited[startNode.ID] = true;
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
                node.MarkAs(NodeBrushes.Visited);
                foreach (Edge edge in node.Edges)
                {
                    Node nextNode = edge.Node;
                    if (!Visited[nextNode.ID])
                    {
                        nextNode.MarkAs(NodeBrushes.Observed);
                        Visited[nextNode.ID] = true; // Mark node as visited
                        Pred[nextNode.ID] = node; // Set predecessor 
                        Queue.Enqueue(nextNode); // Add node to queue
                        if (nextNode.ID == EndNode.ID)
                        {
                            PathExists = true; // If reached end node
                            Path.Add(EndNode); // Add end node to path
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
