using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddCommentReply
{
    public class AddCommentReplyCommandHandler : ICommandHandler<AddCommentReplyCommand, Guid>
    {
        private readonly IMeetingCommentRepository _meetingCommentRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingGroupRepository _meetingGroupRepository;
        private readonly IMeetingCommentingConfigurationRepository _meetingCommentingConfigurationRepository;
        private readonly IMemberContext _memberContext;

        public AddCommentReplyCommandHandler(IMeetingCommentRepository meetingCommentRepository, IMeetingRepository meetingRepository, IMeetingGroupRepository meetingGroupRepository, IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository, IMemberContext memberContext)
        {
            _meetingCommentRepository = meetingCommentRepository;
            _meetingRepository = meetingRepository;
            _meetingGroupRepository = meetingGroupRepository;
            _meetingCommentingConfigurationRepository = meetingCommentingConfigurationRepository;
            _memberContext = memberContext;
        }

        public async Task<Guid> Handle(AddCommentReplyCommand command, CancellationToken cancellationToken)
        {
            var meetingComment = await _meetingCommentRepository.GetByIdAsync(new MeetingCommentId(command.InReplyToCommentId));
            if (meetingComment == null)
            {
                throw new InvalidCommandException(new List<string> { "To create reply the commit must exist." });
            }

            var meeting = await _meetingRepository.GetByIdAsync(meetingComment.GetMeetingId());

            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(meeting.GetMeetingGroupId());

            var meetingCommentingConfiguration = await _meetingCommentingConfigurationRepository.GetByMeetingIdAsync(meetingComment.GetMeetingId());

            var commentReply = meetingComment.Reply(_memberContext.MemberId, command.Reply, meetingGroup, meetingCommentingConfiguration);
            await _meetingCommentRepository.AddAsync(commentReply);

            return commentReply.Id.Value;
        }
    }
}