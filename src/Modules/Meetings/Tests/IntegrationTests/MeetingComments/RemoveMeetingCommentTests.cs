﻿using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using static CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings.MeetingHelper;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingComments
{
    public class RemoveMeetingCommentTests : TestBase
    {
        [Test]
        public async Task RemoveMeetingComment_ByAuthor_WhenDataIsValid_IsSuccessful()
        {
            //Arrange
            var meetingId = await CreateMeetingAsync(MeetingsModule, ExecutionContext);
            var meetingCommentId = await MeetingsModule.ExecuteCommandAsync(new AddMeetingCommentCommand(meetingId, "The meeting was great."));
            
            // Act
            await MeetingsModule.ExecuteCommandAsync(new RemoveMeetingCommentCommand(meetingCommentId, reason: string.Empty));
            
            //Assert
            var meetingComments = await MeetingsModule.ExecuteQueryAsync(new GetMeetingCommentsQuery(meetingId));
            Assert.IsEmpty(meetingComments);
        }
        
        [Test]
        public void RemoveMeetingComment_WhenItIsNonexistent_ThrowsInvalidCommandException()
        {
            // Assert
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                // Act
                await MeetingsModule.ExecuteCommandAsync(new RemoveMeetingCommentCommand(
                    meetingCommentId: Guid.NewGuid(), 
                    reason: String.Empty));
            });
        }
    }
}