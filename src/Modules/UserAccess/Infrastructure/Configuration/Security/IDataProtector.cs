namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Security
{
    public interface IDataProtector
    {
        string Encrypt(string plainText);

        string Decrypt(string encryptedText);
    }
}