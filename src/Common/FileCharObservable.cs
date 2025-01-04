namespace Common;

public class FileCharObservable(string path) : IObservable<char>
{
    private readonly string _data = File.ReadAllText(path);
    
    public IDisposable Subscribe(IObserver<char> observer)
    {
        foreach (var c in _data)
        {
            observer.OnNext(c);
        }
        observer.OnCompleted();
        return System.Reactive.Disposables.Disposable.Empty;
    }
}