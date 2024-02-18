using System.Data;
using Dapper;

namespace CompanyName.MyMeetings.SUT.SeedWork
{
    internal static class DatabaseCleaner
    {
        internal static async Task ClearAllData(IDbConnection connection)
        {
            await ClearAdministration(connection);

            await ClearApp(connection);

            await ClearMeetings(connection);

            await ClearPayments(connection);

            await ClearUsers(connection);

            await ClearRegistration(connection);
        }

        private static async Task ClearUsers(IDbConnection connection)
        {
            var clearUsersSql =
                "DELETE FROM [users].[InboxMessages] " +
                "DELETE FROM [users].[InternalCommands] " +
                "DELETE FROM [users].[OutboxMessages] " +
                "DELETE FROM [users].[Permissions] " +
                "DELETE FROM [users].[RolesToPermissions] " +
                "DELETE FROM [users].[UserRoles] " +
                "DELETE FROM [users].[Users] ";

            await connection.ExecuteScalarAsync(clearUsersSql);
        }

        private static async Task ClearPayments(IDbConnection connection)
        {
            var clearPaymentsSql =
                "DELETE FROM [payments].[InboxMessages] " +
                "DELETE FROM [payments].[InternalCommands] " +
                "DELETE FROM [payments].[MeetingFees] " +
                "DELETE FROM [payments].[Messages] " +
                "DELETE FROM [payments].[OutboxMessages] " +
                "DELETE FROM [payments].[Payers] " +
                "DELETE FROM [payments].[PriceListItems] " +
                "DELETE FROM [payments].[Streams] " +
                "DELETE FROM [payments].[SubscriptionCheckpoints] " +
                "DELETE FROM [payments].[SubscriptionDetails] " +
                "DELETE FROM [payments].[SubscriptionPayments] ";

            await connection.ExecuteScalarAsync(clearPaymentsSql);
        }

        private static async Task ClearMeetings(IDbConnection connection)
        {
            var clearMeetingsSql =
                "DELETE FROM [meetings].[Countries] " +
                "DELETE FROM [meetings].[InboxMessages] " +
                "DELETE FROM [meetings].[InternalCommands] " +
                "DELETE FROM [meetings].[MeetingAttendees] " +
                "DELETE FROM [meetings].[MeetingCommentingConfigurations] " +
                "DELETE FROM [meetings].[MeetingComments] " +
                "DELETE FROM [meetings].[MeetingGroupMembers] " +
                "DELETE FROM [meetings].[MeetingGroupProposals] " +
                "DELETE FROM [meetings].[MeetingGroups] " +
                "DELETE FROM [meetings].[MeetingMemberCommentLikes] " +
                "DELETE FROM [meetings].[MeetingNotAttendees] " +
                "DELETE FROM [meetings].[Meetings] " +
                "DELETE FROM [meetings].[MeetingWaitlistMembers] " +
                "DELETE FROM [meetings].[Members] " +
                "DELETE FROM [meetings].[MemberSubscriptions] " +
                "DELETE FROM [meetings].[OutboxMessages]";

            await connection.ExecuteScalarAsync(clearMeetingsSql);
        }

        private static async Task ClearApp(IDbConnection connection)
        {
            var clearAppSql =
                "DELETE FROM [app].[Emails] ";
            await connection.ExecuteScalarAsync(clearAppSql);
        }

        private static async Task ClearAdministration(IDbConnection connection)
        {
            var clearAdministrationSql =
                "DELETE FROM [administration].[InboxMessages] " +
                "DELETE FROM [administration].[InternalCommands] " +
                "DELETE FROM [administration].[MeetingGroupProposals] " +
                "DELETE FROM [administration].[Members] " +
                "DELETE FROM [administration].[OutboxMessages] ";

            await connection.ExecuteScalarAsync(clearAdministrationSql);
        }

        private static async Task ClearRegistration(IDbConnection connection)
        {
            const string sql = "DELETE FROM [registrations].[InboxMessages] " +
                               "DELETE FROM [registrations].[InternalCommands] " +
                               "DELETE FROM [registrations].[OutboxMessages] " +
                               "DELETE FROM [registrations].[UserRegistrations] ";

            await connection.ExecuteScalarAsync(sql);
        }
    }
}