namespace Company.Project.Infra.Utils.Security
{
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;

    /// <summary>
    /// Cryptography class.
    /// </summary>
    public static class Cryptography
    {
        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string GetHash(string password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                return Convert.ToBase64String(HashPassword(password, rng));
            }
        }

        /// <summary>
        /// Validates the specified hashed password.
        /// </summary>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static bool Validate(string hashedPassword, string password)
        {
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);
            return VerifyHashedPassword(decodedHashedPassword, password);
        }

        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="rng">The RNG.</param>
        /// <returns></returns>
        private static byte[] HashPassword(string password, RandomNumberGenerator rng)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
            const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
            const int SaltSize = 128 / 8; // 128 bits

            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            outputBytes[0] = 0x00; // format marker
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
            return outputBytes;
        }

        /// <summary>
        /// Verifies the hashed password.
        /// </summary>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        private static bool VerifyHashedPassword(byte[] hashedPassword, string password)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
            const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
            const int SaltSize = 128 / 8; // 128 bits

            // We know ahead of time the exact length of a valid hashed password payload.
            if (hashedPassword.Length != 1 + SaltSize + Pbkdf2SubkeyLength)
            {
                return false; // bad size
            }

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPassword, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(hashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);
            return ByteArraysEqual(actualSubkey, expectedSubkey);
        }

        /// <summary>
        /// Bytes the arrays equal.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }

    }
}
