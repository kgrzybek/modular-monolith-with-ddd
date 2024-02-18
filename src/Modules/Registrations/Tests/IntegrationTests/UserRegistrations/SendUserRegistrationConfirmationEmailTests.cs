using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;
using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using CompanyNames.MyMeetings.Modules.Registrations.IntegrationTests.SeedWork;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace CompanyNames.MyMeetings.Modules.Registrations.IntegrationTests.UserRegistrations
{
    [TestFixture]
    public class SendUserRegistrationConfirmationEmailTests : TestBase
    {
        [Test]
        public async Task SendUserRegistrationConfirmationEmail_Test()
        {
            var registrationId = Guid.NewGuid();

            var confirmLink = "confirmLink/";
            await RegistrationsModule.ExecuteCommandAsync(new SendUserRegistrationConfirmationEmailCommand(
                Guid.NewGuid(),
                new UserRegistrationId(registrationId),
                UserRegistrationSampleData.Email,
                confirmLink));
            string link = $"<a href=\"{confirmLink}{registrationId}\">link</a>";
            var content = $"Welcome to MyMeetings application! Please confirm your registration using this {link}.";
            var email = new EmailMessage(
                UserRegistrationSampleData.Email,
                "MyMeetings - Please confirm your registration",
                content);

            await EmailSender.Received(Quantity.Exactly(1)).SendEmail(email);
        }
    }
}