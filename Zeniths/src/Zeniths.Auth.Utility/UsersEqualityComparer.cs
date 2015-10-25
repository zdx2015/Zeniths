using System.Collections.Generic;
using Zeniths.Auth.Entity;

namespace Zeniths.Auth.Utility
{
    public class UsersEqualityComparer : IEqualityComparer<SystemUser>
    {
        public bool Equals(SystemUser user1, SystemUser user2)
        {
            return user1 == null || user2 == null || user1.Id == user2.Id;
        }

        public int GetHashCode(SystemUser user)
        {
            return user.Id.GetHashCode();
        }
    }
}