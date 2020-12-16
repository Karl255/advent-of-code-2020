using System;
using System.Collections.Generic;
using System.Linq;

string input = System.IO.File.ReadAllText("input.txt");
string[][] inputGroups = input.Split("\n\n").Select(group => group.Split('\n')).ToArray();

HashSet<int>[] fieldValidation = inputGroups[0]
	.Select(line => line
		.Split(':')
		[1]
		.Split("or")
		.SelectMany(g =>
		{
			int[] t = g.Split('-').Select(int.Parse).ToArray();
			return Enumerable.Range(t[0], t[1] - t[0] + 1);
		}).ToHashSet()
	).ToArray();

long[] myTicket = inputGroups[1][1]
	.Split(',')
	.Select(long.Parse)
	.ToArray();

int[][] nearbyTickets = inputGroups[2]
	[1..]
	.Select(line => line
		.Split(',')
		.Select(int.Parse)
		.ToArray()
	).Where(ticket => ticket
		.All(ticketField => fieldValidation
			.Any(validtionField => validtionField.Contains(ticketField))
		)
	).ToArray();

int fieldCount = fieldValidation.Length;

HashSet<int>[] possibleFieldIndecies = Enumerable
	.Range(0, fieldCount)
	.Select(i => Enumerable.Range(0, fieldCount).ToHashSet())
	.ToArray();

for (int i = 0; i < fieldCount; i++)
	for (int j = 0; j < nearbyTickets.Length; j++)
		for (int k = 0; k < fieldCount; k++)
			if (!fieldValidation[k].Contains(nearbyTickets[j][i]))
				possibleFieldIndecies[i].Remove(k);

bool[] solvedFields = new bool[fieldCount];

while (possibleFieldIndecies.Any(x => x.Count > 1))
{
	for (int i = 0; i < fieldCount; i++)
	{
		if (solvedFields[i])
			continue;

		if (possibleFieldIndecies[i].Count == 1)
		{
			solvedFields[i] = true;
			int usedUpField = possibleFieldIndecies[i].First();

			for (int j = 0; j < possibleFieldIndecies.Length; j++)
				if (i != j)
					possibleFieldIndecies[j].Remove(usedUpField);

			break;
		}
	}
}

long result = 1;

for (int i = 0; i < possibleFieldIndecies.Length; i++)
{
	if (possibleFieldIndecies[i].First() < 6)
		result *= myTicket[i];
}

Console.WriteLine(result);
