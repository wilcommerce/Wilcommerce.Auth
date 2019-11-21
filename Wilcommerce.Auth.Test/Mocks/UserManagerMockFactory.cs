using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Wilcommerce.Auth.Models;

namespace Wilcommerce.Auth.Test.Mocks
{
    public static class UserManagerMockFactory
    {
        public static UserManager<User> BuildUserManager(
            IUserStore<User> store = null, 
            IOptions<IdentityOptions> optionsAccessor = null, 
            IPasswordHasher<User> passwordHasher = null,
            IEnumerable<IUserValidator<User>> userValidators = null, 
            IEnumerable<IPasswordValidator<User>> passwordValidators = null)
        {
            if (store == null)
            {
                store = new Mock<IUserStore<User>>().Object;
            }
            
            var keyNormalizer = new Mock<ILookupNormalizer>().Object;
            var errors = new IdentityErrorDescriber();
            var services = new Mock<IServiceProvider>().Object;
            var logger = new Mock<ILogger<UserManager<User>>>().Object;

            var userManager = new UserManager<User>(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger);

            return userManager;
        }
    }
}
