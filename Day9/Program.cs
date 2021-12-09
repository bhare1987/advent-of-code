using System;

Console.WriteLine("Day 9 - Part 1");
var lines = from line in File.ReadAllLines("input.txt") select line.Trim();
var convertedInput = lines.Select(l => l.ToCharArray()).Select(p => p.Select(x => int.Parse(x.ToString())).ToArray()).ToArray();

var lowPoints = new Dictionary<string, int>();

for (int i = 0; i < convertedInput.Count(); i++)
{
    var end = convertedInput.Count() - 1;
    var row = convertedInput[i];
    for (int j = 0; j < row.Count(); j++)
    {
        var val = row[j];
        var pointsToCheck = new List<int>() {  val };

        if (i != 0)
        {
            // Top
            pointsToCheck.Add(convertedInput[i - 1][j]);
        }

        if (j != 0)
        {
            // Left
            pointsToCheck.Add(convertedInput[i][j - 1]);
        }

        if (i != end)
        {
            // Bottom
            pointsToCheck.Add(convertedInput[i + 1][j]);
        }

        if (j != end)
        {
            // Right
            pointsToCheck.Add(convertedInput[i][j + 1]);
        }

        if (pointsToCheck.Min() == val && !pointsToCheck.All(x => val == x))
        {
            lowPoints.Add($"{i},{j}", val + 1);
        }
    }
}

var result = lowPoints.Values.Sum();

Console.WriteLine($"Day 9 - Part 1 Result: {result}");