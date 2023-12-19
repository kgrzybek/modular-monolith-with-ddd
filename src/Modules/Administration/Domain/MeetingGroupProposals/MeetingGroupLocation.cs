using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    /// <summary>
    /// Represents the location of a meeting group.
    /// </summary>
    public class MeetingGroupLocation : ValueObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingGroupLocation"/> class.
        /// </summary>
        private MeetingGroupLocation(string city, string countryCode)
        {
            City = city;
            CountryCode = countryCode;
        }

        /// <summary>
        /// Gets the city of the meeting group location.
        /// </summary>
        public string City { get; }

        /// <summary>
        /// Gets the country code of the meeting group location.
        /// </summary>
        public string CountryCode { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="MeetingGroupLocation"/> class.
        /// </summary>
        /// <param name="city">The city of the meeting group location.</param>
        /// <param name="countryCode">The country code of the meeting group location.</param>
        /// <returns>A new instance of the <see cref="MeetingGroupLocation"/> class.</returns>
        public static MeetingGroupLocation Create(string city, string countryCode)
        {
            return new MeetingGroupLocation(city, countryCode);
        }
    }
}