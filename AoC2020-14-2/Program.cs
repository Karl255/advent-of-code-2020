using System;
using System.Collections.Generic;
using System.Linq;

string[] input = System.IO.File.ReadAllLines("input.txt");

string mask = "";
Dictionary<long, long> memory = new();

for (int i = 0; i < input.Length; i++)
{
	string[] t = input[i].Split(" = ");

	if (t[0][1] == 'a')
		mask = t[1];
	else
	{
		long address = int.Parse(t[0][4..^1]);
		long value = long.Parse(t[1]);

		for (int j = 0; j < 36; j++)
			if (mask[j] == '1')
				address = address | (1L << 35 - j);

		AllCombos(mask.Count(c => c == 'X'), list =>
		{
			int k = 0;
			for (int j = 0; j < 36; j++)
				if (mask[j] == 'X')
				{
					if (list[k] == 1)
						address = address | (1L << 35 - j);
					else
						address = address & ~(1L << 35 - j);
					k++;
				}

			memory[address] = value;
		});
	}
}

Console.WriteLine(memory.Sum(x => x.Value));

void AllCombos(int n, Action<int[]> callback)
{
	int[] list = new int[n];

	void allCombos(int[] list, int i)
	{
		if (i == list.Length)
			callback(list);
		else
		{
			list[i] = 0;
			allCombos(list, i + 1);
			list[i] = 1;
			allCombos(list, i + 1);
		}
	}

	allCombos(list, 0);
}