using System;
using System.Collections.Generic;
using System.Linq;

int[] startNumbers = System.IO.File.ReadAllText("input.txt")
	.Split(',')
	.Select(int.Parse)
	.ToArray();

Dictionary<int, int> numbersLastTurn = startNumbers[0..^1]
	.Select((x, i) => (key: x, value: i + 1))
	.ToDictionary(x => x.key, x => x.value);

int nextNumber = startNumbers[^1];

for (int i = numbersLastTurn.Count + 1; i < 30000000; i++)
{
	if (numbersLastTurn.ContainsKey(nextNumber))
	{
		int t = numbersLastTurn[nextNumber];
		numbersLastTurn[nextNumber] = i;
		nextNumber = i - t;
	}
	else
	{
		numbersLastTurn.Add(nextNumber, i);
		nextNumber = 0;
	}
}

Console.WriteLine(nextNumber);
