using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ClinicDoctor.Data;
using ClinicDoctor.Entities;

/// <summary>
/// Summary description for CustomRoleProvider
/// </summary>
public class CustomRoleProvider :RoleProvider
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
        string roles = ClinicDoctor.Settings.BusinessLayer.ServiceFacade.SettingsHelper.Roles;
        return roles.Split(';');
        //throw new NotImplementedException();
    }

    public override string[] GetRolesForUser(string username)
    {
        string authUserName;
        string[] split = username.Split('\\');
        if (split.Length > 1)
            authUserName = username.Split('\\')[1];
        else
            authUserName = username;
        Staff obj = DataRepository.StaffProvider.GetByUserName(authUserName);

        if (obj == null)
            return null;

        if (obj.Roles.Contains(';'))
            return obj.Roles.Split(';');

        string[] result = new string[1];
        result[0] = obj.Roles;
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
