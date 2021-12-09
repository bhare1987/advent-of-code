Console.WriteLine("Day 6 - Part 1");
var lines = from line in File.ReadAllLines("input.txt") select line.Trim();
var convertedInput = lines.ElementAt(0).Split(',').Select(line => int.Parse(line)).ToArray();
var grouped = convertedInput.GroupBy(f => f).ToDictionary(v => v.Key, v => (double)v.Count());
var fishDict = new SortedDictionary<int, double>(grouped);

for (int i = 0; i < 80; i++)
{
    var updatedFish = new SortedDictionary<int, double>();
    for (int j = 0; j < fishDict.Count(); j++)
    {
        KeyValuePair<int, double> fish = fishDict.ElementAt(j);
        if (fish.Key == 0)
        {
            fishDict.TryGetValue(7, out double sevenFish);
            fishDict[7] = sevenFish + fish.Value;
            updatedFish.Add(8, fish.Value);
        }
        else
        {
            var key = fish.Key - 1;
            var value = fish.Value;
            updatedFish.TryAdd(key, value);
        }
    }
    fishDict = updatedFish;
}

var result = fishDict.Values.Sum();

Console.WriteLine($"Day 6 - Part 1 Result: {result}");
Console.WriteLine("Day 6 - Part 2");

for (int i = 80; i < 256; i++)
{

    var updatedFish = new SortedDictionary<int, double>();
    for (int j = 0; j < fishDict.Count(); j++)
    {
        KeyValuePair<int, double> fish = fishDict.ElementAt(j);
        if (fish.Key == 0)
        {
            fishDict.TryGetValue(7, out double sevenFish);
            fishDict[7] = sevenFish + fish.Value;
            updatedFish.Add(8, fish.Value);
        }
        else
        {
            var key = fish.Key - 1;
            var value = fish.Value;
            updatedFish.TryAdd(key, value);
        }
    }
    fishDict = updatedFish;
}

var result2 = fishDict.Values.Sum();

Console.WriteLine($"Day 6 - Part 2 Result {result2}");