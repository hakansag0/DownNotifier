namespace DownNotifier.Domain.Entities.Shared
{
    public class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
    }
}
