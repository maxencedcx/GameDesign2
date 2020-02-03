public static class StringExtensions
{
    public static string FormatWith(this string stringValue, params object[] arguments)
    {
        return string.Format(stringValue, arguments);
    }
}
