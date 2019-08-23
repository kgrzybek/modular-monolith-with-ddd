using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups
{
    public class MeetingGroupLocation : ValueObject
    {
        public MeetingGroupLocation(string city, string countryCode)
        {
            City = city;
            CountryCode = countryCode;
        }

        public string City { get; }

        public string CountryCode { get; }
    }
}