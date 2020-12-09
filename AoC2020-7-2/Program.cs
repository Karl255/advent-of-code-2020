using System;
using System.Collections.Generic;
using System.Linq;

string[] input = System.IO.File.ReadAllLines("input.txt");
Dictionary<string, Dictionary<string, int>> bags = new();

foreach (string line in input)
{
	string[] t = line.Split(" bags contain ");
	string key = t[0];
	Dictionary<string, int> value = new();

	foreach (var contained in t[1].Split(", "))
	{
		if (contained == "no other bags.")
			break;

		int firstSpace = contained.IndexOf(' ');
		int lastSpace = contained.LastIndexOf(' ');

		string item = contained[(firstSpace + 1)..lastSpace];
		int amount = int.Parse(contained[0..firstSpace]);

		value.Add(item, amount);
	}

	bags.Add(key, value);
}

/*
foreach ((string bagType, Dictionary<string, int> innerBags) in bags)
{
	Console.WriteLine($"{bagType}:");

	foreach (var innerBag in innerBags)
	{
		Console.WriteLine($"\t{innerBag.Key}: {innerBag.Value}");
	}

}
*/

Console.WriteLine("gold shiny:");
int bagsInShiny = BagsInsideCount(bags, "shiny gold") - 1;

Console.WriteLine(bagsInShiny);


static int BagsInsideCount(Dictionary<string, Dictionary<string, int>> bags, string bagName, string indent = "")
{
	indent += "    ";
	int count = 1;

	foreach ((string name, int amount) in bags[bagName])
	{
		Console.WriteLine($"{indent}{amount} {name}");
		count += amount * BagsInsideCount(bags, name, indent);
	}

	return count;
}
