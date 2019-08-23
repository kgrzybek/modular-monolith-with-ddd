using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    internal class AttendeeCanBeAddedOnlyInRsvpTermRule : IBusinessRule
    {
        private readonly Term _rsvpTerm;
        internal AttendeeCanBeAddedOnlyInRsvpTermRule(Term rsvpTerm)
        {
            _rsvpTerm = rsvpTerm;
        }

        public bool IsBroken() => !_rsvpTerm.IsInTerm(DateTime.UtcNow);

        public string Message => "Attendee can be added only in RSVP term";
    }
}