using System;

Console.WriteLine("Day 5 - Part 1");
var lines = from line in File.ReadAllLines("input.txt") where !string.IsNullOrEmpty(line) select line.Trim();
var convertedInput = lines.Select(line => line.Split(" -> ")).Select(coords => coords.Select(coord => coord.Split(',').Select(c => int.Parse(c)).ToArray()).ToArray()).ToArray();

var coordsWithoutDiagonals = convertedInput.Where(c => c[0][0] == c[1][0] || c[0][1] == c[1][1]).ToArray();
var diagonalCoords = convertedInput.Where(c => c[0][0] != c[1][0] && c[0][1] != c[1][1]).ToArray();

var hitDict = new Dictionary<string, int>();

foreach (var coord in coordsWithoutDiagonals)
{
    var isHorizontal = coord[0][1] == coord[1][1];
    string[] range;

    if (isHorizontal)
    {
        var y = coord[0][1];
        var start = coord[0][0] > coord[1][0] ? coord[1][0] : coord[0][0];
        var count = Math.Abs(coord[0][0] - coord[1][0]) + 1;
        range = Enumerable.Range(start, count).Select(r => $"{r},{y}").ToArray();
    }
    else
    {
        var x = coord[0][0];
        var start = coord[0][1] > coord[1][1] ? coord[1][1] : coord[0][1];
        var count = Math.Abs(coord[0][1] - coord[1][1]) + 1;
        range = Enumerable.Range(start, count).Select(r => $"{x},{r}").ToArray();

    }

    foreach (var point in range)
    {
        if (!hitDict.TryGetValue(point, out int val))
        {
            hitDict.Add(point, 1);
        }
        else
        {
            hitDict[point] = hitDict[point] + 1;
        };
    }
}

var result = hitDict.Sum(point => point.Value > 1 ? 1 : 0);

Console.WriteLine($"Day 5 - Part 1 Result: {result}");
Console.WriteLine($"Day 5 - Part 2");


foreach (var coord in diagonalCoords)
{ 
    string[] range;
    var xStart = coord[0][0] > coord[1][0] ? coord[1][0] : coord[0][0];
    var xDiff = coord[0][0] - coord[1][0];
    var xCount = Math.Abs(xDiff) + 1;
    var xRange = Enumerable.Range(xStart, xCount);
    var xIsDescending = xDiff > 0;

    if (xIsDescending)
    {
        xRange = xRange.Reverse();
    }

    var yStart = coord[0][1] > coord[1][1] ? coord[1][1] : coord[0][1];
    var yDiff = coord[0][1] - coord[1][1];
    var yCount = Math.Abs(yDiff) + 1;
    var yRange = Enumerable.Range(yStart, yCount);
    var yIsDescending = yDiff > 0;

    if(yIsDescending)
    {
        yRange = yRange.Reverse();
    }

    
    range = xRange.Zip(yRange, (x, y) => $"{x},{y}").ToArray();


    foreach (var point in range)
    {
        if (!hitDict.TryGetValue(point, out int val))
        {
            hitDict.Add(point, 1);
        }
        else
        {
            hitDict[point] = hitDict[point] + 1;
        };
    }
}

var result2 = hitDict.Sum(point => point.Value > 1 ? 1 : 0);
Console.WriteLine($"Day 5 - Part 2 Result: {result2}");