using System;
using Wilcommerce.Auth.Services.Interfaces;
using Wilcommerce.Auth.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Wilcommerce.Auth.Services
{
    /// <summary>
    /// Implementation of <see cref="ITokenGenerator"/>
    /// </summary>
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Construct the token generator class
        /// </summary>
        /// <param name="userManager">The identity user manager</param>
        public TokenGenerator(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Implementation of <see cref="ITokenGenerator.GenerateEmailConfirmationTokenForUser(User)"/>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GenerateEmailConfirmationTokenForUser(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                return await _userManager.GenerateEmailConfirmationTokenAsync(user);
            }
            catch 
            {
                throw;
            }
        }

        public async Task<string> GeneratePasswordRecoveryTokenForUser(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                return await _userManager.GeneratePasswordResetTokenAsync(user);
            }
            catch 
            {
                throw;
            }
        }
    }
}
