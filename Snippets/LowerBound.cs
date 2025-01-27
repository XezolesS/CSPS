static partial class BinarySearchExtension
{
    public static int SearchLowerBound<T>(this T[] array, T value) where T : IComparable<T> => SearchLowerBoundImpl(array, 0, array.Length, value, Comparer<T>.Default);
    public static int SearchLowerBound<T>(this T[] array, T value, IComparer<T> comparer) => SearchLowerBoundImpl(array, 0, array.Length, value, comparer);
    public static int SearchLowerBound<T>(this T[] array, int index, int length, T value) where T : IComparable<T> => SearchLowerBoundImpl(array, index, length, value, Comparer<T>.Default);
    public static int SearchLowerBound<T>(this T[] array, int index, int length, T value, IComparer<T> comparer) => SearchLowerBoundImpl(array, index, length, value, comparer);
    public static int SearchLowerBound<T>(this List<T> list, T value) where T : IComparable<T> => SearchLowerBoundImpl(list, 0, list.Count, value, Comparer<T>.Default);
    public static int SearchLowerBound<T>(this List<T> list, T value, IComparer<T> comparer) => SearchLowerBoundImpl(list, 0, list.Count, value, comparer);
    public static int SearchLowerBound<T>(this List<T> list, int index, int count, T value) where T : IComparable<T> => SearchLowerBoundImpl(list, index, count, value, Comparer<T>.Default);
    public static int SearchLowerBound<T>(this List<T> list, int index, int count, T value, IComparer<T> comparer) => SearchLowerBoundImpl(list, index, count, value, comparer);

    private static int SearchLowerBoundImpl<T>(IList<T> list, int index, int count, T value, IComparer<T> comparer)
    {
        int lo = index, hi = index + count - 1;
        while (lo < hi)
        {
            int mid = (lo + hi) / 2;
            int comp = comparer.Compare(value, list[mid]);

            if (comp <= 0)
                hi = mid;
            else
                lo = mid + 1;
        }

        return hi;
    }
}