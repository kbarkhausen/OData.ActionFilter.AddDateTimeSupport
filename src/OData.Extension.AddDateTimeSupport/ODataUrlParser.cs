using OData.ActionFilter.AddDateTimeSupport.Models;

namespace OData.ActionFilter.AddDateTimeSupport
{
    public class ODataUrlParser
    {
        public UriQueryOption ExtractFilterQuery(string url)
        {
            var urlSection = GetSubStringInBetween(url, "$filter", "&");
            return urlSection;
        }

        private UriQueryOption GetSubStringInBetween(string input, string startsWith, string endsWith)
        {
            int Start, End, Length;
            if (input.Contains(startsWith))
            {
                Start = input.IndexOf(startsWith, 0);
                End = input.IndexOf(endsWith, Start);

                if (End < 0)
                    End = input.Length;

                Length = End - Start;                

                return new UriQueryOption
                {
                    StringValue = input.Substring(Start, End - Start),
                    StartPosition = Start,
                    Length = Length
                };
            }
            else
            {
                return null;
            }
        }
    }
}
