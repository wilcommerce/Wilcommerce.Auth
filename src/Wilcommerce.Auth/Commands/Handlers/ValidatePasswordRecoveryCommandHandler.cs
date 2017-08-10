using System;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Auth.Models;
using Wilcommerce.Auth.ReadModels;
using Wilcommerce.Auth.Repository;

namespace Wilcommerce.Auth.Commands.Handlers
{
    public class ValidatePasswordRecoveryCommandHandler : Interfaces.IValidatePasswordRecoveryCommandHandler
    {
        public IAuthDatabase Database { get; }

        public IRepository Repository { get; }

        public ValidatePasswordRecoveryCommandHandler(IAuthDatabase database, IRepository repository)
        {
            Database = database;
            Repository = repository;
        }

        public async Task Handle(ValidatePasswordRecoveryCommand command)
        {
            try
            {
                var userToken = Database.Tokens
                    .NotExpired()
                    .ByTokenType(TokenTypes.PasswordRecovery)
                    .FirstOrDefault(t => t.Token == command.Token);

                if (userToken == null)
                {
                    throw new InvalidOperationException("Invalid token");
                }

                var token = await Repository.GetByKeyAsync<UserToken>(userToken.Id);
                token.SetAsExpired();

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }
    }
}
