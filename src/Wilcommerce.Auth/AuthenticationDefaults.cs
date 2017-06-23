using Microsoft.AspNetCore.Authentication.Cookies;

namespace Wilcommerce.Auth
{
    public static class AuthenticationDefaults
    {
        public static string AuthenticationScheme => CookieAuthenticationDefaults.AuthenticationScheme;

        public static string CustomerRole => "Customer";

        public static string AdministratorRole => "Administrator";

        public static string CookiePrefix => ".Wilcommerce.";
    }
}
