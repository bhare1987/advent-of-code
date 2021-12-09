using System;

Console.WriteLine("Day 4 - Part 1");
var lines = from line in File.ReadAllLines("input.txt") select line.Trim();
var convertedInput = lines.ElementAt(0).Split(',').Select(line => int.Parse(line)).ToArray();

// Could improve this by making the grids dictionaries for faster lookup. Could save loop cycles.
var bingoBoards = lines.Skip(1).Aggregate(new List<List<int[]>>(), (boards, line) => {
    if (String.IsNullOrWhiteSpace(line))
    {
        boards.Add(new List<int[]>());

    }
    else
    {
        var row = line.Replace("  ", " ").Split(' ').Select(l => int.Parse(l.ToString())).ToArray();
        boards.Last().Add(row);
    }
    return boards;
});

List<int[]> winner = null;
int winningGuess = 0;

foreach (int guess in convertedInput)
{
    foreach (var board in bingoBoards)
    {
        if (winner != null)
        {
            break;
        }
        for (var i = 0; i < board.Count(); i++)
        {
            var row = board.ElementAt(i);
            var index = Array.IndexOf(row, guess);
            if (index > -1)
            {
                row[index] = -1;
                var col = board.Select(r => r[i]).ToArray();
                if (row.Sum(x => x) == -5 || col.Sum(x => x) == -5)
                {
                    winner = board;
                    winningGuess = guess;
                    break;
                }
            
            }

        }
    }
}

var sum = winner.Aggregate(0, (sum, val) => {
    var rowSum = val.Where(v => v != -1).Sum();
    sum += rowSum;
    return sum;
});

Console.WriteLine($"Day 4 - Part 1 Result: {sum * winningGuess}");
Console.WriteLine("Day 4 - Part 2");

List<(int, List<int[]>)> lastWinner = new List<(int, List<int[]>)>();

foreach (int guess in convertedInput)
{
    bingoBoards = bingoBoards.Where(b => b != null).ToList();

    for (var j = 0; j < bingoBoards.Count(); j++)
    {
        var board = bingoBoards[j];
        for (var i = 0; i < board.Count(); i++)
        {
            var row = board.ElementAt(i);
            var index = Array.IndexOf(row, guess);
            if (index > -1)
            {
                row[index] = -1;
                var col = board.Select(r => r[i]).ToArray();
                if (row.Sum(x => x) == -5 || col.Sum(x => x) == -5)
                {
                    lastWinner.Add((guess, board.ConvertAll(r => (int[]) r.Clone())));
                    bingoBoards[j] = null;
                    break;
                }
            }
        }
    }
}

var lastSum = lastWinner.Last().Item2.Aggregate(0, (sum, val) => {
    var rowSum = val.Where(v => v != -1).Sum();
    sum += rowSum;
    return sum;
});

Console.WriteLine($"Day 4 - Part 2 Result: {lastSum * lastWinner.Last().Item1}");