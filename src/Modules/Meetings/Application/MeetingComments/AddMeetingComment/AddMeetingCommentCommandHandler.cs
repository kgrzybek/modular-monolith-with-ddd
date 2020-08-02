using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment
{
    internal class AddMeetingCommentCommandHandler : ICommandHandler<AddMeetingCommentCommand, Guid>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingCommentRepository _meetingCommentRepository;
        private readonly IMemberContext _memberContext;

        public AddMeetingCommentCommandHandler(IMeetingRepository meetingRepository, IMeetingCommentRepository meetingCommentRepository, IMemberContext memberContext)
        {
            _meetingRepository = meetingRepository;
            _memberContext = memberContext;
            _meetingCommentRepository = meetingCommentRepository;
        }
        
        public async Task<Guid> Handle(AddMeetingCommentCommand command, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(command.MeetingId));
            if (meeting == null)
            {
                throw new InvalidCommandException(new List<string>{"Meeting for adding comment must exist."});
            }

            var meetingComment = meeting.AddComment(_memberContext.MemberId, command.Comment);
            await _meetingCommentRepository.AddAsync(meetingComment);
            
            return meetingComment.Id.Value;
        }
    }
}