using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using DelaunatorSharp;

namespace PathfindingAlgorithms
{
    /// <summary>
    /// Point Environment
    /// Shape: Circle
    /// Neighnours: random (3-5?)
    /// </summary>
    class PointEnvironment : Environment
    {
        private Vector CanvasSize; 
        private double Radius; // space between
        private const double CircleDiameter = 30; 

        public PointEnvironment(Canvas canv, (int,int) shape, double radius) : base(canv)
        {
            CanvasSize = new Vector(shape.Item1, shape.Item2);
            canv.Width = CanvasSize.X;
            canv.Height = CanvasSize.Y;
            Radius = radius;
            Initialize();
        }

        // Euclidean (Pythagorian) distance 
        public override float HeuristicDist(Node start, Node end)
        {
            return (float)Math.Sqrt(
                Math.Pow(start.Pos.Item1 - end.Pos.Item1, 2) +
                Math.Pow(start.Pos.Item2 - end.Pos.Item2, 2)
            );
        }

        public override void Initialize()
        {
            List<Vector> points = PoissonDicsSampling.Generate(Radius, CanvasSize);
            int ind = 0;
            foreach (var point in points)
            {
                Node node = new Node(
                    ind,
                    ((int)point.X, (int)point.Y),
                    new Vector(point.X, point.Y),
                    new Ellipse
                    {
                        Height = CircleDiameter,
                        Width = CircleDiameter,
                    }
                );
                Nodes.Add(node);
                node.Shape.MouseDown += OnMouseDown; // add event handler

                ind++;
            }
            ConnectNodes();
            // draw nodes on top of edges
            foreach(var node in Nodes)
            {
                Canv.Children.Add(node.Shape); // add rect to canvas
                Canvas.SetLeft(node.Shape, node.OnScreenPos.X - (CircleDiameter / 2));
                Canvas.SetTop(node.Shape, node.OnScreenPos.Y - (CircleDiameter / 2));
            }
        }
        public void ConnectNodes()
        {
            // Initialize
            Dictionary<(int, int), Node> nodesDict = new Dictionary<(int, int), Node>();
            IPoint[] points = new IPoint[Nodes.Count];
            for (int i = 0; i < Nodes.Count; i++)
            {
                var node = Nodes[i];
                var point = new DelaunatorSharp.Point(node.OnScreenPos.X, node.OnScreenPos.Y);
                nodesDict.Add(PointToTupleID(point), node);
                points[i] = point;
            }

            // Create Delanunay Triangulation Generator from point
            Delaunator delaunator = new Delaunator(points);

            foreach (var edge in delaunator.GetEdges())
            {
                var pNode = nodesDict[PointToTupleID(edge.P)];
                var qNode = nodesDict[PointToTupleID(edge.Q)];

                // if edge is too long skip (removes hull edges)
                if (HeuristicDist(pNode, qNode) > 2*Radius) 
                {
                    continue;
                } // skip long edge
                pNode.AddEdge(qNode, HeuristicDist(pNode, qNode));
                qNode.AddEdge(pNode, HeuristicDist(qNode, pNode));

                var line = new Line()
                {
                    X1 = edge.P.X,
                    X2 = edge.Q.X,
                    Y1 = edge.P.Y,
                    Y2 = edge.Q.Y
                };
                line.Stroke = NodeBrushes.Visited;
                line.StrokeThickness = 2;
                Canv.Children.Add(line);
            }
        }

        protected override void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse rect = sender as Ellipse;
            int nodeIndex = (int)rect.Tag; //convert coordinates to node
            Node node = Nodes[nodeIndex];
            SetType(node, MainViewModel.SelectedNodeType);
        }

        private (int, int) PointToTupleID(DelaunatorSharp.Point pt) { return ((int)Math.Round(pt.X), (int)Math.Round(pt.Y)); }
        private (int, int) PointToTupleID(IPoint pt) { return ((int)Math.Round(pt.X), (int)Math.Round(pt.Y)); }
    }
}
