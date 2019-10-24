using Autofac;
using Autofac.Extensions.DependencyInjection;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.API.Configuration.Validation;
using CompanyName.MyMeetings.API.Modules.Administration;
using CompanyName.MyMeetings.API.Modules.Meetings;
using CompanyName.MyMeetings.API.Modules.Payments;
using CompanyName.MyMeetings.API.Modules.UserAccess;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration;
using CompanyName.MyMeetings.Modules.UserAccess.Application.IdentityServer;
using Hellang.Middleware.ProblemDetails;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;
using System;

namespace CompanyName.MyMeetings.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string MeetingsConnectionString = "MeetingsConnectionString";
        private static ILogger _logger;
        private static ILogger _loggerForApi;

        public Startup(IHostingEnvironment env)
        {
            ConfigureLogger();

            this._configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddUserSecrets<Startup>()
                .Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerDocumentation();

            ConfigureIdentityServer(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services
                .AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                
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

            services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();

            return CreateAutofacServiceProvider(services);
        }
     
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseMiddleware<CorrelationMiddleware>();
            
            app.UseSwaggerDocumentation();

            app.UseIdentityServer();

            if (env.IsDevelopment())
            {
                // app.UseProblemDetails();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc();
           
        }

        private static void ConfigureLogger()
        {
            _logger =  new LoggerConfiguration()   
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate:"[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.RollingFile(new CompactJsonFormatter(),"logs/logs")
                .CreateLogger();

            _loggerForApi = _logger.ForContext("Module", "API");

            _loggerForApi.Information("Logger configured");
        }

        private void ConfigureIdentityServer(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApis())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryPersistedGrants()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, x =>
                {
                    x.Authority = "http://localhost:5000";
                    x.ApiName = "myMeetingsAPI";
                    x.RequireHttpsMetadata = false;
                });
        }
        
        private IServiceProvider CreateAutofacServiceProvider(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(services);
            
            containerBuilder.RegisterModule(new MeetingsAutofacModule());
            containerBuilder.RegisterModule(new AdministrationAutofacModule());
            containerBuilder.RegisterModule(new UserAccessAutofacModule());
            containerBuilder.RegisterModule(new PaymentsAutofacModule());

            var container = containerBuilder.Build();
            
            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            var emailsConfiguration = new EmailsConfiguration(_configuration["EmailsConfiguration:FromEmail"]);
            
            MeetingsStartup.Initialize(this._configuration[MeetingsConnectionString], executionContextAccessor, _logger, emailsConfiguration);
            AdministrationStartup.Initialize(this._configuration[MeetingsConnectionString], executionContextAccessor, _logger);
            UserAccessStartup.Initialize(
                this._configuration[MeetingsConnectionString], 
                executionContextAccessor, 
                _logger, 
                emailsConfiguration,
                this._configuration["Security:TextEncryptionKey"]);
            PaymentsStartup.Initialize(this._configuration[MeetingsConnectionString], executionContextAccessor, _logger);

            return new AutofacServiceProvider(container);
        }
    }
}
