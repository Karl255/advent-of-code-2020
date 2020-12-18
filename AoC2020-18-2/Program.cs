using System;
using System.Collections.Generic;
using System.Linq;

long result = System.IO.File.ReadAllLines("input.txt")
	.Select(e => new Node(e.Replace(" ", "")).Calculate())
	.Sum();

Console.WriteLine(result);

internal struct Node
{
	private readonly NodeType Type;
	private readonly long Number;
	private readonly List<Node> Children;

	public Node(string self)
	{
		if (HasStripParenthesisToStrip(self))
			self = self[1..^1];
		Type = FindType(self);

		(Number, Children) = ((long, List<Node>))(Type switch
		{
			NodeType.Multiplication =>
				(0,
				self
					.ParenthesisAwareSplit('*')
					.Select(x => new Node(x))
					.ToList()),

			NodeType.Addition =>
				(0,
				self
					.ParenthesisAwareSplit('+')
					.Select(x => new Node(x))
					.ToList()),

			NodeType.Number =>
				(long.Parse(self), null),

			_ => throw new Exception()
		});
	}

	public long Calculate() => Type switch
	{
		NodeType.Number => Number,

		NodeType.Addition => Children
			.Select(x => x.Calculate())
			.Sum(),

		NodeType.Multiplication => Children
			.Select(x => x.Calculate())
			.Aggregate((x, y) => x * y),

		_ => throw new Exception()
	};

	private static NodeType FindType(string self)
	{
		int parenthesisLevel = 0;
		bool hasAddition = false;

		for (int i = 0; i < self.Length; i++)
		{
			if (self[i] == '(')
				parenthesisLevel++;
			else if (self[i] == ')')
				parenthesisLevel--;

			if (parenthesisLevel == 0)
			{
				if (self[i] == '*')
					return NodeType.Multiplication;
				else if (self[i] == '+')
					hasAddition = true;
			}
		}

		return hasAddition ? NodeType.Addition : NodeType.Number;
	}

	private static bool HasStripParenthesisToStrip(string expression)
	{
		if (expression[0] == '(' && expression[^1] == ')')
		{
			int parenthesisLevel = 1;

			for (int i = 1; i < expression.Length - 1; i++)
			{
				if (expression[i] == '(')
					parenthesisLevel++;
				else if (expression[i] == ')')
					parenthesisLevel--;

				if (parenthesisLevel == 0)
					return false;
			}

			return true;
		}

		return false;
	}
}

internal enum NodeType
{
	Number,
	Addition,
	Multiplication
}

public static class Extensions
{
	public static string[] ParenthesisAwareSplit(this string @this, char separator)
	{
		List<string> splits = new();
		int parenthesisLevel = 0;
		int startIndex = 0;

		for (int i = 0; i < @this.Length; i++)
		{
			if (@this[i] == '(')
				parenthesisLevel++;
			else if (@this[i] == ')')
				parenthesisLevel--;

			if (parenthesisLevel == 0 && @this[i] == separator)
			{
				splits.Add(@this[startIndex..i]);
				startIndex = i + 1;
			}
		}

		splits.Add(@this[startIndex..]);
		return splits.ToArray();
	}
}
