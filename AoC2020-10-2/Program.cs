using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

var input = System.IO.File.ReadAllLines("input.txt").Select(int.Parse);

Stopwatch sw = new();
sw.Start();

int[] lines = input
	.OrderBy(x => x)
	.Prepend(0)
	.Append(input.Max() + 3)
	.ToArray();

Dictionary<int, long> cache = new();

long result = Arrangements(lines) + 1;
sw.Stop();

Console.WriteLine(result);
Console.WriteLine(sw.Elapsed);

long Arrangements(int[] input, int startIndex = 0)
{
	long count = 0;

	for (int i = startIndex; i < input.Length - 3; i++)
	{
		// detect if there is a branching in the tree
		if (input[i + 2] - input[i] <= 3)
		{
			if (cache.ContainsKey(input[i]))
				count += cache[input[i]];
			else
			{
				count += Arrangements(input, i + 1);
				count += 1 + Arrangements(input, i + 2);

				// if tripple branch, add the 3rd branch
				if (input[i + 3] - input[i] <= 3)
					count += 1 + Arrangements(input, i + 3);

				cache.Add(input[i], count);
			}

			break;
		}
	}

	return count;
}
