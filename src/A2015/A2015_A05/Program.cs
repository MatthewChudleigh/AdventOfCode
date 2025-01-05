using System.Reactive.Linq;
using Common;

var baseDir = Environment.GetEnvironmentVariable("AOC_2015_DATA")!;
var dataPath = Path.Combine(baseDir, "A05.txt");

var s1Task = new TaskCompletionSource<int>();
var s2Task = new TaskCompletionSource<int>();

var strings = new FileLineObservable(dataPath).Publish();

var vowels = strings.Select(s => s.Count(c => c is 'a' or 'e' or 'i' or 'o' or 'u') >= 3);
var doubles = strings.Select(s => s.SlidingWindow(2).Any(x => x.s[0] == x.s[1]));
var legals = strings.Select(s => !s.SlidingWindow(2).Any(x => x.s is "ab" or "cd" or "pq" or "xy"));

var dupes = strings.Select(s =>
{
    var b = s.SlidingWindow(2).Any(x => 
        s.SlidingWindow(2, x.i + 2).Any(y => 
            x.s[0] == y.s[0] && x.s[1] == y.s[1])
        );
    return (s, b);
});
var twins = strings.Select(s =>
{
    var b = s.SlidingWindow(3).Any(x => x.s[0] == x.s[2]);
    return (s, b);
});

using var s1 = Observable.Zip(vowels, doubles, legals)
    .Count(vdl => vdl[0] && vdl[1] && vdl[2])
    .Subscribe(s1Task.SetResult);

using var s2 = Observable.Zip(dupes, twins)
    .Count(dt => dt[0].b && dt[1].b)
    .Subscribe(s2Task.SetResult);

strings.Connect();

Task.WaitAll(s1Task.Task, s2Task.Task);

Console.WriteLine(s1Task.Task.Result);
Console.WriteLine(s2Task.Task.Result);