using CompanyName.MyMeetings.Modules.Administration.Domain.Members;
using CompanyName.MyMeetings.Modules.Administration.Domain.Members.Events;
using CompanyName.MyMeetings.Modules.Administration.Domain.UnitTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.UnitTests.Members
{
    [TestFixture]
    public class MemberTests : TestBase
    {
        [Test]
        public void CreateMember_IsSuccessful()
        {
            MemberId memberId = new MemberId(Guid.NewGuid());
            var member = Member.Create(
                memberId.Value,
                "memberLogin",
                "memberEmail@mail.com",
                "John",
                "Doe",
                "John Doe");

            var memberCreated = AssertPublishedDomainEvent<MemberCreatedDomainEvent>(member);

            Assert.That(memberCreated.MemberId, Is.EqualTo(memberId));
        }
    }
}