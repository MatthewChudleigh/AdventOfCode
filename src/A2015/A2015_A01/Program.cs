using System.Reactive.Linq;
using Common;

var floorTask = new TaskCompletionSource<int>();
var basementTask = new TaskCompletionSource<int>();

var baseDir = Environment.GetEnvironmentVariable("AOC_2015_DATA")!;
var dataPath = Path.Combine(baseDir, "A01.txt");
var fs = (new FileCharObservable(dataPath))
    .Select(c => (c == '(' ? 1 : -1))
    .Scan((position: 0, floor: 0), (agg, data) => (agg.position + 1, floor: agg.floor + data))
    .Publish();

using var s1 = fs.LastOrDefaultAsync()
    .Select(x => x.floor)
    .Subscribe(floorTask.SetResult);
using var s2 = fs.FirstAsync(x => x.floor == -1)
    .Select(x => x.position)
    .Subscribe(basementTask.SetResult);

fs.Connect();

await Task.WhenAll(floorTask.Task, basementTask.Task);

Console.WriteLine(floorTask.Task.Result);
Console.WriteLine(basementTask.Task.Result);
