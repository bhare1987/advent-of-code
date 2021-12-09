using System;

Console.WriteLine("Day 8 - Part 1");
var lines = from line in File.ReadAllLines("input.txt") select line.Trim();
var convertedInput = lines.Select(l => l.Split("|").Select(s => s.Trim()).ToArray()).ToArray();

Func<int, int> mapDigitCount = (digitCount) => {
    switch (digitCount)
    {
        case 2:
            return 1;
        case 3:
            return 7;
        case 4:
            return 4;
        case 7:
            return 8;
        default:
            return 0;
    }
};

var digitCount = convertedInput.Select(i => i[1].Split(' ')).SelectMany(d => d).Select(d => d.Count()).Aggregate(new Dictionary<int, double>(), (counts, digit) => {
    var mappedDigit = mapDigitCount(digit);
    if (counts.ContainsKey(mappedDigit))
    {
        counts[mappedDigit] += 1;
    }
    else
    {
        counts.Add(mappedDigit, 1);
    }
    return counts;
});

var total = digitCount.Values.Sum() - digitCount[0];

Console.WriteLine($"Day 8 - Part 1 Result: {total}");
Console.WriteLine($"Day 8 - Part 2");

Func<string, int, Dictionary<int, string>, bool> containsAllValuesOfDigit = (digitToCheck, containingDigit, map) => {
    map.TryGetValue(containingDigit, out string digit);

    if (digit == null)
    {
        return false;
    }

    foreach (char c in digit)
    {
        if (!digitToCheck.Contains(c))
        {
            return false;
        }
    };

    return true;
};

var total2 = convertedInput.Select(input =>
{
    var map = new Dictionary<int, string>();
    var unmappedDigits = new List<string>();
    // Map known digits first
    foreach (var digit in input[0].Split(' '))
    {
        var sorted = String.Concat(digit.OrderBy(c => c));
        var mappedCount = mapDigitCount(digit.Count());
        if (mappedCount != 0)
        {
            map.Add(mappedCount, sorted);
        }
        else
        {
            unmappedDigits.Add(sorted);
        }
    }

    foreach (var unmapped in unmappedDigits)
    {
        var length = unmapped.Count();
        if (length == 6)
        {
            // 6 has length 6 and won't have 1 in it.
            if (!containsAllValuesOfDigit(unmapped, 1, map))
            {
                map.Add(6, unmapped);
            }
            // 0 has length 6 and won't have 4 in it.
            else if (!containsAllValuesOfDigit(unmapped, 4, map))
            {
                map.Add(0, unmapped);
            }
            // 9 has length 6 and will have 1, 4, and 7 in it. But last list
            else
            {
                map.Add(9, unmapped);
            }

        }
        else
        {
            // 3 has length 5 and will have 7 in it.
            if (containsAllValuesOfDigit(unmapped, 7, map))
            {
                map.Add(3, unmapped);
                continue;
            }
            // 5 has length 5 and is one digit off four. 
            map.TryGetValue(4, out string four);
            var isFive = four.Except(unmapped);
            if (isFive.Count() == 1)
            {
                map.Add(5, unmapped);
            }
            // 2 must be only one left.
            else
            {
                map.Add(2, unmapped);
            }
        }
    }

    var result = input[1].Split(' ').Select(s =>
    {
        var sorted = String.Concat(s.OrderBy(c => c));
        var val = map.FirstOrDefault(x => x.Value == sorted).Key;
        return val.ToString();
    }).ToArray();
    return double.Parse(String.Concat(result));
}).ToArray().Sum();


Console.WriteLine($"Day 8 - Part 2 Result: {total2}");
