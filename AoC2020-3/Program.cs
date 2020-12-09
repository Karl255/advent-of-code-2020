using System;
using System.Diagnostics;

static class Program
{
	static void Main()
	{
		string[] field = System.IO.File.ReadAllLines("input.txt");
		long totalTrees = 0;

		Stopwatch sw = new();
		sw.Start();

		for (int i = 0; i < 100; i++)
		{
			totalTrees = field.CheckForTrees(1, 1)
				* field.CheckForTrees(3, 1)
				* field.CheckForTrees(5, 1)
				* field.CheckForTrees(7, 1)
				* field.CheckForTrees(1, 2);
		}

		sw.Stop();
		Console.WriteLine(totalTrees);
		Console.WriteLine(sw.Elapsed / 100);
	}

	static long CheckForTrees(this string[] field, int dX, int dY)
	{
		(int x, int y) current = (dX, dY);
		int bottom = field.Length - 1;
		long treeCount = 0;

		while (current.y <= bottom)
		{
			if (field.At(current) == '#')
				treeCount++;

			current.x += dX;
			current.x %= field[0].Length;
			current.y += dY;
		}
		return treeCount;
	}

	static char At(this string[] @this, (int x, int y) pos)
		=> @this[pos.y][pos.x];
}
