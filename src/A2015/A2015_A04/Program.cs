using System.Reactive.Linq;
using System.Security.Cryptography;
using System.Text;
using Common;

var baseDir = Environment.GetEnvironmentVariable("AOC_2015_DATA")!;
var dataPath = Path.Combine(baseDir, "A04.txt");

var s1Task = new TaskCompletionSource<int>();
var s2Task = new TaskCompletionSource<int>();

var inf = Observable.Generate(1, _ => true, i => i + 1, i => i);
var fs = (new FileLineObservable(dataPath))
        .SelectMany((f) => inf.Select(i => ComputeMd5Hash(f, i)))
        .Publish()
        .RefCount();

using var s1 = fs.Where(data => data.hash.StartsWith("00000"))
        .FirstOrDefaultAsync().Select(d => d.index)
        .Subscribe(s1Task.SetResult, () => { });

using var s2 = fs.Where(data => data.hash.StartsWith("000000"))
    .FirstOrDefaultAsync().Select(d => d.index)
    .Subscribe(s2Task.SetResult);

await Task.WhenAll(s1Task.Task, s2Task.Task);

Console.WriteLine(s1Task.Task.Result);
Console.WriteLine(s2Task.Task.Result);

return;

(int index, string hash) ComputeMd5Hash(string prefix, int i)
{
    var inputBytes = Encoding.ASCII.GetBytes($"{prefix}{i}");
    var hash = MD5.HashData(inputBytes);
    return (i, Convert.ToHexString(hash));
}