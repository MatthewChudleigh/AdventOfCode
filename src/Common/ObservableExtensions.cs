namespace Common;

public static class ObservableExtensions
{
    public static GridObservable ToGridObservable(this IObservable<char> source)
    {
        return new GridObservable(source);
    }
}