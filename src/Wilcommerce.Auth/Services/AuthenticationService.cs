using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Common.Domain.ReadModels;
using Microsoft.AspNetCore.Identity;
using Wilcommerce.Auth.Services.Interfaces;
using Wilcommerce.Auth.Commands.Handlers.Interfaces;
using Wilcommerce.Auth.Commands;
using Wilcommerce.Core.Infrastructure;
using Wilcommerce.Auth.Events.User;

namespace Wilcommerce.Auth.Services
{
    /// <summary>
    /// Defines the implementations for the authentication actions
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Get the OWIN Authentication manager
        /// </summary>
        public AuthenticationManager AuthenticationManager { get; }

        /// <summary>
        /// Get the common context database
        /// </summary>
        public ICommonDatabase CommonDatabase { get; }

        /// <summary>
        /// Get the password hasher service
        /// </summary>
        public IPasswordHasher<User> PasswordHasher { get; }

        /// <summary>
        /// Get the token generator service
        /// </summary>
        public ITokenGenerator TokenGenerator { get; }

        /// <summary>
        /// Get the password recovery handler
        /// </summary>
        public IRecoverPasswordCommandHandler RecoverPasswordHandler { get; }

        /// <summary>
        /// Get the password recovery validation handler
        /// </summary>
        public IValidatePasswordRecoveryCommandHandler ValidatePasswordRecoveryHandler { get; }

        public IEventBus EventBus { get; }

        public AuthenticationService(AuthenticationManager authenticationManager, ICommonDatabase commonDatabase, IPasswordHasher<User> passwordHasher, ITokenGenerator tokenGenerator, IRecoverPasswordCommandHandler recoverPasswordHandler, IValidatePasswordRecoveryCommandHandler validatePasswordRecoveryHandler, IEventBus eventBus)
        {
            AuthenticationManager = authenticationManager;
            CommonDatabase = commonDatabase;
            PasswordHasher = passwordHasher;
            TokenGenerator = tokenGenerator;
            RecoverPasswordHandler = recoverPasswordHandler;
            ValidatePasswordRecoveryHandler = validatePasswordRecoveryHandler;
            EventBus = eventBus;
        }

        /// <inheritdoc cref="IAuthenticationService.SignIn(string, string, bool)"/>
        public Task SignIn(string email, string password, bool isPersistent)
        {
            try
            {
                var user = CommonDatabase.Users
                    .Where(u => u.IsActive && u.DisabledOn == null)
                    .FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    throw new InvalidOperationException($"User {email} not found");
                }

                if (!IsPasswordValid(user, password))
                {
                    throw new InvalidOperationException("Bad credentials");
                }

                var principal = CreatePrincipalForUser(user);
                var signin = AuthenticationManager.SignInAsync(
                    AuthenticationDefaults.AuthenticationScheme, 
                    principal, 
                    new AuthenticationProperties { IsPersistent = isPersistent });

                var @event = new UserSignedInEvent(user.Id, user.Email);
                EventBus.RaiseEvent(@event);

                return signin;
            }
            catch 
            {
                throw;
            }
        }

        /// <inheritdoc cref="IAuthenticationService.SignOut"/>
        public Task SignOut()
        {
            try
            {
                return AuthenticationManager.SignOutAsync(AuthenticationDefaults.AuthenticationScheme);
            }
            catch 
            {
                throw;
            }
        }

        /// <inheritdoc cref="IAuthenticationService.RecoverPassword(string)"/>
        public Task RecoverPassword(string email)
        {
            try
            {
                var user = CommonDatabase.Users
                    .Where(u => u.IsActive && u.DisabledOn == null)
                    .FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    throw new InvalidOperationException($"User {email} not found");
                }

                var command = new RecoverPasswordCommand(user, TokenGenerator.GenerateForUser(user));
                return RecoverPasswordHandler.Handle(command);
            }
            catch 
            {
                throw;
            }
        }

        /// <inheritdoc cref="IAuthenticationService.ValidatePasswordRecovery(string)"/>
        public Task ValidatePasswordRecovery(string token)
        {
            try
            {
                var command = new ValidatePasswordRecoveryCommand(token);
                return ValidatePasswordRecoveryHandler.Handle(command);
            }
            catch 
            {
                throw;
            }
        }

        #region Protected methods
        /// <summary>
        /// Check whether the password is valid
        /// </summary>
        /// <param name="user">The user instance</param>
        /// <param name="password">The password to check</param>
        /// <returns>true if the password is valid, false otherwise</returns>
        protected virtual bool IsPasswordValid(User user, string password)
        {
            try
            {
                var result = PasswordHasher.VerifyHashedPassword(user, user.Password, password);
                return result != PasswordVerificationResult.Failed;
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Create the claim principal for the user
        /// </summary>
        /// <param name="user">The user instance</param>
        /// <returns>The claim principal</returns>
        protected virtual ClaimsPrincipal CreatePrincipalForUser(User user)
        {
            try
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.Role, GetRoleStringForUser(user)));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));

                return new ClaimsPrincipal(identity);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Get the string representing the user's role
        /// </summary>
        /// <param name="user">The user instance</param>
        /// <returns>The user's role as a string</returns>
        protected virtual string GetRoleStringForUser(User user)
        {
            var role = user.Role;
            switch (role)
            {
                case User.Roles.CUSTOMER:
                    return AuthenticationDefaults.CustomerRole;
                case User.Roles.ADMINISTRATOR:
                    return AuthenticationDefaults.AdministratorRole;
                default:
                    return null;
            }
        }
        #endregion
    }
}
