using System;
using System.Linq;

string[] input = System.IO.File.ReadAllLines("input.txt");
int startTime = int.Parse(input[0]);
var lines = input[1]
	.Split(',')
	.Where(x => x != "x")
	.Select(int.Parse)
	.Select(x => (waitTime: (int)Math.Ceiling(startTime / (double)x) * x - startTime, id: x));

var minLine = lines.First(x => x.waitTime == lines.Min(x => x.waitTime));

Console.WriteLine(minLine.id * minLine.waitTime);
