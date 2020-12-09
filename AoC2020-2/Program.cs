using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

string[] input = File.ReadAllLines("input.txt");
var sw = new Stopwatch();
sw.Start();
int c = 0;

for (int i = 0; i < 100; i++)
	c = input.Count(line => IsValid(ParseLine(line)));

sw.Stop();
Console.WriteLine(c);
Console.WriteLine(sw.Elapsed / 100);

// Parses "x-y c: pass" into (pass, c, x, y)
static (string pass, char c, int min, int max) ParseLine(string line)
{
	string[] t = line.Split(' ');
	int[] nums = t[0].Split('-').Select(int.Parse).ToArray();

	return (t[2], t[1][0], nums[0], nums[1]);
}

static bool IsValid((string pass, char c, int min, int max) t)
	=> (t.pass[t.min - 1] == t.c) != (t.pass[t.max - 1] == t.c);
