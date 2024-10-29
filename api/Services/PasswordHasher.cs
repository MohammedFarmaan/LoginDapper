using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace api.Services
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private readonly int saltSize = 16;
        private readonly int hashSize = 32;
        private readonly int iterations = 100000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        public string Hash(string password)
        {
           byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
           byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, Algorithm, hashSize);

           return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";

        }

        public bool Verify(string password, string passwordHash)
        {
             string[] parts = passwordHash.Split("-");

            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, Algorithm, hashSize);

            // return hash.SequenceEqual(inputHash);
            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}