using System;
using System.Linq;

var input = System.IO.File.ReadAllLines("input.txt")
	.Select(x => (instruction: x[0], value: int.Parse(x[1..])))
	.ToArray();

(int x, int y) pos = (0, 0);
(int x, int y) waypoint = (10, 1);

foreach (var line in input)
{
	switch (line.instruction)
	{
		case 'N':
			waypoint.y += line.value;
			break;
		case 'S':
			waypoint.y -= line.value;
			break;
		case 'E':
			waypoint.x += line.value;
			break;
		case 'W':
			waypoint.x -= line.value;
			break;
		case 'L':
			for (int i = 0; i < line.value / 90; i++)
				waypoint = (-waypoint.y, waypoint.x);
			break;
		case 'R':
			for (int i = 0; i < line.value / 90; i++)
				waypoint = (waypoint.y, -waypoint.x);
			break;
		case 'F':
			pos.x += waypoint.x * line.value;
			pos.y += waypoint.y * line.value;
			break;
		default:
			break;
	};
}

Console.WriteLine(Math.Abs(pos.x) + Math.Abs(pos.y));
