using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wilcommerce.Auth.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task SignIn(string username, string password);

        Task SignOut();
    }
}
