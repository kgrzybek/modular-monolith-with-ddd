using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CompanyName.MyMeetings.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)

                .UseStartup<Startup>()
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json");
                });
    }
}
