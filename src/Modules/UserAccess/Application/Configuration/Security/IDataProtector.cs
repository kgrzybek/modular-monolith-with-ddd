namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Security
{
    public interface IDataProtector
    {
        string Encrypt(string plainText);

        string Decrypt(string encryptedText);
    }
}