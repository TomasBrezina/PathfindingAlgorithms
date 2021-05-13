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
        protected List<Node> Graph;

        public Path Path;
        public Exist PathExists = Exist.Unknown;
        public bool PathFound = false;

        public Algorithm(Node startNode, List<Node> graph)
        {
            StartNode = startNode;
            Graph = graph;
            Path = new Path();
        }
        public abstract void Step();
    }
}
