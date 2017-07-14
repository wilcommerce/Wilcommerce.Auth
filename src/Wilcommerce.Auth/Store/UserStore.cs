using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Domain.Models;
using System.Threading;
using Wilcommerce.Core.Common.Domain.ReadModels;
using Wilcommerce.Core.Common.Domain.Repository;
using System.Linq;

namespace Wilcommerce.Auth.Store
{
    public class UserStore : IUserStore<User>
    {
        public ICommonDatabase Database { get; }

        public IRepository Repository { get; }

        public UserStore(ICommonDatabase database, IRepository repository)
        {
            Database = database;
            Repository = repository;
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }

                Repository.Add(user);
                return Repository.SaveChangesAsync().ContinueWith(t => IdentityResult.Success, cancellationToken);
            }
            catch 
            {
                throw;
            }
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }

                user.Disable();
                return Repository.SaveChangesAsync().ContinueWith(t => IdentityResult.Success, cancellationToken);
            }
            catch 
            {
                throw;
            }
        }

        public void Dispose()
        {
            if (Repository != null)
            {
                Repository.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException("userId");
                }

                var id = new Guid(userId);
                var user = Database.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                return Task.FromResult(user);
            }
            catch 
            {
                throw;
            }
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (string.IsNullOrEmpty(normalizedUserName))
                {
                    throw new ArgumentNullException("normalizedUserName");
                }

                var user = Database.Users.FirstOrDefault(u => u.Email == normalizedUserName);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                return Task.FromResult(user);
            }
            catch
            {
                throw;
            }
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }

                return Task.FromResult(user.Email);
            }
            catch
            {
                throw;
            }
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }

                return Task.FromResult(user.Id.ToString());
            }
            catch
            {
                throw;
            }
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }

                return Task.FromResult(user.Email);
            }
            catch
            {
                throw;
            }
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }

                if (string.IsNullOrEmpty(normalizedName))
                {
                    throw new ArgumentNullException("normalizedName");
                }

                user.ChangeEmail(normalizedName);
                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }

                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentNullException("userName");
                }

                user.ChangeEmail(userName);
                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }

                return Repository.SaveChangesAsync().ContinueWith(t => IdentityResult.Success, cancellationToken);
            }
            catch 
            {
                throw;
            }
        }
    }
}
