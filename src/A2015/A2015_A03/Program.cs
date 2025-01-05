using System.Reactive.Linq;
using Common;

var baseDir = Environment.GetEnvironmentVariable("AOC_2015_DATA")!;
var dataPath = Path.Combine(baseDir, "A03.txt");

var s1Task = new TaskCompletionSource<int>();
var s2Task = new TaskCompletionSource<int>();

var fs = (new FileCharObservable(dataPath)).Publish();

var realSanta = fs.Where((_, i) => i % 2 == 0).ToGridObservable();
var roboSanta = fs.Where((_, i) => i % 2 == 1).ToGridObservable();

using var s1 = fs.ToGridObservable()
    .Distinct().Count()
    .Subscribe(s1Task.SetResult); 

using var s2 = realSanta.Merge(roboSanta)
    .Distinct().Count()
    .Subscribe(s2Task.SetResult);

fs.Connect();

await Task.WhenAll(s1Task.Task, s2Task.Task);

Console.WriteLine(s1Task.Task.Result);
Console.WriteLine(s2Task.Task.Result);