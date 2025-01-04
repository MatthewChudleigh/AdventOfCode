// See https://aka.ms/new-console-template for more information

using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Common;

var baseDir = Environment.GetEnvironmentVariable("AOC_2015_DATA")!;
var dataPath = Path.Combine(baseDir, "A01.txt");
var fs = (new FileCharObservable(dataPath));

var step = await 
    fs.Scan((step: 0, floor: 0), 
    (i, c) => c == '(' 
        ? (i.step + 1, i.floor + 1) 
        : (i.step + 1, i.floor - 1)
    ).Aggregate((basement: 0, floor: 0), 
        (s, i) => 
            (s.basement == 0 && i.floor == -1 ? i.step : s.basement, i.floor))
    .ToTask();

Console.WriteLine(step.floor);
Console.WriteLine(step.basement);
