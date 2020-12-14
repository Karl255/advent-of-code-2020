using System;
using System.Collections.Generic;
using System.Linq;

string[] input = System.IO.File.ReadAllLines("input.txt");

long bitMask = ((long)1 << 36) - 1;
long maskValue = 0;
Dictionary<int, long> memory = new();

for (int i = 0; i < input.Length; i++)
{
	string[] t = input[i].Split(" = ");

	if (input[i][1] == 'a')
	{
		string maskStrValue = t[1];

		bitMask = Convert.ToInt64(maskStrValue
			.Replace('1', '0')
			.Replace('X', '1'),
			2);

		maskValue = Convert.ToInt64(maskStrValue.Replace('X', '0'), 2);
	}
	else
	{
		int address = int.Parse(t[0][4..^1]);
		long value = long.Parse(t[1]);

		memory[address] = value & bitMask | maskValue;
	}
}

Console.WriteLine(memory.Sum(x => x.Value));
