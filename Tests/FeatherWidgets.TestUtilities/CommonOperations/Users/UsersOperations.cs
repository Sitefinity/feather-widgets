using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace FeatherWidgets.TestUtilities.CommonOperations.Users
{
    /// <summary>
    /// This is a class with server operations related to users.
    /// </summary>
    public class UsersOperations
    {
        /// <summary>
        /// Assign specific role to a specific user.
        /// </summary>
        /// <param name="roleName">Role name.</param>
        /// <param name="roleProvider">Role provider.</param>
        /// <param name="userName">User name.</param>
        public void AddUserToRole(string roleName, string roleProvider, string userName)
        {
            RoleManager roleManager = RoleManager.GetManager(roleProvider);
            var role = roleManager.GetRole(roleName);
            User user = UserManager.GetManager().GetUser(userName);

            if (user == null)
            {
                throw new ArgumentException("There is no such user");
            }

            roleManager.AddUserToRole(user, role);
            roleManager.SaveChanges();
        }

        /// <summary>
        /// Removes specific role from specific user.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <param name="roleProvider">The provider name.</param>
        /// <param name="userName">The user name.</param>
        public void RemoveUserFromRole(string roleName, string roleProvider, string userName)
        {
            RoleManager roleManager = RoleManager.GetManager(roleProvider);
            var role = roleManager.GetRole(roleName);
            User user = UserManager.GetManager().GetUser(userName);

            if (user == null)
            {
                throw new ArgumentException("There is no such user");
            }

            roleManager.RemoveUserFromRole(user, role);
            roleManager.SaveChanges();
        }
    }
}
