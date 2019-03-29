using System;

namespace OData.ActionFilter.AddDateTimeSupport
{
    public static class UriExtensions
    {
        public static string UnescapedAbsoluteUri(this Uri uri)
        {
            var url = uri.AbsoluteUri;
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }
    }
}
