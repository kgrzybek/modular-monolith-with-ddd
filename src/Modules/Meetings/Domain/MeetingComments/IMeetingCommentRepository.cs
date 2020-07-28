using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments
{
    public interface IMeetingCommentRepository
    {
        Task AddAsync(MeetingComment meetingComment);
    }
}