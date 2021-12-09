using System.Text;

Console.WriteLine("Day 3 - Part 1");
var lines = from line in File.ReadAllLines("input.txt") where !string.IsNullOrEmpty(line) select line.Trim();
var convertedInput = lines.Select(line => line.ToCharArray().Select(c => c.ToString()).Select(item => double.Parse(item)).ToArray()).ToArray();

var sums = new Dictionary<int, double>();
double totalInputs = convertedInput.Count();

foreach (double[] binArr in convertedInput)
{
    for (int i = 0; i < binArr.Count(); i++)
    {
        sums.TryAdd(i, 0);
        sums[i] += binArr[i];
    }
}

var gamma = new StringBuilder();
var epsilon = new StringBuilder();

foreach (var sum in sums)
{
    double diff = sum.Value / totalInputs;
    int mostCommon;
    int leastCommon;
    if (Math.Round(diff) == 0)
    {
        mostCommon = 0;
        leastCommon = 1;
    }
    else
    {
        mostCommon = 1;
        leastCommon = 0;
    }
    gamma.Append(mostCommon.ToString());
    epsilon.Append(leastCommon.ToString());
}

var result = Convert.ToInt32(gamma.ToString(), 2) * Convert.ToInt32(epsilon.ToString(), 2);

Console.WriteLine($"Day 3 - Part 1 Result: {result}");
Console.WriteLine("Day 3 - Part 2");

// Need to do the sums for each subsequent list.

var rollingCountsOxygen = new Dictionary<int, List<double[]>>()
{
    [0] = new List<double[]>(convertedInput)
};

for (var i = 0; i < gamma.Length; i++)
{
    var total = rollingCountsOxygen[i].Sum(c => c[i]);
    double diff = total / rollingCountsOxygen[i].Count();
    rollingCountsOxygen.Add(i + 1, new List<double[]>());
    var mostCommon = diff >= .5 ? 1 : 0;

    if (rollingCountsOxygen[i].Count() == 1)
    {
        break;
    }

    foreach (double[] bitArr in rollingCountsOxygen[i])
    {
        if (bitArr[i] == mostCommon)
        {
            rollingCountsOxygen[i + 1].Add(bitArr);
        }
    }

    if (rollingCountsOxygen[i + 1].Count == 1)
    {
        break;
    }

}

var rollingCountsC02 = new Dictionary<int, List<double[]>>()
{
    [0] = new List<double[]>(convertedInput)
};

for (var i = 0; i < gamma.Length; i++)
{
    var total = rollingCountsC02[i].Sum(c => c[i]);
    double diff = total / rollingCountsC02[i].Count();
    rollingCountsC02.Add(i + 1, new List<double[]>());
    var leastCommon = diff >= .5 ? 0 : 1;

    foreach (double[] bitArr in rollingCountsC02[i])
    {
        if (bitArr[i] == leastCommon)
        {
            rollingCountsC02[i + 1].Add(bitArr);
        }
    }

    if (rollingCountsC02[i + 1].Count() == 1)
    {
        break;
    }

}

var result2 = Convert.ToInt32(string.Join("", rollingCountsOxygen[rollingCountsOxygen.Count() - 1].First()), 2) * Convert.ToInt32(string.Join("", rollingCountsC02[rollingCountsC02.Count() - 1].First()), 2);

Console.WriteLine($"Day 3 - Part 2 Result { result2 }");