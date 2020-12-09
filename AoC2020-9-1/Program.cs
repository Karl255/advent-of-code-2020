using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

long[] input = System.IO.File.ReadAllLines("input.txt").Select(long.Parse).ToArray();
Stopwatch sw = new();
sw.Start();
long target = 0;

for (int _ = 0; _ < 100; _++)
{
	int preambleLength = 25;

	for (int i = preambleLength; i < input.Length; i++)
	{
		if (!CanBeSum(input[i], input.Skip(i - preambleLength).Take(preambleLength).ToArray()))
		{
			target = input[i];
			break;
		}
	}
}

sw.Stop();
Console.WriteLine(target);
Console.WriteLine(sw.Elapsed / 100);

static bool CanBeSum(long num, long[] sequence)
{
	for (int i = 0; i < sequence.Length - 1; i++)
		for (int j = i + 1; j < sequence.Length; j++)
			if (sequence[i] + sequence[j] == num)
				return true;

	return false;
}
