using DownNotifier.Application.Repositories;
using DownNotifier.Application.Utilities.HealthCheck;
using DownNotifier.Application.Utilities.Notification;
using Microsoft.AspNetCore.Http;

namespace DownNotifier.MVC.Services
{
    public class AppStatusCheckerBackgroundService : BackgroundService
    {
        private readonly ILogger<AppStatusCheckerBackgroundService> logger;
        private readonly IServiceProvider serviceProvider;

        public AppStatusCheckerBackgroundService(ILogger<AppStatusCheckerBackgroundService> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await DoWork();
                await Task.Delay(10 * 1000, stoppingToken);
            }
        }

        private async Task DoWork()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var appStatusNotificationSender = scope.ServiceProvider.GetRequiredService<IAppStatusNotificationSender>();
                var targetAppRepository = scope.ServiceProvider.GetRequiredService<ITargetAppRepository>();

                var appsToCheck = targetAppRepository.GetAll();

                foreach (var targetApp in appsToCheck.Where(s => s.LastCheckDate.AddSeconds(s.MonitoringIntervalInSeconds) < DateTime.UtcNow))
                {
                    bool status = true;
                    try
                    {
                        status = await HealthChecker.CheckURL(targetApp.URL);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.Message);
                    }
                    finally
                    {
                        targetApp.UpdateCheckDate(DateTime.UtcNow);
                        if (!status)
                        {
                            appStatusNotificationSender.SendNotification(targetApp, string.Empty);
                        }
                    }

                }

                targetAppRepository.UpdateAll(appsToCheck);
                targetAppRepository.SaveChanges();
            }
        }
    }
}
