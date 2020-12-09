using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

long[] input = System.IO.File.ReadAllLines("input.txt").Select(long.Parse).ToArray();
long t = 0;
Stopwatch sw = new();
sw.Start();

for (int _ = 0; _ < 100; _++)
{
	int preambleLength = 25;
	long target = 0;

	for (int i = preambleLength; i < input.Length; i++)
	{
		if (!CanBeSum(input[i], input.Skip(i - preambleLength).Take(preambleLength).ToArray()))
		{
			target = input[i];
			break;
		}
	}

	t = FindTargetSum(target, input.Where(x => x < target).ToArray());
}

sw.Stop();
Console.WriteLine(t);
Console.WriteLine(sw.Elapsed / 100);

static bool CanBeSum(long num, long[] sequence)
{
	for (int i = 0; i < sequence.Length - 1; i++)
		for (int j = i + 1; j < sequence.Length; j++)
			if (sequence[i] + sequence[j] == num)
				return true;

	return false;
}

static long FindTargetSum(long target, long[] input)
{
	for (int length = 2; length <= input.Length - 1; length++)
	{
		for (int start = 0; start < input.Length - length; start++)
		{
			var set = input.Skip(start).Take(length);

			if (set.Sum() == target)
				return set.Min() + set.Max();
		}
	}

	return -1;
}
