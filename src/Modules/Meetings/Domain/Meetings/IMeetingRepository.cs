namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public interface IMeetingRepository
    {
        Task AddAsync(Meeting meeting);

        Task<Meeting> GetByIdAsync(MeetingId id);
    }
}