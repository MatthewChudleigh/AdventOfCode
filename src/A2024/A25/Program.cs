// See https://aka.ms/new-console-template for more information

var baseDir = Environment.GetEnvironmentVariable("AOC_BaseDir");
var data = File.ReadAllLines(Path.Combine(baseDir!, "A25.data.txt"));
var (locks, keys) = Solution.Load(data);
var total = Solution.Solve(locks, keys);
Console.WriteLine(total);

public static class Solution
{
    public static int Solve(List<int[]> locks, List<int[]> keys)
    {
        var total = 0;
        foreach (var k in keys)
        {
            foreach (var l in locks)
            {
                var ok = true;
                for (var p = 0; p < k.Length; p++)
                {
                    if (k[p] + l[p] > 5)
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    total++;
                }
            }
        }

        return total;
    }
    
    public static (List<int[]> Locks, List<int[]> Keys) Load(string[] data)
    {
        var locks = new List<int[]>();
        var keys = new List<int[]>();

        foreach (var chunk in data.Chunk(8))
        {
            var pins = new int[5];
            var pin = chunk[0][0] == '#' ? '.' : '#';

            for (var p = 0; p < 5; ++p)
            {
                for (var j = 1; j < 7; ++j)
                {
                    if (chunk[j][p] == pin)
                    {
                        pins[p] = pin == '#' ? 6-j : j-1;
                        break;
                    }
                }
            }

            if (pin == '#')
            {
                keys.Add(pins);
            }
            else
            {
                locks.Add(pins);
            }
        }

        return (locks, keys);
    }

    public static void Print(string[] locks, string[] keys)
    {
        Console.WriteLine("Keys");
        foreach (var key in keys)
        {
            Console.WriteLine(string.Join(" ", key));
        }

        Console.WriteLine("Locks");
        foreach (var l in locks)
        {
            Console.WriteLine(string.Join(" ", l));
        }
    }
}
