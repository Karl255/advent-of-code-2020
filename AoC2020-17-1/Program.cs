using System;
using System.Linq;

string[] input = System.IO.File.ReadAllLines("input.txt");

bool[,,] space = new bool[input[0].Length + 12, input.Length + 12, 13];

(int x, int y, int z) endPos = (input[0].Length + 12, input.Length + 12, 13);

for (int y = 0; y < input.Length; y++)
	for (int x = 0; x < input[y].Length; x++)
		space[x + 6, y + 6, 6] = input[y][x] == '#';

for (int cycle = 0; cycle < 6; cycle++)
{
	bool[,,] spaceCopy = (bool[,,])space.Clone();

	for (int z = 0; z < endPos.z; z++)
		for (int y = 0; y < endPos.y; y++)
			for (int x = 0; x < endPos.x; x++)
			{
				int count = CountAround(x, y, z);

				if (space[x, y, z])
				{
					if (count != 2 && count != 3)
						spaceCopy[x, y, z] = false;
				}
				else
				{
					if (count == 3)
						spaceCopy[x, y, z] = true;
				}
			}

	space = spaceCopy;
}

Console.WriteLine(space.Cast<bool>().Count(x => x));

int CountAround(int sx, int sy, int sz)
{
	int count = 0;
	int xStart = Math.Max(sx - 1, 0);
	int xEnd =   Math.Min(sx + 1, endPos.x - 1);
	int yStart = Math.Max(sy - 1, 0);
	int yEnd =   Math.Min(sy + 1, endPos.y - 1);
	int zStart = Math.Max(sz - 1, 0);
	int zEnd =   Math.Min(sz + 1, endPos.z - 1);

	for (int iz = zStart; iz <= zEnd; iz++)
		for (int iy = yStart; iy <= yEnd; iy++)
			for (int ix = xStart; ix <= xEnd; ix++)
				if (space[ix, iy, iz] && (ix != sx || iy != sy || iz != sz))
					count++;

	return count;
}
