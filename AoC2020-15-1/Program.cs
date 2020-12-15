using System;
using System.Collections.Generic;
using System.Linq;

List<int> numbers = System.IO.File.ReadAllText("input.txt")
	.Split(',')
	.Select(int.Parse)
	.ToList();

numbers.Capacity = 2020;

for (int current = numbers.Count - 1; current < 2020 - 1; current++)
{
	if (numbers.Count(n => n == numbers[current]) == 1)
		numbers.Add(0);
	else
	{
		int indexOfLast = numbers.LastIndexOf(numbers[current], numbers.Count - 2);
		numbers.Add(current - indexOfLast);
	}
}

Console.WriteLine(numbers.Last());
