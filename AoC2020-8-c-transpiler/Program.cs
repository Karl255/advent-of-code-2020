using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

(string instruction, int argument)[] code = System.IO.File
	.ReadAllLines("input.txt")
	.Select(x => (
		x.Substring(0, x.IndexOf(' ')),
		int.Parse(x.Substring(x.IndexOf(' ') + 1)))
	).ToArray();

HashSet<int> lineLabels = code
	.Select((x, i) => (line: i, instruction: x.instruction, argument: x.argument))
	.Where(x => x.instruction == "jmp")
	.Select(x => x.line + x.argument)
	.ToHashSet();

StringBuilder sb = new();

sb.Append(@"#include <stdio.h>

int main() {
	int acc = 0;
");

string endCode = @"
	printf(""%d\n"", acc);
	return 0;
}
";

int i;
for (i = 0; i < code.Length; i++)
{
	if (lineLabels.Contains(i))
		sb.Append($"_{i}:\n");

	sb.Append(code[i].instruction switch
	{
		"acc" => $"\tacc += {code[i].argument};\n",
		"jmp" => $"\tgoto _{i + code[i].argument};\n",
		"nop" => $"\t// NOP {code[i].argument}\n",
		_ => ""
	});
}

foreach (int extraLabel in lineLabels.Where(x => x >= i).OrderBy(x => x))
	sb.Append($"_{extraLabel}:\n");

sb.Append(endCode);
System.IO.File.WriteAllText("output.c", sb.ToString());
