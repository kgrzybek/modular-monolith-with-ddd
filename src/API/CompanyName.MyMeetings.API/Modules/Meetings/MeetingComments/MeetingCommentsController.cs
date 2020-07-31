﻿using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment;
using Microsoft.AspNetCore.Http;
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
            var commentId = await _meetingModule.ExecuteCommandAsync(new AddMeetingCommentCommand(request.MeetingId, request.Comment));

            return Ok(commentId);
        }

        [HttpPut("{meetingCommentId}")]
        [HasPermission(MeetingsPermissions.EditMeetingComment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditComment([FromRoute] Guid meetingCommentId, [FromBody] EditMeetingCommentRequest request)
        {
            await _meetingModule.ExecuteCommandAsync(new EditMeetingCommentCommand(meetingCommentId, request.EditedComment));

            return Ok();
        }
    }
}