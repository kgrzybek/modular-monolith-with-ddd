using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentReply;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Meetings.MeetingComments
{
    [Route("api/meetings/[controller]")]
    [ApiController]
    public class MeetingCommentsController : ControllerBase
    {
        private readonly IMeetingsModule _meetingModule;

        public MeetingCommentsController(IMeetingsModule meetingModule)
        {
            _meetingModule = meetingModule;
        }

        [HttpPost]
        [HasPermission(MeetingsPermissions.AddMeetingComment)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddComment([FromBody] AddMeetingCommentRequest request)
        {
            var commentId =
                await _meetingModule.ExecuteCommandAsync(new AddMeetingCommentCommand(
                    request.MeetingId,
                    request.Comment));

            return Ok(commentId);
        }

        [HttpPut("{meetingCommentId}")]
        [HasPermission(MeetingsPermissions.EditMeetingComment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditComment(
            [FromRoute] Guid meetingCommentId,
            [FromBody] EditMeetingCommentRequest request)
        {
            await _meetingModule.ExecuteCommandAsync(new EditMeetingCommentCommand(
                meetingCommentId,
                request.EditedComment));

            return Ok();
        }

        [HttpDelete("{meetingCommentId}")]
        [HasPermission(MeetingsPermissions.RemoveMeetingComment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid meetingCommentId, [FromQuery] string reason)
        {
            await _meetingModule.ExecuteCommandAsync(
                new RemoveMeetingCommentCommand(meetingCommentId, reason));

            return Ok();
        }

        [HttpPost("{meetingCommentId}/replies")]
        [HasPermission(MeetingsPermissions.AddMeetingCommentReply)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddReply([FromRoute] Guid meetingCommentId, [FromBody] string reply)
        {
            await _meetingModule.ExecuteCommandAsync(new AddReplyToMeetingCommentCommand(meetingCommentId, reply));

            return Ok();
        }

        [HttpPost("{meetingCommentId}/likes")]
        [HasPermission(MeetingsPermissions.LikeMeetingComment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LikeComment([FromRoute] Guid meetingCommentId)
        {
            await _meetingModule.ExecuteCommandAsync(
                new AddMeetingCommentLikeCommand(meetingCommentId));

            return Ok();
        }

        [HttpDelete("{meetingCommentId}/likes")]
        [HasPermission(MeetingsPermissions.UnlikeMeetingComment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UnlikeComment([FromRoute] Guid meetingCommentId)
        {
            await _meetingModule.ExecuteCommandAsync(
                new RemoveMeetingCommentLikeCommand(meetingCommentId));

            return Ok();
        }
    }
}
