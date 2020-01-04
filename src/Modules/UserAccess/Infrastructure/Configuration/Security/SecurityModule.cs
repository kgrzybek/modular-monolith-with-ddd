using Autofac;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Security
{
    internal class SecurityModule : Module
    {
        private readonly string _encryptionKey;

        public SecurityModule(string encryptionKey)
        {
            _encryptionKey = encryptionKey;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AesDataProtector>()
                .As<IDataProtector>()
                .WithParameter("encryptionKey", _encryptionKey);
        }
    }
}