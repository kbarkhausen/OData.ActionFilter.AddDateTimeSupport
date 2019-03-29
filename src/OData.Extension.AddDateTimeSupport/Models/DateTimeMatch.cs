using System;

namespace OData.ActionFilter.AddDateTimeSupport.Models
{
    public class DateTimeMatch
    {
        public string RegexMatch { get; set; }

        public DateTime DateTimeValue { get; set; }
    }
}
