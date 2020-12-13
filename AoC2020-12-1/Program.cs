using System;
using System.Linq;

var input = System.IO.File.ReadAllLines("input.txt")
	.Select(x => (instruction: x[0], value: int.Parse(x[1..])))
	.ToArray();

(int x, int y) pos = (0, 0);
int rotation = 0;

foreach (var line in input)
{
	switch (line.instruction)
	{
		case 'N':
			pos.y += line.value;
			break;
		case 'S':
			pos.y -= line.value;
			break;
		case 'E':
			pos.x += line.value;
			break;
		case 'W':
			pos.x -= line.value;
			break;
		case 'L':
			rotation++;
			rotation %= 4;
			break;
		case 'R':
			rotation += 3;
			rotation %= 4;
			break;
		case 'F':
			_ = rotation switch
			{
				0 => pos.x += line.value,
				1 => pos.y += line.value,
				2 => pos.x -= line.value,
				3 => pos.y -= line.value,
				_ => throw new Exception()
			};
			break;
		default:
			break;
	};
}

Console.WriteLine(Math.Abs(pos.x) + Math.Abs(pos.y));
