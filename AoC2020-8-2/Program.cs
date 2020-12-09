using System;
using System.Collections.Generic;
using System.Linq;

(string instruction, int argument)[] code = System.IO.File
	.ReadAllLines("input.txt")
	.Select(x => (
		x.Substring(0, x.IndexOf(' ')),
		int.Parse(x.Substring(x.IndexOf(' ') + 1)))
	).ToArray();

for (int i = 0; i < code.Length; i++)
{
	var newCode = new (string instruction, int argument)[code.Length];
	code.CopyTo(newCode, 0);

	if (newCode[i].instruction == "nop")
		newCode[i].instruction = "jmp";
	else if (newCode[i].instruction == "jmp")
		newCode[i].instruction = "nop";
	else
		continue;

	var result = RunCode(newCode);

	if (result.terminates)
	{
		Console.WriteLine(result.acc);
		break;
	}
}
static (bool terminates, int acc) RunCode((string instruction, int argument)[] code)
{
	int acc = 0;
	HashSet<int> visitedLines = new();

	for (int i = 0; i < code.Length; i++)
	{
		if (visitedLines.Contains(i))
			return (false, acc);

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

	return (true, acc);
}
