﻿using System;
using System.Threading.Tasks;
using Wilcommerce.Auth.Events.User;
using Wilcommerce.Auth.Models;
using Wilcommerce.Auth.Repository;

namespace Wilcommerce.Auth.Commands.Handlers
{
    public class RecoverPasswordCommandHandler : Interfaces.IRecoverPasswordCommandHandler
    {
        public Core.Infrastructure.IEventBus EventBus { get; }

        public IRepository Repository { get; }

        public RecoverPasswordCommandHandler(IRepository repository, Core.Infrastructure.IEventBus eventBus)
        {
            Repository = repository;
            EventBus = eventBus;
        }

        public async Task Handle(RecoverPasswordCommand command)
        {
            try
            {
                var userToken = UserToken.PasswordRecovery(command.UserInfo, command.Token, DateTime.Now.AddDays(AuthenticationDefaults.ExpirationDays));

                Repository.Add(userToken);
                await Repository.SaveChangesAsync();

                var @event = new PasswordRecoveryRequestedEvent(userToken.UserId, userToken.User.Email, userToken.Id, userToken.Token, userToken.ExpirationDate);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }
    }
}
