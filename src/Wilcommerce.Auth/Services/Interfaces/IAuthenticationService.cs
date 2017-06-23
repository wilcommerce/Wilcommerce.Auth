using System.Threading.Tasks;

namespace Wilcommerce.Auth.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task SignIn(string username, string password);

        Task SignOut();
    }
}
