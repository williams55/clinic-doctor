using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using AppointmentSystem.Data;
using AppointmentSystem.Settings.BusinessLayer;

/// <summary>
/// Summary description for CustomRoleProvider
/// </summary>
public class CustomRoleProvider : RoleProvider
{
    public CustomRoleProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
        throw new NotImplementedException();
    }

    public override string ApplicationName
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
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
        var lstRoles = DataRepository.UserGroupProvider.GetAll();
        lstRoles = lstRoles.FindAll(x => !x.IsDisabled);
        return lstRoles.Select(x => x.Title).ToArray();
    }

    public override string[] GetRolesForUser(string username)
    {
        //string authUserName = username.Split('\\')[1];
        var user = DataRepository.UsersProvider.GetByUsername(username);
        if (user == null || user.IsDisabled)
        {
            return new string[] { };
        }

        var result = new string[1];
        result[0] = user.UserGroupId;
        return result;
    }

    public override string[] GetUsersInRole(string roleName)
    {
        throw new NotImplementedException();
    }

    public override bool IsUserInRole(string username, string roleName)
    {
        int abc = 0;
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
