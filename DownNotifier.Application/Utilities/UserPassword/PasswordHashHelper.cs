using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.Utilities.UserPassword
{
    internal class PasswordHashHelper
    {
        internal static void CreatePasswordHashSHA512
            (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
        }

        /// <summary>
        /// Returns true if password is VALID
        /// </summary>
        /// <param name="password">password to check</param>
        /// <param name="passwordHash">hashed password stored in database</param>
        /// <param name="passwordSalt">password salt stored in database</param>
        /// <returns></returns>
        internal static bool VerifyPasswordHashSHA512(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
