using Microsoft.AspNetCore.Authentication.Cookies;

namespace Wilcommerce.Auth
{
    /// <summary>
    /// Defines the authentication's options default values
    /// </summary>
    public static class AuthenticationDefaults
    {
        /// <summary>
        /// Get the Authentication scheme used
        /// </summary>
        public const string AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        /// <summary>
        /// Get the string representing the customer role
        /// </summary>
        public const string CustomerRole = "Customer";

        /// <summary>
        /// Get the string representing the administrator role
        /// </summary>
        public const string AdministratorRole = "Administrator";

        /// <summary>
        /// Get the cookie prefix
        /// </summary>
        public const string CookiePrefix = ".Wilcommerce.";

        /// <summary>
        /// Get the number of days after which expires the generated tokens
        /// </summary>
        public const int ExpirationDays = 1;
    }
}
