using DownNotifier.Domain.Entities.Shared;

namespace DownNotifier.Domain.Entities
{
    public class TargetApp : BaseEntity
    {
        public int UserId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string URL { get; private set; } = string.Empty;

        public int MonitoringIntervalInSeconds { get; private set; } = 60 * 10 * 1000; //10 Minutes
        public DateTime LastCheckDate { get; private set; } = DateTime.MinValue;

        public TargetApp() { }

        private TargetApp(int userId, string name, string url, int monitoringIntervalInSeconds)
        {
            UserId = userId;
            SetName(name);
            SetURL(url);
            SetMonitoringIntervalInSeconds(monitoringIntervalInSeconds);
        }

        public static TargetApp Create(int userId, string name, string url, int monitoringIntervalInSeconds) => new(userId, name, url, monitoringIntervalInSeconds);

        public void SetName(string name)
        {
            //Name must not be empty
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be empty");
            }
            Name = name;
        }

        public void SetURL(string url)
        {
            URL = url;
        }

        public void SetMonitoringIntervalInSeconds(int monitoringIntervalInSeconds)
        {
            //Minimum interval is 10 second.
            MonitoringIntervalInSeconds = Math.Max(monitoringIntervalInSeconds, 10);
        }

        public void UpdateCheckDate(DateTime? updateDate = null)
        {
            LastCheckDate = updateDate != null ? DateTime.SpecifyKind(updateDate.Value, DateTimeKind.Utc) : DateTime.UtcNow;
        }
    }
}
