using CompanyName.MyMeetings.Modules.Administration.Application.Members.CreateMember;
using CompanyName.MyMeetings.Modules.Administration.Application.Members.GetMember;
using CompanyName.MyMeetings.Modules.Administration.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Administration.IntegrationTests.Members
{
    [TestFixture]
    public class CreateMemberTests : TestBase
    {
        [Test]
        public async Task CreateMember_Test()
        {
            var memberId = await AdministrationModule.ExecuteCommandAsync(new CreateMemberCommand(
                Guid.NewGuid(),
                MemberSampleData.MemberId,
                MemberSampleData.Login,
                MemberSampleData.Email,
                MemberSampleData.FirstName,
                MemberSampleData.LastName,
                MemberSampleData.Name));

            var member = await AdministrationModule.ExecuteQueryAsync(new GetMemberQuery(memberId));

            Assert.That(member.Id, Is.EqualTo(MemberSampleData.MemberId));
            Assert.That(member.LastName, Is.EqualTo(MemberSampleData.LastName));
            Assert.That(member.Login, Is.EqualTo(MemberSampleData.Login));
            Assert.That(member.Email, Is.EqualTo(MemberSampleData.Email));
            Assert.That(member.FirstName, Is.EqualTo(MemberSampleData.FirstName));
            Assert.That(member.Name, Is.EqualTo(MemberSampleData.Name));
        }
    }
}