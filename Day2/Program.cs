// See https://aka.ms/new-console-template for more information

Console.WriteLine("Day 2 - Part 1");
var lines = from line in File.ReadAllLines("input.txt") where !string.IsNullOrEmpty(line) select line.Trim();
var convertedInput = lines.Select(line => line).ToArray();

var results = new Dictionary<string, double>();
results.Add("horizontal", 0);
results.Add("vertical", 0);

foreach (string instruction in convertedInput)
{
    var parsed = instruction.Split(" ");
    var direction = parsed[0];
    var distance = Double.Parse(parsed[1]);

    if (direction == "forward")
    {
        results.TryGetValue("horizontal", out double horizontal);
        results["horizontal"] = horizontal + distance;
    }
    else
    {

        var verticalDirection = direction == "up" ? -1 : 1;
        results.TryGetValue("vertical", out double vert);
        results["vertical"] = vert + (verticalDirection * distance);
    }
}

results.TryGetValue("horizontal", out double hor);
results.TryGetValue("vertical", out double ver);

Console.WriteLine($"Distance: {hor * ver}");

Console.WriteLine("Day 2 - Part 2");

var results2 = new Dictionary<string, double>();
results2.Add("horizontal", 0);
results2.Add("vertical", 0);
results2.Add("aim", 0);

foreach (string instruction in convertedInput)
{
    var parsed = instruction.Split(" ");
    var direction = parsed[0];
    var distance = Double.Parse(parsed[1]);

    if (direction == "forward")
    {
        results2.TryGetValue("aim", out double aim);
        results2.TryGetValue("horizontal", out double horizontal);
        results2.TryGetValue("vertical", out double vertical);
        results2["horizontal"] = horizontal + distance;
        results2["vertical"] = vertical + (distance * aim);
    }
    else
    {

        var verticalDirection = direction == "up" ? -1 : 1;
        results2.TryGetValue("aim", out double aim);
        results2["aim"] = aim + (verticalDirection * distance);
    }
}

results2.TryGetValue("horizontal", out double hor2);
results2.TryGetValue("vertical", out double ver2);

Console.WriteLine($"Distance: {hor2 * ver2}");


