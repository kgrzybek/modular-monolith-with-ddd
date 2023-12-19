namespace CompanyName.MyMeetings.Modules.UserAccessIS.Infrastructure.Configuration.Security
{
    public interface IDataProtector
    {
        string Encrypt(string plainText);

        string Decrypt(string encryptedText);
    }
}