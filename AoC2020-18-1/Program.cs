using System;
using System.Linq;

long result = System.IO.File.ReadAllLines("input.txt")
	.Select(e => ParseExpression(e.Replace(" ", "")))
	.Sum();

Console.WriteLine(result);

long ParseExpression(string expression)
{
	long value = 0;
	int startIndex = 0;
	Operator lastOperator = Operator.None;
	int parenthesisLevel = 0;

	for (int i = 0; i < expression.Length; i++)
	{
		char c =  expression[i];

		if (c == '(')
			parenthesisLevel++;
		else if (c == ')')
			parenthesisLevel--;
		else if (parenthesisLevel == 0 && (c == '+' || c == '*'))
		{
			value = parsePartial(value, expression, startIndex, i, lastOperator);

			startIndex = i + 1;
			lastOperator = c == '+' ? Operator.Addition : Operator.Multiplication;
		}
	}

	value = parsePartial(value, expression, startIndex, expression.Length, lastOperator);

	return value;

	long parsePartial(long initialValue, string expression, int startIndex, int currentIndex, Operator lastOperator)
	{
		long num = expression[currentIndex - 1] == ')'
		? ParseExpression(expression[(startIndex + 1)..(currentIndex - 1)])
		: long.Parse(expression[startIndex..currentIndex]);

		return lastOperator switch
		{
			Operator.None => num,
			Operator.Addition => initialValue + num,
			Operator.Multiplication => initialValue * num,
			_ => throw new Exception()
		};
	}
}

internal enum Operator
{
	None,
	Addition,
	Multiplication
}
