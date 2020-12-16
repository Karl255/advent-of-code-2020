using System;
using System.Collections.Generic;
using System.Linq;

string input = System.IO.File.ReadAllText("input.txt");
string[][] inputGroups = input.Split("\n\n").Select(group => group.Split('\n')).ToArray();

HashSet<int>[] fieldsAllowedValues = inputGroups[0]
	.Select(line => line
		.Split(':')
		[1]
		.Split("or")
		.SelectMany(g =>
		{
			int[] t = g.Split('-').Select(int.Parse).ToArray();
			return Enumerable.Range(t[0], t[1] - t[0] + 1);
		}).ToHashSet()
	).ToArray();

int errorRate = 0;

for (int i = 1; i < inputGroups[2].Length; i++)
{
	int[] values = inputGroups[2][i].Split(',').Select(int.Parse).ToArray();
	errorRate += values.Where(x => !fieldsAllowedValues.Any(field => field.Contains(x))).Sum();
}

Console.WriteLine(errorRate);
