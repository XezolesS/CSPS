static partial class BinarySearchExtension
{
    public static int SearchUpperBound<T>(this T[] array, T value) where T : IComparable<T> => SearchUpperBoundImpl(array, 0, array.Length, value, Comparer<T>.Default);
    public static int SearchUpperBound<T>(this T[] array, T value, IComparer<T> comparer) => SearchUpperBoundImpl(array, 0, array.Length, value, comparer);
    public static int SearchUpperBound<T>(this T[] array, int index, int length, T value) where T : IComparable<T> => SearchUpperBoundImpl(array, index, length, value, Comparer<T>.Default);
    public static int SearchUpperBound<T>(this T[] array, int index, int length, T value, IComparer<T> comparer) => SearchUpperBoundImpl(array, index, length, value, comparer);
    public static int SearchUpperBound<T>(this List<T> list, T value) where T : IComparable<T> => SearchUpperBoundImpl(list, 0, list.Count, value, Comparer<T>.Default);
    public static int SearchUpperBound<T>(this List<T> list, T value, IComparer<T> comparer) => SearchUpperBoundImpl(list, 0, list.Count, value, comparer);
    public static int SearchUpperBound<T>(this List<T> list, int index, int count, T value) where T : IComparable<T> => SearchUpperBoundImpl(list, index, count, value, Comparer<T>.Default);
    public static int SearchUpperBound<T>(this List<T> list, int index, int count, T value, IComparer<T> comparer) => SearchUpperBoundImpl(list, index, count, value, comparer);

    private static int SearchUpperBoundImpl<T>(IList<T> list, int index, int count, T value, IComparer<T> comparer)
    {
        int lo = index, hi = count;
        while (lo < hi)
        {
            int mid = (lo + hi) / 2;
            int comp = comparer.Compare(value, list[mid]);

            if (comp < 0)
                hi = mid;
            else
                lo = mid + 1;
        }

        return hi;
    }
}