namespace ObjToForm.Extensions
{
    internal static class StringExtension
    {
        internal static int GetEnumerableIndex(this string str)
        {
            ReadOnlySpan<char> span = str.AsSpan();
            int start = -1;
            int end = -1;

            for (int i = 0; i < span.Length; i++)
            {
                if (span[i] == '[')
                    start = i + 1;
                else if (span[i] == ']')
                {
                    end = i;
                    break;
                }
            }

            if (start == -1 || end == -1 || end <= start)
                return -1;

            int value = 0;
            for (int i = start; i < end; i++)
            {
                char c = span[i];
                if ((uint)(c - '0') > 9)
                    return -1;
                value = value * 10 + (c - '0');
            }

            return value;
        }
    }
}
