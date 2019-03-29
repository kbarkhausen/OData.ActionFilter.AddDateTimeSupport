using Microsoft.VisualStudio.TestTools.UnitTesting;
using OData.ActionFilter.AddDateTimeSupport;
using System;
using System.Linq;

namespace Cmc.Nexus.Web.Tests.Services
{
    [TestClass]
    public class DateTimeUriParserUnitTests
    {
        private DateTimeUriParser _sut;

        [TestInitialize]
        public void Init()
        {
            _sut = new DateTimeUriParser();
        }    

        [TestMethod, TestCategory("Unit Tests")]
        public void Parse_StringWithDate_FoundOneMatch()
        {
            var input = "Examples:  '2-1-1900' '3/4/2018' '12.31.2012'.";

            var result = _sut.Parse(input);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod, TestCategory("Unit Tests")]
        public void Parse_StringWithDate2_FoundOneMatch()
        {
            //var input = "Examples:  '1900-12-31' .";
            var input = "Examples:  1900-12-31  .";
            //var input = "Examples:  '12-31-1900' .";

            var result = _sut.Parse(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            var firstItem = result.FirstOrDefault();
            Assert.AreEqual("1900-12-31", firstItem.RegexMatch);
            Assert.AreEqual(DateTime.Parse("12/31/1900"), firstItem.DateTimeValue);
        }            
    }
}