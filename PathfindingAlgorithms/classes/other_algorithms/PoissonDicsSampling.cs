using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PathfindingAlgorithms
{
	/*
	Modified version of Poisson-Disc-Sampling from Sebastian Lague under MIT Licence
	https://github.com/SebLague/Poisson-Disc-Sampling/blob/master/LICENSE
	 
	MIT License

	Copyright (c) 2020 Sebastian Lague

	Permission is hereby granted, free of charge, to any person obtaining a copy
	of this software and associated documentation files (the "Software"), to deal
	in the Software without restriction, including without limitation the rights
	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the Software is
	furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in all
	copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
	SOFTWARE.
	*/
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
