using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wilcommerce.Auth.Services.Interfaces;
using Wilcommerce.Core.Infrastructure;
using Wilcommerce.Auth.Events.User;
using Wilcommerce.Auth.ReadModels;
using Wilcommerce.Auth.Models;
using Microsoft.AspNetCore.Authentication;

namespace Wilcommerce.Auth.Services
{
    /// <summary>
    /// Implementation of <see cref="IAuthenticationService"/>
    /// </summary>
    public class AuthenticationService : Interfaces.IAuthenticationService
    {
        /// <summary>
        /// Get the common context database
        /// </summary>
        public IAuthDatabase AuthDatabase { get; }

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
        /// <param name="eventBus">The event bus instance</param>
        /// <param name="signInManager"></param>
        public AuthenticationService(IAuthDatabase authDatabase, IEventBus eventBus, SignInManager<User> signInManager)
        {
            AuthDatabase = authDatabase ?? throw new ArgumentNullException(nameof(authDatabase));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            SignInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        /// <summary>
        /// Implementation of <see cref="Interfaces.IAuthenticationService.SignIn(string, string, bool)"/>
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        public virtual async Task<SignInResult> SignIn(string email, string password, bool isPersistent)
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
                    var @event = new UserSignedInEvent(Guid.Parse(user.Id), user.Email);
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
        /// Implementation of <see cref="Interfaces.IAuthenticationService.SignOut()"/>
        /// </summary>
        /// <returns></returns>
        public virtual Task SignOut()
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
