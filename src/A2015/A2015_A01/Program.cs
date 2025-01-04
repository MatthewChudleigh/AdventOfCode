// See https://aka.ms/new-console-template for more information
var baseDir = Environment.GetEnvironmentVariable("AOC_2015_DATA")!;
var dataPath = Path.Combine(baseDir, "A01.txt");
var data = File.ReadAllText(dataPath);

var floor = 0;
var basement = 0;
var position = 0;
foreach (var c in data)
{
    position++;
    floor += c == '(' ? 1 : -1;
    
    if (floor == -1 && basement == 0)
    {
        basement = position;
    }
}

Console.WriteLine(floor);
Console.WriteLine(basement);
