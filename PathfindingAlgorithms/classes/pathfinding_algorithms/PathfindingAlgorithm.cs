using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PathfindingAlgorithms
{
    /// <summary>
    /// Finds the shortest path between Start and End Node.
    /// </summary>
    public abstract class PathfindingAlgorithm
    {
        protected Node StartNode;
        protected Node EndNode;
        protected List<Node> Graph;

        public Path Path;

        /// <summary>
        /// Does path exists? (True, False, Unknown)
        /// </summary>
        public Exist PathExists = Exist.Unknown;

        /// <summary>
        /// Algorithm ended and found a path.
        /// </summary>
        public bool PathFound = false;

        public PathfindingAlgorithm(Node startNode, Node endNode, List<Node> graph)
        {
            StartNode = startNode;
            EndNode = endNode;
            Graph = graph;
            Path = new Path();
        }


        /// <summary>
        /// Makes one step of the algorithm.
        /// </summary>
        public abstract void Step();
    }
}
