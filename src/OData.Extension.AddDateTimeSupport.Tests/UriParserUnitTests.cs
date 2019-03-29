using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cmc.Nexus.Web.Tests.Services
{
    [TestClass]
    public class UriParserUnitTests
    {
        private OData.ActionFilter.AddDateTimeSupport.ODataUrlParser _sut;

        [TestInitialize]
        public void Init()
        {
            _sut = new OData.ActionFilter.AddDateTimeSupport.ODataUrlParser();
        }

        [TestMethod, TestCategory("Unit Tests")]
        public void Parse_OnlyFilterOptionInQuery_FindsFilterText()
        {
            var input = "http://localhost:10121/odata/People()?$filter=FirstName eq 'Ken'";

            var result = _sut.ExtractFilterQuery(input);

            // Assert
            Assert.AreEqual("$filter=FirstName eq 'Ken'", result.StringValue);
        }

        [TestMethod, TestCategory("Unit Tests")]
        public void Parse_MultipleOptionsInQuery_FindsFilterText()
        {
            var input = "http://localhost:10121/odata/People()?$filter=FirstName eq 'Ken'&$orderby=LastName";

            var result = _sut.ExtractFilterQuery(input);

            // Assert
            Assert.AreEqual("$filter=FirstName eq 'Ken'", result.StringValue);
        }
    }
}