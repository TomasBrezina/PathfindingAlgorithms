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
    class HexagonEnviroment : Enviroment
    {
        private double HexRadius; 
        private double HexWidth; // 2 * HexRadius
        private double HexHeight; // Sqrt(3) * HexRadius
        public HexagonEnviroment(Canvas canv, int[] shape) : base(canv, shape)
        {
            // radius to fully fill width or height of canvas with hexagons
            HexRadius = Math.Min(
                (canv.ActualWidth / shape[0]) / Math.Sqrt(3), // radius to fit width
                (canv.ActualHeight / shape[1]) / 1.5 // radius to fit height
            );
            HexWidth = HexRadius * Math.Sqrt(3);
            HexHeight = HexRadius * 2;

            Initialize();
        }
        private Point GetHexCorner(double cx, double cy, int i)
        {
            int angleDeg = 60 * i - 30;
            double angleRad = Math.PI / 180 * angleDeg;
            return new Point(cx + HexRadius * Math.Cos(angleRad),
                    cy + HexRadius * Math.Sin(angleRad));
        }

        public override void Initialize()
        {
            int index = 0;
            double leftOffset;
            for (int j = 0; j < Shape[1]; j++)
            {
                for (int i = 0; i < Shape[0]; i++)
                {
                    leftOffset = (j % 2 != 0) ? (HexWidth / 2) : 0; // if odd row than offset 3/4 * w
                    double centerX = leftOffset + i * HexWidth + (HexWidth * 0.5) ;
                    double centerY = j * (HexHeight * 0.75) + (HexHeight * 0.5);
                    // hex corners
                    PointCollection points = new PointCollection();
                    for (int c = 0; c < 6; c++)  { points.Add(GetHexCorner(centerX, centerY,  c)); }
                    HexNode node = new HexNode(
                        index,
                        new double[2] { centerX, centerY },
                        new Polygon { Points = points }
                    );
                    Nodes.Add(node);
                    node.Pol.MouseDown += OnMouseDown; // add event handler
                    Canv.Children.Add(node.Pol); // add rect to canvas
                    index += 1;
                }
            }
            ConnectNodes();
        }
        private void ConnectNodes()
        {
            (int, int)[][] shifts = new (int, int)[2][] {
                new (int, int)[6] { (-1, 0), (-1, -1), (0, -1), (1, 0), (0, 1), (-1, 1) },
                new (int, int)[6] { (-1, 0), (0, -1), (1, -1), (1, 0), (1, 1), (0, 1) },
            };
            float weight = 1;
            for (int i = 0; i < Shape[0]; i++)
            {
                for (int j = 0; j < Shape[1]; j++)
                {
                    // is row even ? 0 : 1
                    int shiftParity = (j % 2 == 0) ? 0 : 1;
                    for (int shiftIndex = 0; shiftIndex < 6; shiftIndex++)
                    {
                        int x = i + shifts[shiftParity][shiftIndex].Item1; 
                        int y = j + shifts[shiftParity][shiftIndex].Item2;
                        if (x >= 0 && x < Shape[0] && y >= 0 && y < Shape[1])
                        {
                            Node thisNode = Nodes[CoordsToIndex(i, j)];
                            Node neighbourNode = Nodes[CoordsToIndex(x, y)];
                            thisNode.AddEdge(neighbourNode, weight);
                            neighbourNode.AddEdge(thisNode, weight);
                        }
                    }
                }
            }
        }
        public override int CoordsToIndex(int x, int y)
        {
            return x + (y * Shape[0]);
        }
        public override int ScreenCoordsToIndex(double x, double y)
        {
            return -1;
        }

        protected override void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Polygon pol = sender as Polygon;
            int nodeIndex = (int) pol.Tag;
            Node node = Nodes[nodeIndex];
            SetType(node, MainViewModel.SelectedNodeType);
        }
    }
}
