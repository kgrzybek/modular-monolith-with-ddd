using CompanyName.MyMeetings.Modules.UserAccess.Domain.UnitTests.SeedWork;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Events;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Rules;
using NSubstitute;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.UnitTests.UserRegistrations
{
    [TestFixture]
    public class UserRegistrationTests : TestBase
    {
        [Test]
        public void NewUserRegistration_WithUniqueLogin_IsSuccessful()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var userRegistration =
                UserRegistration.RegisterNewUser(
                    "login", "password", "test@email", 
                    "firstName", "lastName", usersCounter);

            var newUserRegisteredDomainEvent = GetPublishedDomainEvent<NewUserRegisteredDomainEvent>(userRegistration);

            Assert.That(newUserRegisteredDomainEvent.UserRegistrationId, Is.EqualTo(userRegistration.Id));
        }

        [Test]
        public void NewUserRegistration_WithoutUniqueLogin_BreaksUserLoginMustBeUniqueRule()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            usersCounter.CountUsersWithLogin("login").Returns(x => 1);

            AssertBrokenRule<UserLoginMustBeUniqueRule>(() =>
            {
                UserRegistration.RegisterNewUser(
                    "login", "password", "test@email",
                    "firstName", "lastName", usersCounter);
            });
        }

        [Test]
        public void ConfirmingUserRegistration_WhenWaitingForConfirmation_IsSuccessful()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                    "login", "password", "test@email",
                    "firstName", "lastName", usersCounter);

            registration.Confirm();

            var userRegistrationConfirmedDomainEvent = GetPublishedDomainEvent<UserRegistrationConfirmedDomainEvent>(registration);

            Assert.That(userRegistrationConfirmedDomainEvent.UserRegistrationId, Is.EqualTo(registration.Id));
        }

        [Test]
        public void UserRegistration_WhenIsConfirmed_CannotBeConfirmedAgain()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login", "password", "test@email",
                "firstName", "lastName", usersCounter);

            registration.Confirm();
            
            AssertBrokenRule<UserRegistrationCannotBeConfirmedMoreThanOnceRule>(() =>
            {
                registration.Confirm();
            });
        }

        [Test]
        public void UserRegistration_WhenIsExpired_CannotBeConfirmed()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login", "password", "test@email",
                "firstName", "lastName", usersCounter);

            registration.Expire();
            
            AssertBrokenRule<UserRegistrationCannotBeConfirmedAfterExpirationRule>(() =>
            {
                registration.Confirm();
            });
        }

        [Test]
        public void ExpiringUserRegistration_WhenWaitingForConfirmation_IsSuccessful()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login", "password", "test@email",
                "firstName", "lastName", usersCounter);

            registration.Expire();

            var userRegistrationExpired = GetPublishedDomainEvent<UserRegistrationExpiredDomainEvent>(registration);

            Assert.That(userRegistrationExpired.UserRegistrationId, Is.EqualTo(registration.Id));          
        }

        [Test]
        public void UserRegistration_WhenIsExpired_CannotBeExpiredAgain()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login", "password", "test@email",
                "firstName", "lastName", usersCounter);

            registration.Expire();

            AssertBrokenRule<UserRegistrationCannotBeExpiredMoreThanOnceRule>(() =>
            {
                registration.Expire();
            });       
        }
    }
}