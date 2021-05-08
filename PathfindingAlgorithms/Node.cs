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
        public NodeState State;

        protected Node(int id, double[] pos)
        {
            ID = id;
            Pos = pos;
            Edges = new List<Edge>();
            Type = NodeType.Empty;
            State = NodeState.Unseen;
        }
        public virtual void MarkAs(Brush b) { }
        public void SetDefaultState() { SetState(NodeState.Unseen); }
        public void SetDefaultType() { SetType(NodeType.Empty); }

        // Set state of the node
        public void SetState(NodeState state) {
            State = state;
            if (Type == NodeType.Empty)
            {
                MarkAs(NodeBrushes.FromState[state]);
            }
        }
        // Set type of the node
        public void SetType(NodeType type)
        {
            Type = type;
            MarkAs(NodeBrushes.FromType[type]);
        }

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
            Rect.Fill = NodeBrushes.Empty;
            Rect.Stroke = Brushes.Gray;
            Rect.StrokeThickness = 0.5;
        }
        public override void MarkAs(Brush b) { Rect.Fill = b; }
    }

    class HexNode : Node
    {
        public Polygon Pol;
        public HexNode(int id, double[] pos, Polygon pol) : base(id, pos)
        {
            Pol = pol;
            Pol.Tag = id;
            Pol.Fill = NodeBrushes.Empty;
            Pol.Stroke = Brushes.Gray;
            Pol.StrokeThickness = 0.5;
        }
        public override void MarkAs(Brush b) { Pol.Fill = b; }

    }
}
