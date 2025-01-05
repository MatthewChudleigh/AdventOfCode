// See https://aka.ms/new-console-template for more information

using System.Reactive.Linq;
using Common;

var baseDir = Environment.GetEnvironmentVariable("AOC_2015_DATA")!;
var dataPath = Path.Combine(baseDir, "A02.txt");

var paperTask = new TaskCompletionSource<int>();
var ribbonTask = new TaskCompletionSource<int>();

var presents = new FileLineObservable(dataPath)
    .Select(line => line.Split('x'))
    .Select(d => d.Select(int.Parse).ToList())
    .Publish();

var area = presents
    .Select(d => (int[]) [d[0] * d[1], d[1] * d[2], d[2] * d[0]]);

var perimeter = presents
    .Select(d => (int[]) [d[0] + d[1], d[1] + d[2], d[2] + d[0]])
    .Select(sides => sides.Min() * 2);

var volume = presents
    .Select(d => d[0] * d[1] * d[2]);

using var s1 = area
    .Sum(sides => 2 * sides.Sum() + sides.Min())
    .Subscribe(paperTask.SetResult);

using var s2 = (perimeter.Zip(volume, (p, v) => p + v))
    .Sum()
    .Subscribe(ribbonTask.SetResult);

presents.Connect();

await Task.WhenAll(paperTask.Task, ribbonTask.Task);

Console.WriteLine(paperTask.Task.Result);
Console.WriteLine(ribbonTask.Task.Result);