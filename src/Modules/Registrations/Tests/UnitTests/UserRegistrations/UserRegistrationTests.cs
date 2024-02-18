using CompanyName.MyMeetings.Modules.Registrations.Domain.UnitTests.SeedWork;
using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations.Events;
using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations.Rules;
using NSubstitute;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Registrations.Domain.UnitTests.UserRegistrations
{
    [TestFixture]
    public class UserRegistrationTests : TestBase
    {
        [Test]
        public void NewUserRegistration_WithUniqueLogin_IsSuccessful()
        {
            // Arrange
            var usersCounter = Substitute.For<IUsersCounter>();

            // Act
            var userRegistration =
                UserRegistration.RegisterNewUser(
                    "login",
                    "password",
                    "test@email",
                    "firstName",
                    "lastName",
                    usersCounter,
                    "confirmLink");

            // Assert
            var newUserRegisteredDomainEvent =
                AssertPublishedDomainEvent<NewUserRegisteredDomainEvent>(userRegistration);
            Assert.That(newUserRegisteredDomainEvent.UserRegistrationId, Is.EqualTo(userRegistration.Id));
        }

        [Test]
        public void NewUserRegistration_WithoutUniqueLogin_BreaksUserLoginMustBeUniqueRule()
        {
            // Arrange
            var usersCounter = Substitute.For<IUsersCounter>();
            usersCounter.CountUsersWithLogin("login").Returns(x => 1);

            // Assert
            AssertBrokenRule<UserLoginMustBeUniqueRule>(() =>
            {
                // Act
                UserRegistration.RegisterNewUser(
                    "login",
                    "password",
                    "test@email",
                    "firstName",
                    "lastName",
                    usersCounter,
                    "confirmLink");
            });
        }

        [Test]
        public void ConfirmingUserRegistration_WhenWaitingForConfirmation_IsSuccessful()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login",
                "password",
                "test@email",
                "firstName",
                "lastName",
                usersCounter,
                "confirmLink");

            registration.Confirm();

            var userRegistrationConfirmedDomainEvent =
                AssertPublishedDomainEvent<UserRegistrationConfirmedDomainEvent>(registration);

            Assert.That(userRegistrationConfirmedDomainEvent.UserRegistrationId, Is.EqualTo(registration.Id));
        }

        [Test]
        public void UserRegistration_WhenIsConfirmed_CannotBeConfirmedAgain()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login",
                "password",
                "test@email",
                "firstName",
                "lastName",
                usersCounter,
                "confirmLink");

            registration.Confirm();

            AssertBrokenRule<UserRegistrationCannotBeConfirmedMoreThanOnceRule>(() => { registration.Confirm(); });
        }

        [Test]
        public void UserRegistration_WhenIsExpired_CannotBeConfirmed()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login",
                "password",
                "test@email",
                "firstName",
                "lastName",
                usersCounter,
                "confirmLink");

            registration.Expire();

            AssertBrokenRule<UserRegistrationCannotBeConfirmedAfterExpirationRule>(() => { registration.Confirm(); });
        }

        [Test]
        public void ExpiringUserRegistration_WhenWaitingForConfirmation_IsSuccessful()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login",
                "password",
                "test@email",
                "firstName",
                "lastName",
                usersCounter,
                "confirmLink");

            registration.Expire();

            var userRegistrationExpired = AssertPublishedDomainEvent<UserRegistrationExpiredDomainEvent>(registration);

            Assert.That(userRegistrationExpired.UserRegistrationId, Is.EqualTo(registration.Id));
        }

        [Test]
        public void UserRegistration_WhenIsExpired_CannotBeExpiredAgain()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            var registration = UserRegistration.RegisterNewUser(
                "login",
                "password",
                "test@email",
                "firstName",
                "lastName",
                usersCounter,
                "confirmLink");

            registration.Expire();

            AssertBrokenRule<UserRegistrationCannotBeExpiredMoreThanOnceRule>(() => { registration.Expire(); });
        }

        // [Test]
        // public void CreateUser_WhenRegistrationIsConfirmed_IsSuccessful()
        // {
        //     var usersCounter = Substitute.For<IUsersCounter>();
        //
        //     var registration = UserRegistration.RegisterNewUser(
        //         "login",
        //         "password",
        //         "test@email",
        //         "firstName",
        //         "lastName",
        //         usersCounter,
        //         "confirmLink");
        //
        //     registration.Confirm();
        //
        //     var user = registration.CreateUser();
        //
        //     var userCreated = AssertPublishedDomainEvent<UserCreatedDomainEvent>(user);
        //
        //     Assert.That(user.Id, Is.EqualTo(registration.Id));
        //     Assert.That(userCreated.Id, Is.EqualTo(registration.Id));
        //// }

        // [Test]
        // public void UserCreation_WhenRegistrationIsNotConfirmed_IsNotPossible()
        // {
        //     var usersCounter = Substitute.For<IUsersCounter>();
        //
        //     var registration = UserRegistration.RegisterNewUser(
        //         "login",
        //         "password",
        //         "test@email",
        //         "firstName",
        //         "lastName",
        //         usersCounter,
        //         "confirmLink");
        //
        //     AssertBrokenRule<UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule>(
        //         () => { registration.CreateUser(); });
        // }
    }
}