using System;
using System.Collections.Generic;
using System.Linq;

Console.WriteLine(System.IO.File
	.ReadAllText("input.txt")
	.Split("\n\n", StringSplitOptions.None)
	.Select(x => x
		.Split('\n')
		.Cast<IEnumerable<char>>()
		.Aggregate((x, y) => x.Intersect(y))
		.Count()
	).Sum()
);
