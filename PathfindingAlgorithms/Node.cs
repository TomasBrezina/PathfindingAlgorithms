using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathfindingAlgorithms
{
    public static class NodeBrushes
    {
        public static SolidColorBrush Start = Brushes.Blue;
        public static SolidColorBrush End = Brushes.Green;

        public static SolidColorBrush Default = Brushes.White;
        public static SolidColorBrush Visited = Brushes.DarkGray;
        public static SolidColorBrush Observed = Brushes.LightGray;
    }
    public enum NodeType
    {
        Start,
        End,
        Empty,
        Wall,
    }

    public class Edge
    {
        public Node Node;
        public float Weight;

        public Edge(Node toNode, float weight)
        {
            Node = toNode;
            Weight = weight;
        }
    }
    public class Node
    {
        public int ID;
        public double[] Pos;
        public List<Edge> Edges;
        public NodeType Type;

        protected Node(int id, double[] pos)
        {
            ID = id;
            Pos = pos;
            Edges = new List<Edge>();
            Type = NodeType.Empty;
        }
        public virtual void Clear() { }
        public virtual void MarkAs(Brush b) { }

        // Add neighbour and weight to Neigbours list 
        public void AddEdge(Node node, float weight)
        {
            Edges.Add(new Edge(node, weight));
        }
    }
    class GridNode : Node
    {
        public Rectangle Rect;
        public GridNode(int id, double[] pos, Rectangle rect) : base(id, pos)
        { 
            Rect = rect;
            Rect.Fill = NodeBrushes.Default;
            Rect.Stroke = Brushes.Gray;
            Rect.StrokeThickness = 0.5;
            Rect.PreviewMouseLeftButtonDown += Rect_MouseLeftButtonDown;
            Rect.PreviewMouseRightButtonDown += Rect_MouseRightButtonDown;
        }
        public override void MarkAs(Brush b)
        {
            Rect.Fill = b;
        }

        private void Rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            rect.Fill = Brushes.Red;
        }

        private void Rect_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            rect.Fill = Brushes.White;
        }
    }
}
