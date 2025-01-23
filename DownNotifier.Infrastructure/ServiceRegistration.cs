using DownNotifier.Application.Repositories;
using DownNotifier.Application.Utilities;
using DownNotifier.Application.Utilities.Notification;
using DownNotifier.Infrastructure.Context;
using DownNotifier.Infrastructure.Repositories;
using DownNotifier.Infrastructure.Utilities;
using DownNotifier.Infrastructure.Utilities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DownNotifier.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureInjections(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MsSqlServer");
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            services.AddDbContext<DownNotifierDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITargetAppRepository, TargetAppRepository>();


            services.AddScoped<IAppStatusNotificationSender, EmailNotificationSender>();

#if DEBUG
            services.AddScoped<IEmailSender, EmailSender_ConsoleLogger>();
#else
            if (string.IsNullOrEmpty(configuration.GetSection("SMTP:Host").Value) || string.IsNullOrEmpty(configuration.GetSection("SMTP:Port").Value) || string.IsNullOrEmpty(configuration.GetSection("SMTP:EmailAddress").Value) || string.IsNullOrEmpty(configuration.GetSection("SMTP:Password").Value))
            {
                throw new NullReferenceException("SMTP environment values must not be empty");
            }
            services.AddSingleton<SMTP_Values>(options => new(configuration.GetRequiredSection("SMTP:Host").Value!, int.Parse(configuration.GetRequiredSection("SMTP:Port").Value!), configuration.GetRequiredSection("SMTP:EmailAddress").Value!, configuration.GetRequiredSection("SMTP:Password").Value!));
            services.AddScoped<IEmailSender, EmailSender_SMTP>();
#endif

            var optBuilder = new DbContextOptionsBuilder<DownNotifierDbContext>().UseSqlServer(connectionString);
            using (var dbContext = new DownNotifierDbContext(optBuilder.Options))
            {
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
