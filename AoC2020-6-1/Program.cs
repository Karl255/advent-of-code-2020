using System;
using System.Linq;

Console.WriteLine(System.IO.File
	.ReadAllText("input.txt")
	.Split("\n\n", StringSplitOptions.None)
	.Select(x => x
		.Where(x => char.IsLetter(x))
		.Distinct()
		.Count()
	).Sum()
);
