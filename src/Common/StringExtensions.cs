namespace Common;

public static class StringExtensions
{
    public static IEnumerable<(int i, string s)> SlidingWindow(this string str, int windowSize, int start = 0)
    {
        for (var i = start; i <= str.Length - windowSize; ++i)
        {
            yield return (i, str.Substring(i, windowSize));
        }
    }
}