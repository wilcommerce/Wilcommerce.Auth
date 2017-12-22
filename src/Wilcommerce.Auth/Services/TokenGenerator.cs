using System;
using System.Text;
using Wilcommerce.Auth.Services.Interfaces;
using Wilcommerce.Core.Common.Domain.Models;

namespace Wilcommerce.Auth.Services
{
    /// <summary>
    /// Implementation of <see cref="ITokenGenerator"/>
    /// </summary>
    public class TokenGenerator : ITokenGenerator
    {
        /// <see cref="ITokenGenerator.GenerateForUser(User)"/>
        public string GenerateForUser(User user)
        {
            try
            {
                var tokenBytes = Encoding.UTF8.GetBytes($"{user.Email}{Guid.NewGuid().ToString("N")}");
                return Convert.ToBase64String(tokenBytes);
            }
            catch 
            {
                throw;
            }
        }
    }
}
