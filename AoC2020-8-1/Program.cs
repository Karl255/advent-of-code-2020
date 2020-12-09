using System;
using System.Collections.Generic;
using System.Linq;

(string instruction, int argument)[] code = System.IO.File
	.ReadAllLines("input.txt")
	.Select(x => (
		x.Substring(0, x.IndexOf(' ')),
		int.Parse(x.Substring(x.IndexOf(' ') + 1)))
	).ToArray();

int acc = 0;
HashSet<int> visitedLines = new();

for (int i = 0; i < code.Length; i++)
{
	if (visitedLines.Contains(i))
		break;

	visitedLines.Add(i);

	switch (code[i].instruction)
	{
		case "acc":
			acc += code[i].argument;
			break;
		case "jmp":
			i += code[i].argument - 1;
			break;
		case "nop":
			break;
	}
}

Console.WriteLine(acc);
