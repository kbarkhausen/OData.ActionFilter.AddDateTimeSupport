using Microsoft.VisualStudio.TestTools.UnitTesting;
using OData.ActionFilter.AddDateTimeSupport;
using System;

namespace Cmc.Nexus.Web.Tests.Services
{
    [TestClass]
    public class ODataDateTimeSupportServiceTests
    {
        private ODataDateTimeSupportService _sut;

        [TestInitialize]
        public void Init()
        {
            _sut = new ODataDateTimeSupportService();
        }        

        [TestMethod, TestCategory("Unit Tests")]
        public void UpdateRequestUri_NoDateInFilter_ReturnsSameUri()
        {
            var input = "http://localhost:10121/odata/People()?$filter=FirstName eq 'Ken'&$orderby=LastName";
            var output = "http://localhost:10121/odata/People()?$filter=FirstName eq 'Ken'&$orderby=LastName";

            var result = _sut.UpdateRequestUri(new Uri(input));

            // Assert
            Assert.AreEqual(output, Uri.UnescapeDataString(result.AbsoluteUri));
        }

        [TestMethod, TestCategory("Unit Tests")]
        public void UpdateRequestUri_ContainsDateValueInFilter_ReturnsSameUri()
        {
            var input = "http://localhost:10121/odata/People()?$filter=BirthDate gt '1/19/1968'&$orderby=LastName";
            var output = "http://localhost:10121/odata/People()?$filter=BirthDate gt 1968-01-19T00:00:00Z&$orderby=LastName";

            var result = _sut.UpdateRequestUri(new Uri(input));

            // Assert
            Assert.AreEqual(output, Uri.UnescapeDataString(result.AbsoluteUri));
        }

        [TestMethod, TestCategory("Unit Tests")]
        public void UpdateRequestUri_ContainsDateValue2InFilter_ReturnsSameUri()
        {
            var input = "http://localhost:10121/odata/People()?$filter=BirthDate gt 1968-1-19&$orderby=LastName";
            var output = "http://localhost:10121/odata/People()?$filter=BirthDate gt 1968-01-19T00:00:00Z&$orderby=LastName";

            var result = _sut.UpdateRequestUri(new Uri(input));

            // Assert
            Assert.AreEqual(output, Uri.UnescapeDataString(result.AbsoluteUri));
        }

        [TestMethod, TestCategory("Unit Tests")]
        public void UpdateRequestUri_ContainsDateValue3InFilter_ReturnsSameUri()
        {
            var input = "http://localhost:10121/odata/People()?$filter=BirthDate gt '1968-1-19'&$orderby=LastName";
            var output = "http://localhost:10121/odata/People()?$filter=BirthDate gt 1968-01-19T00:00:00Z&$orderby=LastName";

            var result = _sut.UpdateRequestUri(new Uri(input));

            // Assert
            Assert.AreEqual(output, Uri.UnescapeDataString(result.AbsoluteUri));
        }

        [TestMethod, TestCategory("Unit Tests")]
        public void UpdateRequestUri_ContainsNoFilter_ReturnsSameUri()
        {
            var input = "http://localhost:10121/odata/People()?$orderby=LastName";
            var output = "http://localhost:10121/odata/People()?$orderby=LastName";

            var result = _sut.UpdateRequestUri(new Uri(input));

            // Assert
            Assert.AreEqual(output, Uri.UnescapeDataString(result.AbsoluteUri));
        }

        [TestMethod, TestCategory("Unit Tests")]
        public void UpdateRequestUri_FilterContainsNoDateValues_ReturnsSameUri()
        {
            var input = "http://localhost:10121/odata/People()?$filter=FirstName eq 'Bob'&$orderby=LastName";
            var output = "http://localhost:10121/odata/People()?$filter=FirstName eq 'Bob'&$orderby=LastName";

            var result = _sut.UpdateRequestUri(new Uri(input));

            // Assert
            Assert.AreEqual(output, Uri.UnescapeDataString(result.AbsoluteUri));
        }
    }
}