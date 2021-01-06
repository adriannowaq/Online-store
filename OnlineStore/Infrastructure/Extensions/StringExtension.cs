namespace OnlineStore.Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static string RemoveController(this string str) =>
            str.Replace("Controller", string.Empty);
    }
}
