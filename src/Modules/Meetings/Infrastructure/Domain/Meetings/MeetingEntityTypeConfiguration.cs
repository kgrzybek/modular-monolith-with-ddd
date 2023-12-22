using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.Meetings
{
    internal class MeetingEntityTypeConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.ToTable("Meetings", "meetings");

            builder.HasKey(x => x.Id);

            builder.Property<MeetingGroupId>("_meetingGroupId").HasColumnName("MeetingGroupId");
            builder.Property<string>("_title").HasColumnName("Title");
            builder.Property<string>("_description").HasColumnName("Description");
            builder.Property<MemberId>("_creatorId").HasColumnName("CreatorId");
            builder.Property<MemberId>("_changeMemberId").HasColumnName("ChangeMemberId");
            builder.Property<DateTime>("_createDate").HasColumnName("CreateDate");
            builder.Property<DateTime?>("_changeDate").HasColumnName("ChangeDate");
            builder.Property<DateTime?>("_cancelDate").HasColumnName("CancelDate");
            builder.Property<bool>("_isCanceled").HasColumnName("IsCanceled");
            builder.Property<MemberId>("_cancelMemberId").HasColumnName("CancelMemberId");

            builder.OwnsOne<MeetingTerm>("_term", b =>
            {
                b.Property(p => p.StartDate).HasColumnName("TermStartDate");
                b.Property(p => p.EndDate).HasColumnName("TermEndDate");
            });

            builder.OwnsOne<Term>("_rsvpTerm", b =>
            {
                b.Property(p => p.StartDate).HasColumnName("RSVPTermStartDate");
                b.Property(p => p.EndDate).HasColumnName("RSVPTermEndDate");
            });

            builder.OwnsOne<MoneyValue>("_eventFee", b =>
            {
                b.Property(p => p.Value).HasColumnName("EventFeeValue");
                b.Property(p => p.Currency).HasColumnName("EventFeeCurrency");
            });

            builder.OwnsOne<MeetingLocation>("_location", b =>
            {
                b.Property(p => p.Name).HasColumnName("LocationName");
                b.Property(p => p.Address).HasColumnName("LocationAddress");
                b.Property(p => p.PostalCode).HasColumnName("LocationPostalCode");
                b.Property(p => p.City).HasColumnName("LocationCity");
            });

            builder.OwnsMany<MeetingAttendee>("_attendees", y =>
            {
                y.WithOwner().HasForeignKey("MeetingId");
                y.ToTable("MeetingAttendees", "meetings");
                y.Property<MemberId>("AttendeeId");
                y.Property<MeetingId>("MeetingId");
                y.Property<DateTime>("_decisionDate").HasColumnName("DecisionDate");
                y.HasKey("AttendeeId", "MeetingId", "_decisionDate");
                y.Property<bool>("_decisionChanged").HasColumnName("DecisionChanged");
                y.Property<int>("_guestsNumber").HasColumnName("GuestsNumber");
                y.Property<DateTime?>("_decisionChangeDate").HasColumnName("DecisionChangeDate");
                y.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
                y.Property<string>("_removingReason").HasColumnName("RemovingReason");
                y.Property<MemberId>("_removingMemberId").HasColumnName("RemovingMemberId");
                y.Property<DateTime?>("_removedDate").HasColumnName("RemovedDate");
                y.Property<bool>("_isFeePaid").HasColumnName("IsFeePaid");

                y.OwnsOne<MeetingAttendeeRole>("_role", b =>
                {
                    b.Property(x => x.Value).HasColumnName("RoleCode");
                });

                y.OwnsOne<MoneyValue>("_fee", b =>
                {
                    b.Property(p => p.Value).HasColumnName("FeeValue");
                    b.Property(p => p.Currency).HasColumnName("FeeCurrency");
                });
            });

            builder.OwnsMany<MeetingNotAttendee>("_notAttendees", y =>
            {
                y.WithOwner().HasForeignKey("MeetingId");
                y.ToTable("MeetingNotAttendees", "meetings");
                y.Property<MemberId>("MemberId");
                y.Property<MeetingId>("MeetingId");
                y.Property<DateTime>("_decisionDate").HasColumnName("DecisionDate");
                y.HasKey("MemberId", "MeetingId", "_decisionDate");
                y.Property<bool>("_decisionChanged").HasColumnName("DecisionChanged");
                y.Property<DateTime?>("_decisionChangeDate").HasColumnName("DecisionChangeDate");
            });

            builder.OwnsMany<MeetingWaitlistMember>("_waitlistMembers", y =>
            {
                y.WithOwner().HasForeignKey("MeetingId");
                y.ToTable("MeetingWaitlistMembers", "meetings");
                y.Property<MemberId>("MemberId");
                y.Property<MeetingId>("MeetingId");
                y.Property<DateTime>("SignUpDate").HasColumnName("SignUpDate");
                y.HasKey("MemberId", "MeetingId", "SignUpDate");
                y.Property<bool>("_isSignedOff").HasColumnName("IsSignedOff");
                y.Property<DateTime?>("_signOffDate").HasColumnName("SignOffDate");

                y.Property<bool>("_isMovedToAttendees").HasColumnName("IsMovedToAttendees");
                y.Property<DateTime?>("_movedToAttendeesDate").HasColumnName("MovedToAttendeesDate");
            });

            builder.OwnsOne<MeetingLimits>("_meetingLimits", meetingLimits =>
            {
                meetingLimits.Property(x => x.AttendeesLimit).HasColumnName("AttendeesLimit");
                meetingLimits.Property(x => x.GuestsLimit).HasColumnName("GuestsLimit");
            });
        }
    }
}
