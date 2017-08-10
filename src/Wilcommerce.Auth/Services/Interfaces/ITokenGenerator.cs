using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Domain.Models;

namespace Wilcommerce.Auth.Services.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateForUser(User user);
    }
}
