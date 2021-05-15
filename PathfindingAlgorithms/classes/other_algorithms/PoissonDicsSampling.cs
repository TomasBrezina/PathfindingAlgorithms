using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PathfindingAlgorithms
{
    public static class PoissonDicsSampling
    {
        public static List<Vector> Generate(double radius, Vector sampleRegionSize, int numSamplesBeforeRejection = 30)
        {
			double cellSize = radius / Math.Sqrt(2);
			Random random = new Random();
			int[,] grid = new int[(int)Math.Ceiling(sampleRegionSize.X / cellSize), (int)Math.Ceiling(sampleRegionSize.Y / cellSize)];
			List<Vector> points = new List<Vector>();
			List<Vector> spawnPoints = new List<Vector>();

			spawnPoints.Add(sampleRegionSize / 2);
			while (spawnPoints.Count > 0)
			{
				
				int spawnIndex = random.Next(0, spawnPoints.Count);
				Vector spawnCentre = spawnPoints[spawnIndex];
				bool candidateAccepted = false;

				for (int i = 0; i < numSamplesBeforeRejection; i++)
				{
					double angle = random.NextDouble() * Math.PI * 2;
					Vector dir = new Vector(Math.Sin(angle), Math.Cos(angle));
					Vector candidate = spawnCentre + dir * GetRandomDouble(radius, 2 * radius);
					if (IsValid(candidate, sampleRegionSize, cellSize, radius, points, grid))
					{
						points.Add(candidate);
						spawnPoints.Add(candidate);
						grid[(int)(candidate.X / cellSize), (int)(candidate.Y / cellSize)] = points.Count;
						candidateAccepted = true;
						break;
					}
				}
				if (!candidateAccepted)
				{
					spawnPoints.RemoveAt(spawnIndex);
				}

			}

			return points;
		}

		static bool IsValid(Vector candidate, Vector sampleRegionSize, double cellSize, double radius, List<Vector> points, int[,] grid)
		{
			if (candidate.X >= 0 && candidate.X < sampleRegionSize.X && candidate.Y >= 0 && candidate.Y < sampleRegionSize.Y)
			{
				int cellX = (int)(candidate.X / cellSize);
				int cellY = (int)(candidate.Y / cellSize);
				int searchStartX = Math.Max(0, cellX - 2);
				int searchEndX = Math.Min(cellX + 2, grid.GetLength(0) - 1);
				int searchStartY = Math.Max(0, cellY - 2);
				int searchEndY = Math.Min(cellY + 2, grid.GetLength(1) - 1);

				for (int x = searchStartX; x <= searchEndX; x++)
				{
					for (int y = searchStartY; y <= searchEndY; y++)
					{
						int pointIndex = grid[x, y] - 1;
						if (pointIndex != -1)
						{
							double sqrDst = (candidate - points[pointIndex]).LengthSquared;
							if (sqrDst < radius * radius)
							{
								return false;
							}
						}
					}
				}
				return true;
			}
			return false;
		}

		private static double GetRandomDouble(double minimum, double maximum)
		{
			Random random = new Random();
			return random.NextDouble() * (maximum - minimum) + minimum;
		}
	}
}
