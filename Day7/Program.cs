Console.WriteLine("Day 7 - Part 1");
var lines = from line in File.ReadAllLines("input.txt") select line.Trim();
var convertedInput = lines.ElementAt(0).Split(',').Select(line => int.Parse(line)).ToArray();

var sortedInput = convertedInput.OrderBy(n => n).ToArray();
var midpoint = sortedInput.Count() / 2;

double median = sortedInput.ElementAt(midpoint);

if (sortedInput.Count() % 2 == 0)
{
    median = Math.Floor((median + sortedInput.ElementAt(midpoint + 1)) / 2);
}

var fuelCost = sortedInput.Aggregate((double)0, (total, position) =>
{
    total += Math.Abs(median - position);
    return total;
});

Console.WriteLine($"Day 7 - Part 1 Result: {fuelCost}");
Console.WriteLine("Day 7 - Part 2");

var avg = Math.Floor(sortedInput.Average());

var updatedFuelCost = sortedInput.Aggregate((double)0, (total, position) =>
{
    var distance = Math.Abs(avg - position);
    double sum = 0;
    for (int i = 0; i < distance; i++)
    {
        sum += i + 1;
    }
    total += sum;
    return total;
});


Console.WriteLine($"Day 7 - Part 2 Result: {updatedFuelCost}");
