using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

string[] input = System.IO.File.ReadAllText("input.txt").Split("\n\n");
int valid = 0;

Stopwatch sw = new();
sw.Start();
for (int i = 0; i < 1000; i++)
{

	foreach (string line in input)
	{
		Dictionary<string, string> pp = new();
		string[] requiredFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
		string[] pairs = line.Split(' ', '\n');

		foreach (string pair in pairs)
		{
			string[] t = pair.Split(':');
			pp.Add(t[0], t[1]);
		}

		if (requiredFields.All(f => pp.ContainsKey(f))
			&& ValidateInt(pp["byr"], 1920, 2002)
			&& ValidateInt(pp["iyr"], 2010, 2020)
			&& ValidateInt(pp["eyr"], 2020, 2030)
			&& ValidateHeight(pp["hgt"])
			&& ValidateHexColor(pp["hcl"])
			&& ValidateEyeColor(pp["ecl"])
			&& int.TryParse(pp["pid"], out int _)
			&& pp["pid"].Length == 9)
			valid++;
	}
}

sw.Stop();
Console.WriteLine(valid);
Console.WriteLine(sw.Elapsed / 1000);

static bool ValidateInt(string input, int min, int max)
	=> int.TryParse(input, out int num) && min <= num && num <= max;

static bool ValidateHeight(string input)
{
	if (input != null && int.TryParse(input[0..^2], out int num))
		if (input[^2..^0] == "cm")
			return 150 <= num && num <= 193;
		else
			return 59 <= num && num <= 76;
	return false;
}

static bool ValidateHexColor(string input)
	=> input != null
	&& input[0] == '#'
	&& input[1..^0].All(c => ('0' <= c && c <= '9') || ('a' <= c && c <= 'f'));

static bool ValidateEyeColor(string input)
	=> input != null
	&& new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }
		.Contains(input);
