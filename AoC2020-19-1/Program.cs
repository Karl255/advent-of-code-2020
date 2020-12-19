using System;
using System.Collections.Generic;
using System.Linq;

string input = System.IO.File.ReadAllText("input.txt");
int blankLineIndex = input.IndexOf("\n\n");

Dictionary<int, object> rules = input
	.Substring(0, blankLineIndex)
	.Split('\n')
	.ToDictionary(
		r => int.Parse(r[..r.IndexOf(':')]),
		r =>
		{
			string t = r[(r.IndexOf(':') + 1)..];
			return t[1] == '"'
				? t[2] as object
				: t
					.Split('|')
					.Select(group => group
						.Split(' ', StringSplitOptions.RemoveEmptyEntries)
						.Select(int.Parse)
						.ToArray()
					).ToArray()
					as object;
		}
	).OrderBy(r => r.Key)
	.ToDictionary(r => r.Key, r => r.Value);

HashSet<string> messages = input
	.Substring(blankLineIndex + 2)
	.Split('\n')
	.ToHashSet();

int count = 0;

foreach (string message in messages)
{
	var result = TestOnRule(message, 0);
	if (result.success && result.nextIndex == message.Length)
		count++;
}

Console.WriteLine(count);

(bool success, int nextIndex) TestOnRule(string text, int ruleIndex, int startIndex = 0)
{
	if (rules[ruleIndex] is char c)
		if (text[startIndex] == c)
			return (true, startIndex + 1);
		else
			return (false, -1);
	else if (rules[ruleIndex] is int[][] r)
	{
		foreach (int[] group in r)
		{
			var result = TestGroup(text, group, startIndex);
			int nextIndex = result.nextIndex;

			if (result.success)
				return (true, nextIndex);
		}

		return (false, -1);
	}

	return (false, -1);

	(bool success, int nextIndex) TestGroup(string text, int[] group, int startIndex)
	{
		foreach (int rule in group)
		{
			var result = TestOnRule(text, rule, startIndex);
			startIndex = result.nextIndex;

			if (!result.success)
				return (false, -1);
		}

		return (true, startIndex);
	}
}
