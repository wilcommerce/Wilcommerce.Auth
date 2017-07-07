using System;
using Wilcommerce.Auth.Models;
using Xunit;

namespace Wilcommerce.Auth.Test.Models
{
    public class UserTokenTest
    {
        [Fact]
        public void PasswordRecovery_Should_Throw_ArgumentNullException_If_User_IsNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => UserToken.PasswordRecovery(null, "", DateTime.Now));
            Assert.Equal("user", ex.ParamName);
        }
    }
}
