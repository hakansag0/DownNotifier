using DownNotifier.Domain.Entities.Shared;

namespace DownNotifier.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; private set; }
        public string Name { get; private set; }

        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        public User() { }

        private User(string email, string name, byte[] passwordHash, byte[] passwordSalt)
        {
            Email = email.Trim();
            Name = name.Trim();
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public static User Create(string email, string name, byte[] passwordHash, byte[] passwordSalt) => new(email, name, passwordHash, passwordSalt);
    }
}
