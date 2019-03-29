using Microsoft.VisualStudio.TestTools.UnitTesting;
using OData.ActionFilter.AddDateTimeSupport;
using System;

namespace OData.ActionFilter.AddDateTimeSupport.Tests
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
        public void UpdateRequestUri_ContainsTimeOffSetInFilter_ReturnsSameUri()
        {
            var input = "http://localhost:10121/odata/People()?$filter=BirthDate gt 1968-01-19T00:00:00Z&$orderby=LastName";
            var output = "http://localhost:10121/odata/People()?$filter=BirthDate gt 1968-01-19T00:00:00Z&$orderby=LastName";

            var result = _sut.UpdateRequestUri(new Uri(input));

            // Assert
            Assert.AreEqual(output, Uri.UnescapeDataString(result.AbsoluteUri));
        }

        [TestMethod, TestCategory("Unit Tests")]
        public void UpdateRequestUri_ContainsTimeOffSetInFilter2_ReturnsSameUri()
        {
            var input = "http://localhost:32189/odata/Students?$filter=EnrollmentDate%20gt%202019-02-17T00:00:00Z";
            var output = "http://localhost:32189/odata/Students?$filter=EnrollmentDate gt 2019-02-17T00:00:00Z";

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

        [TestMethod, TestCategory("Unit Tests")]
        public void UpdateRequestUri_ComplexUrlWithVariousDates_ReturnsUpdatedUri()
        {
            var input = "http://localhost/Cmc.Nexus.Web/ds/campusnexus/TaskGridDetails?$select=Id,Subject,TaskDueDate,Priority,CreatedDateTime,ReassignedDate,LastModifiedDateTime,TaskStartDate,TimeZone,StudentPhoneNumber,ProgramCode&$expand=Student($select=FirstName,LastName,FullName,State,Id;$expand=Campus($select=Name)),AssignedtoStaff($select=Id;$expand=Person($select=FirstName,LastName,FullName,Id)),TaskTemplate($select=Name),ProspectInquiry($select=StudentId;$expand=AdmissionsRegion($select=Name),LeadSource($select=Name),Program($select=Name)),PreviouslyAssignedtoStaff($select=Id;$expand=Person($select=FirstName,LastName,FullName)),TaskStatus($select=Name,Code),TaskType($select=Code,Name),Employer($select=Name,PhoneNumber),EmployerContact($select=PhoneNumber;$expand=Person($select=FullName)),SyOrganization($select=Name),SyOrganizationContact($expand=Person($select=FullName)),LastModifiedUser($select=StaffName),StudentEnrollmentPeriod($select=ProgramVersionName)&$format=json&$top=50&$orderby=TaskDueDate desc&$count=true&$filter=(((TaskStatus/Code ne 'C' and TaskStatus/Code ne 'X') and (TaskType/Code ne 'INCIDENT'))) and ((AssignedToStaffId eq 2) and (TaskDueDate ge 2017-03-28 and TaskDueDate lt 2199-12-31))&$orderby=LastName";
            var output = "http://localhost/Cmc.Nexus.Web/ds/campusnexus/TaskGridDetails?$select=Id,Subject,TaskDueDate,Priority,CreatedDateTime,ReassignedDate,LastModifiedDateTime,TaskStartDate,TimeZone,StudentPhoneNumber,ProgramCode&$expand=Student($select=FirstName,LastName,FullName,State,Id;$expand=Campus($select=Name)),AssignedtoStaff($select=Id;$expand=Person($select=FirstName,LastName,FullName,Id)),TaskTemplate($select=Name),ProspectInquiry($select=StudentId;$expand=AdmissionsRegion($select=Name),LeadSource($select=Name),Program($select=Name)),PreviouslyAssignedtoStaff($select=Id;$expand=Person($select=FirstName,LastName,FullName)),TaskStatus($select=Name,Code),TaskType($select=Code,Name),Employer($select=Name,PhoneNumber),EmployerContact($select=PhoneNumber;$expand=Person($select=FullName)),SyOrganization($select=Name),SyOrganizationContact($expand=Person($select=FullName)),LastModifiedUser($select=StaffName),StudentEnrollmentPeriod($select=ProgramVersionName)&$format=json&$top=50&$orderby=TaskDueDate desc&$count=true&$filter=(((TaskStatus/Code ne 'C' and TaskStatus/Code ne 'X') and (TaskType/Code ne 'INCIDENT'))) and ((AssignedToStaffId eq 2) and (TaskDueDate ge 2017-03-28T00:00:00Z and TaskDueDate lt 2199-12-31T00:00:00Z))&$orderby=LastName";

            var result = _sut.UpdateRequestUri(new Uri(input));

            // Assert
            Assert.AreEqual(output, Uri.UnescapeDataString(result.AbsoluteUri));
        }

        
    }
}