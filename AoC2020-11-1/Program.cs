using System;
using System.Linq;

string[] input = System.IO.File.ReadAllLines("input.txt");
char[,] grid = new char[input[0].Length, input.Length];

for (int y = 0; y < input.Length; y++)
	for (int x = 0; x < input[y].Length; x++)
		grid[x, y] = input[y][x];

bool anythingchanged = false;

do
{
	/*
	for (int iy = 0; iy < grid.GetLength(1); iy++)
	{
		for (int ix = 0; ix < grid.GetLength(0); ix++)
			Console.Write(grid[ix, iy]);
		Console.WriteLine();
	}
	Console.WriteLine();
	//Console.ReadLine();
	*/

	anythingchanged = false;
	char[,] newGrid = grid.Clone() as char[,];

	for (int iy = 0; iy < grid.GetLength(1); iy++)
		for (int ix = 0; ix < grid.GetLength(0); ix++)
			if (grid[ix, iy] == 'L' && CountAround(grid, ix, iy) == 0)
			{
				newGrid[ix, iy] = '#';
				anythingchanged = true;
			}
			else if (grid[ix, iy] == '#' && CountAround(grid, ix, iy) >= 4)
			{
				newGrid[ix, iy] = 'L';
				anythingchanged = true;
			}

	grid = newGrid;
}
while (anythingchanged);

Console.WriteLine(grid.Cast<char>().Count(c => c == '#'));

int CountAround(char[,] grid, int sx, int sy)
{
	int count = 0;
	int xStart = Math.Max(sx - 1, 0);
	int xEnd = Math.Min(sx + 1, grid.GetLength(0));
	int yStart = Math.Max(sy - 1, 0);
	int yEnd = Math.Min(sy + 1, grid.GetLength(1));

	for (int ix = xStart; ix < xEnd; ix++)
		for (int iy = yStart; iy < yEnd; iy++)
			if (grid[ix, iy] == '#' && (ix != sx || iy != sy))
				count++;

	return count;
}
