namespace Company.Proyect.Infra.Utils.Security
{
    using Isopoh.Cryptography.Argon2;

    public static class Cryptography
    {
        public static string GetHash(string text, string key)
        {
            return Argon2.Hash(text, key);
        }

        public static bool Validate(string hash, string text, string key)
        {
            return Argon2.Verify(hash, text, key);
        }
    }
}
