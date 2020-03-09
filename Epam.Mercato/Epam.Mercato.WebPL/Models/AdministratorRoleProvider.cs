using Epam.Mercato.BLL.Interfaces;
using Epam.Mercato.Entities;
using Epam.Mercato.IOC;
using System;
using System.Linq;
using System.Web.Security;

namespace Epam.Mercato.WebPL.Models
{
    public class AdministratorRoleProvider : RoleProvider
    {
        IModeratorLogic ModeratorLogic = DependencyResolver.ModeratorLogic;

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            var user = ModeratorLogic.GetAll().FirstOrDefault(x => x.Value.Login == username);
            if (user.Value != null && user.Value.ModeratorRole==Role.administrator) { roles.Append("ADMIN"); }
            if (user.Value != null && user.Value.ModeratorRole == Role.moderator) { roles.Append("MODERATOR"); }
            return roles;
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            var user = ModeratorLogic.GetAll().FirstOrDefault(x => x.Value.Login == username);
            Enum.TryParse(roleName, out Role newRole);
            if (user.Value!=null&&user.Value.Login==username&&user.Value.ModeratorRole==newRole)
            {
                return true;
            }
            return false;
        }

        #region NOT_IMPLEMENTED
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }



        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}