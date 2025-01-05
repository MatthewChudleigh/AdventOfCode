using System.Reactive.Linq;

namespace Common;

public record Pos(int X, int Y);

public class GridObservable(IObservable<char> source) : IObservable<Pos>
{
    public IDisposable Subscribe(IObserver<Pos> observer)
    {
        return source
            .Merge(Observable.Return('.'))
            .Scan(new Pos(0,0), (pos, dir) =>
            {
                return dir switch
                {
                    '>' => pos with { X = pos.X + 1 },
                    '<' => pos with { X = pos.X - 1 },
                    '^' => pos with { Y = pos.Y + 1 },
                    'v' => pos with { Y = pos.Y - 1 },
                    _ => pos
                };
            })
            .Subscribe(observer);
    }
}
