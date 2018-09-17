using ExpenseManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ExpenseManagement.Authentication
{
    public class ExpenseRoleProvider : RoleProvider
    {
        ExpenseTrackerEntities db;
        public ExpenseRoleProvider()
        {
            db = new ExpenseTrackerEntities();
        }
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

        public override string[] GetRolesForUser(string username)
        {
            var result = db.Users.Where(x => x.UserName == username).FirstOrDefault().UserRole.ToString().ElementAt(0).ToString();
            return new string[] { result };

            //string[] s = { db.Users.Where(x => x.UserName == username).FirstOrDefault().UserRole };
            //return s;

        }

        public override string[] GetUsersInRole(string roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
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
    }
}