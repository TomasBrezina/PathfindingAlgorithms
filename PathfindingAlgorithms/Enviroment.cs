using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathfindingAlgorithms
{
    abstract class Enviroment
    {
        public Node StartNode;
        public Node EndNode;

        public List<Node> Nodes;
        protected List<Path> Paths;
        protected Canvas Canv;

        public Enviroment(Canvas canv)
        {
            Paths = new List<Path>();
            Nodes = new List<Node>();
            Canv = canv;
        }
        public abstract int CoordsToIndex(int x, int y);
        public abstract int ScreenCoordsToIndex(double x, double y);

        public abstract void Initialize();
        protected abstract void OnMouseDown(object sender, MouseButtonEventArgs e);
        protected void SetType(Node node, NodeType type)
        {
            switch (node.Type)
            {
                case NodeType.Start:
                    StartNode = null;
                    break;
                case NodeType.End:
                    EndNode = null;
                    break;
            }
            switch (type)
            {
                case NodeType.Start:
                    if (StartNode != null) StartNode.SetDefaultType();
                    node.SetType(type);
                    StartNode = node;
                    break;
                case NodeType.End:
                    if (EndNode != null) EndNode.SetDefaultType();
                    node.SetType(type);
                    EndNode = node;
                    break;
                default:
                    node.SetType(type);
                    break;
            }  
        }
        public void Clear()
        {
            foreach (Node node in Nodes) node.SetState(NodeState.Unseen);
        }
        public void AddPath(Path path)
        {
            Paths.Add(path);
            Canv.Children.Add(path.Polyline);
        }
        public void RemovePaths()
        {
            foreach (Path path in Paths)
            {
                Canv.Children.Remove(path.Polyline);
            }
            Paths.Clear();
        }
    }

    class Grid : Enviroment
    {
        private double RectHeight;
        private double RectWidth;
        private int[] Shape;

        public Grid(Canvas canv, int[] shape) : base(canv)
        {
            Shape = shape;
            Initialize();

            StartNode = Nodes.First();
            EndNode = Nodes.Last();

            StartNode.SetType(NodeType.Start);
            EndNode.SetType(NodeType.End);
        }

        public override void Initialize()
        {
            RectHeight = Canv.ActualHeight / Shape[1];
            RectWidth = Canv.ActualWidth / Shape[0];

            int index = 0;
            for (int j = 0; j < Shape[1]; j++)
            {
                for (int i = 0; i < Shape[0]; i++)
                {
                    GridNode node = new GridNode(
                        index,
                        new double[2] {
                            (i * RectWidth) + (RectWidth / 2),
                            (j * RectHeight) + (RectHeight / 2)
                        },
                        new Rectangle
                        {
                            Height = RectHeight,
                            Width = RectWidth,
                        }
                    );
                    Nodes.Add(node);
                    node.Rect.MouseDown += OnMouseDown; // add event handler
                    Canv.Children.Add(node.Rect); // add rect to canvas
                    Canvas.SetLeft(node.Rect, (i * RectWidth)); 
                    Canvas.SetTop(node.Rect, (j * RectHeight));
                    index += 1;
                }
            }
            ConnectNodes();
        }
        private void ConnectNodes()
        {
            for (int i = 0; i < Shape[0]; i++)
            {
                for (int j = 0; j < Shape[1]; j++)
                {
                    int[][] shifts = new int[4][] { new int[] { -1, 0 }, new int[] { -1, -1 }, new int[] { 0, -1 }, new int[] { 1, -1 } };
                    float[] weights = new float[] { 1, 1.4f, 1, 1.4f };
                    for (int shiftIndex = 0; shiftIndex < shifts.Length; shiftIndex++)
                    {
                        int x = i + shifts[shiftIndex][0]; int y = j + shifts[shiftIndex][1];
                        if (x >= 0 && x < Shape[0] && y >= 0 && y < Shape[1])
                        {
                            float weight = weights[shiftIndex];
                            Node thisNode = Nodes[CoordsToIndex(i, j)];
                            Node neighbourNode = Nodes[CoordsToIndex(x, y)];
                            thisNode.AddEdge(neighbourNode, weight);
                            neighbourNode.AddEdge(thisNode, weight);
                        }
                    }
                }
            }
        }
        // convert 2d coordinates to list index of a node
        public override int CoordsToIndex(int x, int y)
        {
            return x + (y * Shape[0]);
        }
        // convert on-screen coordinates to index of node
        public override int ScreenCoordsToIndex(double x, double y)
        {
            int xi = (int)Math.Round(x / RectWidth);
            int yi = (int)Math.Round(y / RectHeight);
            return CoordsToIndex(xi, yi);
        }
        protected override void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            int nodeIndex = ScreenCoordsToIndex(Canvas.GetLeft(rect), Canvas.GetTop(rect)); //convert coordinates to node
            Node node = Nodes[nodeIndex];
            SetType(node, MainViewModel.SelectedNodeType);
        }
    }
}
