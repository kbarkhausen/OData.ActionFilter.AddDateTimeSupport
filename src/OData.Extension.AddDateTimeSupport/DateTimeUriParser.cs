using OData.ActionFilter.AddDateTimeSupport.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OData.ActionFilter.AddDateTimeSupport
{
    public class DateTimeUriParser
    {
        public List<DateTimeMatch> Parse(string input)
        {
            var searchPatterns = new List<SearchPattern>();
            searchPatterns.Add(new SearchPattern
            {
                // Search Pattern = 'MM/DD/YYYY'
                Regex = @"'[0,1]?\d{1}\/(([0-2]?\d{1})|([3][0,1]{1}))\/(([1]{1}[9]{1}[9]{1}\d{1})|([1-9]{1}\d{3}))'",
                ContainsSingleQuotes = true
            });
            searchPatterns.Add(new SearchPattern
            {
                // Search Pattern = 'YYYY-MM-DD'
                Regex = @"'(([1]{1}[9]{1}[9]{1}\d{1})|([1-9]{1}\d{3}))-[0,1]?\d{1}-(([0-3]?\d{1})|([3][0,1]{1}))'",
                ContainsSingleQuotes = true
            });
            searchPatterns.Add(new SearchPattern
            {
                // Search Pattern = YYYY-MM-DD (no quotes)
                Regex = @"(([1]{1}[9]{1}[9]{1}\d{1})|([1-9]{1}\d{3}))-[0,1]?\d{1}-(([0-3]?\d{1})|([3][0,1]{1}))",
                ContainsSingleQuotes = false
            });

            var matches = new List<DateTimeMatch>();
            foreach (var searchPattern in searchPatterns)
            {
                MatchCollection mc = Regex.Matches(input, searchPattern.Regex);
                string[] strArray = new string[mc.Count];
                for (int i = 0; i < mc.Count; i++)
                {
                    var matchStringDateValue = mc[i].Groups[0].Value;
                    var matchStringStartPosition = mc[i].Groups[0].Index;
                    var matchStringLength = mc[i].Groups[0].Length;

                    DateTime dateValue;

                    // if this pattern contains single quotes
                    if (searchPattern.ContainsSingleQuotes)
                    {
                        dateValue = DateTime.Parse(matchStringDateValue.Replace("'", ""));
                    }
                    else
                    {
                        dateValue = DateTime.Parse(matchStringDateValue);
                    }

                    matches.Add(new DateTimeMatch
                    {
                        RegexMatch = matchStringDateValue,
                        DateTimeValue = dateValue
                    });

                    input = input.Remove(matchStringStartPosition, matchStringLength);
                    input = input.Insert(matchStringStartPosition, new string(' ', matchStringLength));
                };
            }

            return matches;
        }
    }

    public class SearchPattern
    {
        public string Regex { get; set; }
        public bool ContainsSingleQuotes { get; set; }
    }
}