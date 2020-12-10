using System;
using System.Linq;

var input = System.IO.File
	.ReadAllLines("input.txt")
	.Select(int.Parse);

int[] lines = input
	.OrderBy(x => x)
	.Append(input.Max() + 3)
	.ToArray();

int previous = 0;
int diff1Count = 0;
int diff3Count = 0;

foreach (int num in lines)
{
	long diff = num - previous;

	switch (diff)
	{
		case 1:
			diff1Count++;
			break;
		case 3:
			diff3Count++;
			break;
		default:
			throw new Exception();
	};

	previous = num;
}

Console.WriteLine(diff1Count * diff3Count);
