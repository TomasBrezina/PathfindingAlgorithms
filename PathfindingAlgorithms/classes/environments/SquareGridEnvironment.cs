using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PathfindingAlgorithms
{
    /// <summary>
    /// Square grid Environment
    /// Shape: Rectangle
    /// Neighnours: 8
    /// </summary>
    public class SquareGridEnvironment : GridEnvironment
    {
        private double RectSize; // Size of one Rectangle
        public SquareGridEnvironment(Canvas canv, (int,int) shape) : base(canv, shape)
        {
            RectSize = (DefaultCanvWidth / shape.Item1);
            canv.Width = RectSize * shape.Item1;
            canv.Height = RectSize * shape.Item2;
            Initialize();
        }

        public override void Initialize()
        {
            int index = 0;
            for (int j = 0; j < Shape.Item2; j++)
            {
                for (int i = 0; i < Shape.Item1; i++)
                {
                    Node node = new Node(
                        index,
                        (i, j),
                        new Vector((i * RectSize) + (RectSize / 2), (j * RectSize) + (RectSize / 2)),
                        new Rectangle
                        {
                            Height = RectSize,
                            Width = RectSize,
                        }
                    );
                    Nodes.Add(node);
                    node.Shape.MouseDown += OnMouseDown; // add event handler
                    Canv.Children.Add(node.Shape); // add rect to canvas
                    Canvas.SetLeft(node.Shape, (i * RectSize));
                    Canvas.SetTop(node.Shape, (j * RectSize));
                    index += 1;
                }
            }
            ConnectNodes();
        }

        /// <summary> Creates edge between grid neighbours </summary>
        private void ConnectNodes()
        {
            for (int i = 0; i < Shape.Item1; i++)
            {
                for (int j = 0; j < Shape.Item2; j++)
                {
                    // Edges coords shift 
                    int[][] shifts = new int[4][] { new int[] { -1, 0 }, new int[] { -1, -1 }, new int[] { 0, -1 }, new int[] { 1, -1 } };
                    float[] weights = new float[] { 1, 1.4f, 1, 1.4f };

                    for (int shiftIndex = 0; shiftIndex < shifts.Length; shiftIndex++)
                    {
                        int x = i + shifts[shiftIndex][0]; int y = j + shifts[shiftIndex][1];
                        if (x >= 0 && x < Shape.Item1 && y >= 0 && y < Shape.Item2)
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
        // Diagonal distance
        public override float HeuristicDist(Node start, Node end)
        {
            float dx = Math.Abs(start.Pos.Item1 - end.Pos.Item1);
            float dy = Math.Abs(start.Pos.Item2 - end.Pos.Item2);
            // D = 1; D2 = sqrt(2)
            // D * (dx + dy) + (D2 - 2 * D) * min(dx, dy)
            return (float)(dx + dy + ((1.41 - 2) * Math.Min(dx, dy)));
        }

        // convert 2d coordinates to list index of a node
        public override int CoordsToIndex(int x, int y)
        {
            return x + (y * Shape.Item1);
        }
        // convert list index to 2d coordinates of a node
        public override (int, int) IndexToCoords(int i)
        {
            int x = i % Shape.Item1;
            int y = i / Shape.Item1;
            return (x, y);
        }

        protected override void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            int nodeIndex = (int) rect.Tag; //convert coordinates to node
            Node node = Nodes[nodeIndex];
            SetType(node, MainViewModel.SelectedNodeType);
        }
    }
}
