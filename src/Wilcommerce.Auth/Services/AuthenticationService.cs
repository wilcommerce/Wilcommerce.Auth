using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wilcommerce.Auth.Services.Interfaces;
using Wilcommerce.Core.Infrastructure;
using Wilcommerce.Auth.Events.User;
using Wilcommerce.Auth.ReadModels;
using Wilcommerce.Auth.Models;

namespace Wilcommerce.Auth.Services
{
    /// <summary>
    /// Implementation of <see cref="IAuthenticationService"/>
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Get the common context database
        /// </summary>
        public IAuthDatabase AuthDatabase { get; }

        /// <summary>
        /// Get the token generator service
        /// </summary>
        public ITokenGenerator TokenGenerator { get; }

        /// <summary>
        /// Get the event bus
        /// </summary>
        public IEventBus EventBus { get; }

        /// <summary>
        /// Get the signin manager instance
        /// </summary>
        public SignInManager<User> SignInManager { get; }

        /// <summary>
        /// Construct the authentication service
        /// </summary>
        /// <param name="authDatabase">The common database instance</param>
        /// <param name="tokenGenerator">The token generator instance</param>
        /// <param name="eventBus">The event bus instance</param>
        /// <param name="signInManager"></param>
        public AuthenticationService(IAuthDatabase authDatabase, ITokenGenerator tokenGenerator, IEventBus eventBus, SignInManager<User> signInManager)
        {
            AuthDatabase = authDatabase ?? throw new ArgumentNullException(nameof(authDatabase));
            TokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            SignInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        /// <summary>
        /// Implementation of <see cref="IAuthenticationService.SignIn(string, string, bool)"/>
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        public async Task<SignInResult> SignIn(string email, string password, bool isPersistent)
        {
            try
            {
                var user = AuthDatabase.Users
                    .Actives()
                    .WithUsername(email)
                    .Single();

                var signin = await SignInManager.PasswordSignInAsync(user, password, isPersistent, false);
                if (signin.Succeeded)
                {
                    var @event = new UserSignedInEvent(user.Id, user.Email);
                    EventBus.RaiseEvent(@event);
                }

                return signin;
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IAuthenticationService.SignOut()"/>
        /// </summary>
        /// <returns></returns>
        public Task SignOut()
        {
            try
            {
                return SignInManager.SignOutAsync();
            }
            catch 
            {
                throw;
            }
        }
    }
}
