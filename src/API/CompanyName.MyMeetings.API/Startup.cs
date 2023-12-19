using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text.Json;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.API.Configuration.ExecutionContext;
using CompanyName.MyMeetings.API.Configuration.Extensions;
using CompanyName.MyMeetings.API.Configuration.Validation;
using CompanyName.MyMeetings.API.Modules.Administration;
using CompanyName.MyMeetings.API.Modules.Meetings;
using CompanyName.MyMeetings.API.Modules.Payments;
using CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.Contracts.Results;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.IdentityServer;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration;
using Hellang.Middleware.ProblemDetails;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Formatting.Compact;
using ILogger = Serilog.ILogger;
using UserAccess = CompanyName.MyMeetings.API.Modules.UserAccess;
using UserAccessISConfiguration = CompanyName.MyMeetings.Modules.UserAccessIS.Infrastructure.Configuration;

namespace CompanyName.MyMeetings.API
{
    public class Startup
    {
        private const string MeetingsConnectionString = "MeetingsConnectionString";
        private static ILogger _logger;
        private static ILogger _loggerForApi;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env)
        {
            ConfigureLogger();

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables("Meetings_")
                .Build();

            _loggerForApi.Information("Connection string:" + _configuration["ConnectionStrings:" + MeetingsConnectionString]);

            AuthorizationChecker.CheckAllEndpoints();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerDocumentation();

            // ConfigureIdentityServer(services);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;

                    var userAccessConfiguration = _configuration.GetUserAccessConfiguration();
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = userAccessConfiguration.GetIssuerSigningKey(),
                        ValidIssuer = userAccessConfiguration.GetValidIssuer(),
                        ValidateIssuer = userAccessConfiguration.ShouldValidateIssuer(),
                        ValidAudience = userAccessConfiguration.GetValidAudience(),
                        ValidateAudience = userAccessConfiguration.ShouldValidateAudience(),
                        ValidateLifetime = true,
                        RequireExpirationTime = true,

                        // Clock skew compensates for server time drift.
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };

                    config.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.Response.ContentType = "application/json";
                            context.Response.OnStarting(() =>
                            {
                                var error = Errors.Authentication.NotAuthorized();
                                string result = JsonSerializer.Serialize(Result.Error(error.ToErrorMessages()));
                                return context.Response.WriteAsync(result);
                            });
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Append("Token-Expired", "true");
                            }

                            return Task.CompletedTask;
                        }
                    };
                })
                .AddCookie(IdentityConstants.ApplicationScheme)
                .AddCookie(IdentityConstants.TwoFactorUserIdScheme);
            /*
                Add additional external authentication providers (don't forget to reference the required packages)
                Package: Microsoft.AspNetCore.Authentication.Google
                Google portal (APIs und Services) for configuration: https://console.cloud.google.com/apis/library
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                });
             */

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
                {
                    policyBuilder.Requirements.Add(new HasPermissionAuthorizationRequirement());
                    policyBuilder.AddAuthenticationSchemes(IdentityServerAuthenticationDefaults.AuthenticationScheme);
                });
            });

            // services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new MeetingsAutofacModule());
            containerBuilder.RegisterModule(new AdministrationAutofacModule());
            containerBuilder.RegisterModule(new UserAccess.IdentityServer.UserAccessAutofacModule());
            containerBuilder.RegisterModule(new PaymentsAutofacModule());
            containerBuilder.RegisterModule(new UserAccess.MicrosoftIdentity.UserAccessAutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            InitializeModules(container);

            app.UseMiddleware<CorrelationMiddleware>();

            app.UseSwaggerDocumentation();

            // app.UseIdentityServer();
            if (env.IsDevelopment())
            {
                app.UseProblemDetails();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void ConfigureLogger()
        {
            _logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(new CompactJsonFormatter(), "logs/logs")
                .CreateLogger();

            _loggerForApi = _logger.ForContext("Module", "API");

            _loggerForApi.Information("Logger configured");
        }

        private void ConfigureIdentityServer(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
                .AddInMemoryApiResources(IdentityServerConfig.GetApis())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryPersistedGrants()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            services.AddTransient<IResourceOwnerPasswordValidator, UserAccess.IdentityServer.ResourceOwnerPasswordValidator>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, x =>
                {
                    x.Authority = "http://localhost:5000";
                    x.ApiName = "myMeetingsAPI";
                    x.RequireHttpsMetadata = false;
                });
        }

        private void InitializeModules(ILifetimeScope container)
        {
            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            var emailsConfiguration = new EmailsConfiguration(_configuration["EmailsConfiguration:FromEmail"]);
            var userAccessConfiguration = _configuration.GetUserAccessConfiguration();

            MeetingsStartup.Initialize(
                _configuration["ConnectionStrings:" + MeetingsConnectionString],
                executionContextAccessor,
                _logger,
                emailsConfiguration,
                null);

            AdministrationStartup.Initialize(
                _configuration["ConnectionStrings:" + MeetingsConnectionString],
                executionContextAccessor,
                _logger,
                null);

            UserAccessStartup.Initialize(
                _configuration["ConnectionStrings:" + MeetingsConnectionString],
                executionContextAccessor,
                _logger,
                emailsConfiguration,
                _configuration["Security:TextEncryptionKey"],
                null,
                null,
                userAccessConfiguration);

            PaymentsStartup.Initialize(
                _configuration["ConnectionStrings:" + MeetingsConnectionString],
                executionContextAccessor,
                _logger,
                emailsConfiguration,
                null);
        }
    }
}