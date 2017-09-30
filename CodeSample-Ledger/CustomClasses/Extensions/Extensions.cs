namespace CodeSample_Ledger.Extensions
{
    public static class Extensions
    {
        // Extends generic arrays.
        // Returns true if the array is null or empty.
        // Returns false otherwise.
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }
    }
}
