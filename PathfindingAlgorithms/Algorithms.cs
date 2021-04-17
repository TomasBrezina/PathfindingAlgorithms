using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    public static abstract class Algorithm
    {
        protected abstract List<Node> ShortestPath();
    }

    /*
    Breadth First Search
    Unweighted
    O(v+e)
    */
    public static class BFS : Algorithm
    {
        public static override List<Node> ShortestPath(Node StartNode, Node EndNode)
        {
            // Queue for BFS
            Queue<Node> queue = Queue<Node>;


        }
    }
}
