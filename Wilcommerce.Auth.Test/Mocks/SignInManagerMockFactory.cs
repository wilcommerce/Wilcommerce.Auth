using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Wilcommerce.Auth.Models;

namespace Wilcommerce.Auth.Test.Mocks
{
    public static class SignInManagerMockFactory
    {
        public static SignInManager<User> BuildSignInManager(
            UserManager<User> userManager = null,
            IHttpContextAccessor contextAccessor = null,
            IUserClaimsPrincipalFactory<User> claimsFactory = null)
        {
            if (userManager == null)
            {
                userManager = UserManagerMockFactory.BuildUserManager();
            }
            if (contextAccessor == null)
            {
                contextAccessor = new Mock<IHttpContextAccessor>().Object;
            }
            if (claimsFactory == null)
            {
                claimsFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            }

            IOptions<IdentityOptions> optionsAccessor = null;
            ILogger<SignInManager<User>> logger = null;
            IAuthenticationSchemeProvider schemes = null;

            var signInManager = new SignInManager<User>(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes);

            return signInManager;
        }
    }
}
