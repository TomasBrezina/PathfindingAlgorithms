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

        public Algorithm(Node startNode, Node endNode, int v)
        {
            StartNode = startNode;
            EndNode = endNode;
            V = v;
        }
    }

    /*
    Breadth First Search
    Unweighted
    O(v+e)
    */
    public class BFS : Algorithm
    {
        public bool[] Visited;
        public int[] Pred;
        public Queue<Node> Queue;

        public BFS(Node startNode, Node endNode, int v) : base(startNode, endNode, v)
        {
            // Initialization
            Queue = new Queue<Node>();
            Visited = new bool[v];
            Pred = new int[v];

            // Fill values
            for (int i = 0; i < v; i++)
            {
                Visited[i] = false;
                Pred[i] = -1;
            }

            // Add start node to the queue
            Queue.Enqueue(startNode);
            Visited[startNode.ID] = true;
        }
        public void Run()
        {

        }
        public bool Step()
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
                        Pred[nextNode.ID] = node.ID; // Set predecessor 
                        Queue.Enqueue(nextNode); // Add node to queue
                        if (nextNode.ID == EndNode.ID) return true; // If reached end
                    }
                }
            }
            return false;
        }
        public List<int> FindPath()
        {
            List<int> path = new List<int>();
            int index = EndNode.ID;
            while (index != StartNode.ID)
            {
                index = Pred[index];
                path.Add(index);
            }
            path.Reverse();
            return path;
        }
    }
}
