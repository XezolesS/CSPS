// List<T> has it's own extension BinarySearch() method, but array does not!
static partial class BinarySearchExtension
{
    public static int BinarySearch<T>(this T[] array, T value) where T : IComparable<T> => Array.BinarySearch(array, value);
    public static int BinarySearch<T>(this T[] array, int index, int length, T value) where T : IComparable<T> => Array.BinarySearch(array, index, length,value);
    public static int BinarySearch<T>(this T[] array, T value, IComparer<T> comparer) => Array.BinarySearch(array, value, comparer);
    public static int BinarySearch<T>(this T[] array, int index, int length, T value, IComparer<T> comparer) => Array.BinarySearch(array, index, length, value, comparer);
}