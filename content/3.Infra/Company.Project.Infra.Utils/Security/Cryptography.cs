namespace Company.Project.Infra.Utils.Security
{
    using Isopoh.Cryptography.Argon2;

    /// <summary>
    /// Cryptography class. 
    /// </summary>
    public static class Cryptography
    {
        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetHash(string text, string key)
        {
            return Argon2.Hash(text, key);
        }

        /// <summary>
        /// Validates the specified hash.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="text">The text.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static bool Validate(string hash, string text, string key)
        {
            return Argon2.Verify(hash, text, key);
        }
    }
}
