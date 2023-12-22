using CompanyName.MyMeetings.Modules.Meetings.Application.Countries;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Countries
{
    [TestFixture]
    public class GetCountriesTests : TestBase
    {
        [Test]
        public async Task GetCountriesTest()
        {
            // Arrange
            await ExecuteScript("Countries/0001_SeedCountries.sql");

            // Act
            var countries = await MeetingsModule.ExecuteQueryAsync(new GetAllCountriesQuery());

            // Assert
            Assert.That(countries, Is.Not.Empty);
            Assert.That(countries.Any(x => x.Code == "PL"));
        }
    }
}