using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

string[] input = System.IO.File.ReadAllText("input.txt").Split("\n\n");
string[] requiredFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
int valid = 0;

Stopwatch sw = new();
sw.Start();

for (int i = 0; i < 1000; i++)
{
	foreach (string line in input)
	{
		List<string> pp = new();
		string[] pairs = line.Split(' ', '\n');

		foreach (string pair in pairs)
		{
			pp.Add(pair[0..pair.IndexOf(':')]);
		}

		if (requiredFields.All(f => pp.Contains(f)))
			valid++;
	}
}

sw.Stop();
Console.WriteLine(valid);
Console.WriteLine(sw.Elapsed / 1000);
