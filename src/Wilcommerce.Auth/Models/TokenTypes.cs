using System;
using System.Linq;
using System.Reflection;

namespace Wilcommerce.Auth.Models
{
    /// <summary>
    /// Defines the available token types
    /// </summary>
    public static class TokenTypes
    {
        /// <summary>
        /// Password recovery token
        /// </summary>
        public static string PasswordRecovery => "PASSWORD_RECOVERY";

        /// <summary>
        /// Registration token
        /// </summary>
        public static string Registration => "REGISTRATION";

        /// <summary>
        /// Check whether the specified type is valid
        /// </summary>
        /// <param name="type">The token type to check</param>
        /// <returns>true if the type is valid, false otherwise</returns>
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
