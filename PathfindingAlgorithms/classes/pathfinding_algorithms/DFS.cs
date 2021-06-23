using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    /*
    Depth First Search
    Unweighted
    O(v+e)
    */
    public class DFS : PathfindingAlgorithm
    {
        /// <summary>
        /// Array of predecessors.
        /// </summary>
        private Node[] Pred;

        /// <summary>
        /// Queue of nodes as revealed.
        /// </summary>
        private Stack<Node> Stack;

        public DFS(Node startNode, Node endNode, List<Node> graph) : base(startNode, endNode, graph)
        {
            // Initialization
            Stack = new Stack<Node>();
            Pred = new Node[graph.Count];

            // Add start node to the queue
            Stack.Push(startNode);
            startNode.SetState(NodeState.Visited);
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
        // Searching trought graph
        private void SearchStep()
        {
            if (Stack.Count > 0) // if not empty
            {
                Node node = Stack.Pop();
                node.SetState(NodeState.Visited);
                foreach (Edge edge in node.Edges)
                {
                    Node nextNode = edge.Node;
                    if (nextNode.Type != NodeType.Wall && nextNode.State == NodeState.Unseen)
                    {
                        nextNode.SetState(NodeState.Revealed);
                        Pred[nextNode.ID] = node; // Set predecessor 
                        Stack.Push(nextNode); // Add node to queue
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
