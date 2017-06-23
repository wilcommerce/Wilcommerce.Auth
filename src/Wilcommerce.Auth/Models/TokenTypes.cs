using System;
using System.Linq;
using System.Reflection;

namespace Wilcommerce.Auth.Models
{
    public static class TokenTypes
    {
        public static string PasswordRecovery => "PASSWORD_RECOVERY";

        public static string Registration => "REGISTRATION";

        public static bool IsValidType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException("type");
            }

            bool isValid = typeof(TokenTypes).GetProperties(BindingFlags.Static).Any(p => p.GetValue(null).ToString() == type);
            return isValid;
        }
    }
}
