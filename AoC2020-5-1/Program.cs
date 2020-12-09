using System;
using System.Collections.Generic;
using System.Linq;

string[] input = System.IO.File.ReadAllLines("input.txt");
HashSet<int> idSet = input.Select(x => Convert.ToInt32(x.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2)).ToHashSet();
Console.WriteLine(idSet.Max()); // part 1
Console.WriteLine(idSet.OrderBy(x => x).SkipWhile(x => idSet.Contains(x + 1)).First() + 1); // part 2

/*
for (int id = 0; id < 1024; id++)
	if (!idSet.Contains(id) && idSet.Contains(id - 1) && idSet.Contains(id + 1))
		Console.WriteLine(id);
*/
