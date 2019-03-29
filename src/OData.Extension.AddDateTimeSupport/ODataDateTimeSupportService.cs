using OData.ActionFilter.AddDateTimeSupport.Models;
using System;
using System.Linq;

namespace OData.ActionFilter.AddDateTimeSupport
{
    public class ODataDateTimeSupportService 
    {
        private ODataUrlParser _oDataUrlParser;
        private DateTimeUriParser _dateTimeUriParser;

        public ODataDateTimeSupportService()
        {
            _oDataUrlParser = new ODataUrlParser();
            _dateTimeUriParser = new DateTimeUriParser();
        }

        /// <summary>
        /// It will take the current OData request Uri and update it.
        /// It will change any Date value used in $filter and change it to a DateTimeOffset value
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public Uri UpdateRequestUri(Uri requestUri)
        {
            try
            {
                var requestUrl = requestUri.UnescapedAbsoluteUri();

                var filterQuery = _oDataUrlParser.ExtractFilterQuery(requestUrl);

                if (filterQuery != null)
                {
                    var dateTimeMatchesInUri = _dateTimeUriParser.Parse(filterQuery.StringValue);

                    if (dateTimeMatchesInUri.Count() > 0)
                    {
                        foreach (DateTimeMatch item in dateTimeMatchesInUri)
                        {
                            var dateTimeMatchValueAsOffset = item.DateTimeValue.ToString("yyyy-MM-ddT00:00:00KZ");
                            filterQuery.StringValue = filterQuery.StringValue.Replace(item.RegexMatch, dateTimeMatchValueAsOffset);
                        }

                        // remove the previous $filter value and replace with new
                        requestUrl = requestUrl.Remove(filterQuery.StartPosition, filterQuery.Length);
                        requestUrl = requestUrl.Insert(filterQuery.StartPosition, filterQuery.StringValue);

                        // create new Uri with modified $filter value
                        requestUri = new Uri(requestUrl);
                    }
                }               
            }
            catch
            {
                // if error occurs - ignore the error
                // return the original Uri without any modifications
            }

            // if there an error adding the access filter; return the original Uri unchanged
            return requestUri;
        }

        private static string DecodeUrlString(string url)
        {
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }
    }
}