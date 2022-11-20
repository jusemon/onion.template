namespace Company.Project.Infra.Utils.Security
{
    /// <summary>
    /// Custom Claim Types class. 
    /// </summary>
    public static class CustomClaimTypes
    {
        /// <summary>
        /// The permission
        /// </summary>
        public static readonly string Permission = "http://example.org/claims/permission";

        /// <summary>
        /// The isAdmin
        /// </summary>
        public static readonly string IsAdmin = "http://example.org/claims/is_admin";
    }
}
