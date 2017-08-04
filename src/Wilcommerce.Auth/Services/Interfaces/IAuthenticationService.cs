using System.Threading.Tasks;

namespace Wilcommerce.Auth.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task SignIn(string username, string password, bool isPersistent);

        Task SignOut();

        Task RecoverPassword(string email);

        Task ValidatePasswordRecovery(string token);
    }
}
