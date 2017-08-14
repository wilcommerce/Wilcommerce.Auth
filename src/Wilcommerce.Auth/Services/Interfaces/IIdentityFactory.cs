using System.Security.Claims;
using Wilcommerce.Core.Common.Domain.Models;

namespace Wilcommerce.Auth.Services.Interfaces
{
    public interface IIdentityFactory
    {
        ClaimsPrincipal CreateIdentity(User user);
    }
}
