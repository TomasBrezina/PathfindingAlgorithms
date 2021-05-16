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
    /// <summary>
    /// Hexagon grid enviroment
    /// Shape: Polygon (Hexagon)
    /// Neighnours: 6
    /// </summary>
    class HexagonGridEnviroment : GridEnviroment
    {
        private double HexRadius; 
        private double HexWidth; // 2 * HexRadius
        private double HexHeight; // Sqrt(3) * HexRadius

        public HexagonGridEnviroment(Canvas canv, (int, int) shape) : base(canv, shape)
        {
            // radius to fully fill width or height of canvas with hexagons

            HexWidth = DefaultCanvWidth / shape.Item1;
            HexRadius = HexWidth / Math.Sqrt(3);
            HexHeight = HexRadius * 2;

            // Set canvas width and height to best fit the env shape
            canv.Width = HexWidth * shape.Item1 + (HexWidth / 2);


            canv.Height = HexHeight * 0.75 * shape.Item2 + (HexHeight*0.25);
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
            for (int j = 0; j < Shape.Item2; j++)
            {
                for (int i = 0; i < Shape.Item1; i++)
                {
                    leftOffset = (j % 2 != 0) ? (HexWidth / 2) : 0; // if odd row than offset 3/4 * w
                    double centerX = leftOffset + i * HexWidth + (HexWidth * 0.5) ;
                    double centerY = j * (HexHeight * 0.75) + (HexHeight * 0.5);
                    // hex corners
                    PointCollection points = new PointCollection();
                    for (int c = 0; c < 6; c++)  { points.Add(GetHexCorner(centerX, centerY,  c)); }
                    Node node = new Node(
                        index,
                        (i, j),
                        new Vector(centerX, centerY),
                        new Polygon { Points = points }
                    );
                    Nodes.Add(node);
                    node.Shape.MouseDown += OnMouseDown; // add event handler
                    Canv.Children.Add(node.Shape); // add rect to canvas
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
            for (int i = 0; i < Shape.Item1; i++)
            {
                for (int j = 0; j < Shape.Item2; j++)
                {
                    // is row even ? 0 : 1
                    int shiftParity = (j % 2 == 0) ? 0 : 1;
                    for (int shiftIndex = 0; shiftIndex < 6; shiftIndex++)
                    {
                        int x = i + shifts[shiftParity][shiftIndex].Item1; 
                        int y = j + shifts[shiftParity][shiftIndex].Item2;
                        if (x >= 0 && x < Shape.Item1 && y >= 0 && y < Shape.Item2)
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
        private (int, int) cubeToPos((int, int, int) cube)
        {
            int col = cube.Item1 + (cube.Item3 - (cube.Item3 & 1)) / 2;
            int row = cube.Item3;
            return (col, row);
        }
        private (int, int, int) posToCube((int, int) pos)
        {
            int x = pos.Item1 - (pos.Item2 - (pos.Item2 & 1)) / 2;
            int z = pos.Item2;
            int y = -x - z;
            return (x, y, z);
        }
        // Manhattan heuristic distance for hexagonal grid with cube coordinates
        public override float HeuristicDist(Node start, Node end)
        {
            var a = posToCube(start.Pos);
            var b = posToCube(end.Pos);
            return (Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2) + Math.Abs(a.Item3 - b.Item3)) / 2;
        }
        public override int CoordsToIndex(int x, int y)
        {
            return x + (y * Shape.Item1);
        }
        public override (int, int) IndexToCoords(int i)
        {
            int x = i % Shape.Item1;
            int y = i / Shape.Item1;
            return (x, y);
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
