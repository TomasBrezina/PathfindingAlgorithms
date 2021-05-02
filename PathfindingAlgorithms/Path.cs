using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathfindingAlgorithms
{

    public class Path
    {
        public List<Node> Nodes;
        public Polyline Polyline;

        public Path()
        {
            Nodes = new List<Node>();
            Polyline = new Polyline
            {
                StrokeThickness = 5,
                Stroke = Brushes.Black
            };
        }
        public void Add(Node node)
        {
            Nodes.Add(node);

            Polyline.Points.Add(new Point(
                node.Pos[0],
                node.Pos[1]
            ));
        }
        public Node Last()
        {
            return Nodes.LastOrDefault();
        }
    }
}
