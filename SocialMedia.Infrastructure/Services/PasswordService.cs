using Microsoft.Extensions.Options;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace SocialMedia.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _passwordOptions;
        public PasswordService(IOptions<PasswordOptions> options)
        {
            _passwordOptions = options.Value;
        }
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
                throw new FormatException("Unexpected hash format");

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
               password,
               salt,
               iterations,
               HashAlgorithmName.SHA512)
               )
            {
                var keyToCheck =algorithm.GetBytes(_passwordOptions.KeySize);

                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
                password, 
                _passwordOptions.SaltSize, 
                _passwordOptions.Iterations, 
                HashAlgorithmName.SHA512)
                )
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_passwordOptions.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{_passwordOptions.Iterations}.{salt}.{key}";
            }
        }
    }
}
