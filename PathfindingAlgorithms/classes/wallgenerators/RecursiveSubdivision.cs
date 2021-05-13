using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    // Not actually recursive 
    class RecursiveSubdivision : WallGenerator
    {
        private class RecursiveSubdivisionParameter
        {
            public bool IsHorizontal;
            public int ColumnStart, ColumnEnd, RowStart, RowEnd;
            public int ColumnDiff { get { return ColumnEnd - ColumnStart; } }
            public int RowDiff { get { return RowEnd - RowStart; } }

            public RecursiveSubdivisionParameter(bool isHorizontal, int columnStart, int columnEnd, int rowStart, int rowEnd)
            {
                IsHorizontal = isHorizontal;
                ColumnStart = columnStart;
                ColumnEnd = columnEnd;
                RowStart = rowStart;
                RowEnd = rowEnd;
            }
        }

        private Random Random;
        private new GridEnviroment Env;
        // Stack with parameters to replace recursion
        private Stack<RecursiveSubdivisionParameter> Layers;
        private Queue<Node> NodesToChange;
        private int MinGap = 1;
        public RecursiveSubdivision(GridEnviroment env) : base(env)
        {
            Env = env;
            Random = new Random();
            Layers = new Stack<RecursiveSubdivisionParameter>();
            NodesToChange = new Queue<Node>();

            Layers.Push(new RecursiveSubdivisionParameter(true, 0, env.Shape.Item1, 0, env.Shape.Item2));
            Run();
        }
        private bool IsEnoughSpace(RecursiveSubdivisionParameter Par)
        {
            return (Par.RowDiff > MinGap && Par.ColumnDiff > MinGap);
        }
        public override void Step()
        {
            if (NodesToChange.Count > 0)
            {
                Node node = NodesToChange.Dequeue();
                if (node.Type == NodeType.Empty) { node.SetType(NodeType.Wall); }
            }
            else IsFinished = true;
        }

        public void Run()
        {
            while (Layers.Count > 0)
            {
                RecursiveSubdivisionParameter Par = Layers.Pop();
                if (Par.ColumnDiff < Par.RowDiff)
                {
                    int rowSeparator = (int)(Par.RowStart + Par.RowEnd) / 2;
                    int entrance = Random.Next(Par.ColumnStart, Par.ColumnEnd - 1);
                    if (entrance == (int)(Par.ColumnStart + Par.ColumnEnd)/2) { entrance++; } //shift entrance so its not in middle
                    
                    for (int col = Par.ColumnStart; col < Par.ColumnEnd; col++)
                    {
                        if (col == entrance) continue;
                        NodesToChange.Enqueue(Env.Nodes[Env.CoordsToIndex(col, rowSeparator)]);
                    }
                    var first = new RecursiveSubdivisionParameter(false, Par.ColumnStart, Par.ColumnEnd, Par.RowStart, rowSeparator);
                    var second = new RecursiveSubdivisionParameter(false, Par.ColumnStart, Par.ColumnEnd, rowSeparator + 1, Par.RowEnd);
                    if (IsEnoughSpace(first)) { Layers.Push(first); }
                    if (IsEnoughSpace(second)) { Layers.Push(second); }
                }
                else
                {
                    int colSeparator = (int)(Par.ColumnStart + Par.ColumnEnd) / 2;
                    int entrance = Random.Next(Par.RowStart, Par.RowEnd - 1);
                    if (entrance == (int)(Par.ColumnStart + Par.ColumnEnd) / 2) { entrance++; } //shift entrance so its not in middle

                    for (int row = Par.RowStart; row < Par.RowEnd; row++)
                    {
                        if (row == entrance) continue;
                        NodesToChange.Enqueue(Env.Nodes[Env.CoordsToIndex(colSeparator, row)]);
                    }
                    var first = new RecursiveSubdivisionParameter(true, Par.ColumnStart, colSeparator, Par.RowStart, Par.RowEnd);
                    var second = new RecursiveSubdivisionParameter(true, colSeparator + 1, Par.ColumnEnd, Par.RowStart, Par.RowEnd);
                    if (IsEnoughSpace(first)) { Layers.Push(first); }
                    if (IsEnoughSpace(second)) { Layers.Push(second); }
                }
            } 
        }
    }
}
