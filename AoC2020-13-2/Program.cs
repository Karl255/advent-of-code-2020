using System;
using System.Collections.Immutable;
using System.Linq;

string[] input = System.IO.File.ReadAllLines("input.txt");
var lines = input[1]
	.Split(',')
	.Select((id, index) => (id: id, offset: index))
	.Where(line => line.id != "x")
	.Select(line => new BusLine(long.Parse(line.id), (long)line.offset))
	.ToImmutableArray();

long time = lines[0].Id;
long period = lines[0].Id;

foreach (var line in lines.Skip(1))
{
	while ((time + line.Offset) % line.Id != 0)
		time += period;

	period *= line.Id;
}

Console.WriteLine(time);

record BusLine(long Id, long Offset);
