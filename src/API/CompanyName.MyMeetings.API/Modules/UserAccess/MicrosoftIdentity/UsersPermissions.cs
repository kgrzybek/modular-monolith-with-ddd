namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity;

internal class UsersPermissions
{
    // Identity
    public const string GetUserAccounts = "Users.GetUserAccounts";

    // Users
    public const string GetUsers = "Users.GetUsers";
    public const string CreateUserAccount = "Users.CreateUserAccount";
    public const string UpdateUserAccount = "Users.UpdateUserAccount";
    public const string UnlockUserAccount = "Users.UnlockUserAccount";
    public const string ConfirmEmailAddress = "Users.ConfirmEmailAddress";
    public const string GetAuthenticatorKey = "Users.GetAuthenticatorKey";
    public const string RegisterAuthenticator = "Users.RegisterAuthenticator";
    public const string GetUserRoles = "Users.GetUserRoles";
    public const string SetUserRoles = "Users.SetUserRoles";
    public const string GetUserPermissions = "Users.GetUserPermissions";
    public const string SetUserPermissions = "Users.SetUserPermissions";

    // Roles
    public const string GetRoles = "Users.GetRoles";
    public const string AddRole = "Users.AddRole";
    public const string RenameRole = "Users.RenameRole";
    public const string DeleteRole = "Users.DeleteRole";
    public const string GetRolePermissions = "Users.GetRolePermissions";
    public const string SetRolePermissions = "Users.SetRolePermissions";
}