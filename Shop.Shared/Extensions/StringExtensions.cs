namespace Shop.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string FormatImageUrl(this string url, string baseUrl)
        {

            if (string.IsNullOrWhiteSpace(url))
            {
                // TODO: Upload the PlaceHolder image and return the placeholder URl
                return url;
            }

            if (baseUrl.EndsWith("/"))
            {
                // If the base URL ends with a slash, remove it
                baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
            }

            if (url.StartsWith("/"))
            {
                // If it's a relative URL, remove the leading slash
                url = url.Substring(1, baseUrl.Length);
            }

            return $"{baseUrl}/{url}";
        }
    }
}
