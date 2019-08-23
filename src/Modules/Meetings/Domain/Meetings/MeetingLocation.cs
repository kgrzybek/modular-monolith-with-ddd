using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingLocation : ValueObject
    {
        public MeetingLocation(string name, string address, string postalCode, string city)
        {
            Name = name;
            Address = address;
            PostalCode = postalCode;
            City = city;
        }

        public string Name { get; }

        public string Address { get; }

        public string PostalCode { get; }

        public string City { get; }
    }
}