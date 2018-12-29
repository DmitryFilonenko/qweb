using DbLayer.Managers;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebUI.Models.Home.User
{
    public class UserModel
    {
        public static string GetUserLogin()
        {
            WindowsIdentity identity = HttpContext.Current.Request.LogonUserIdentity;
            return identity.Name.Substring(identity.Name.LastIndexOf('\\') + 1);
        }

        public static string GetUserRole()
        {
            List<OracleParameter> args = new List<OracleParameter> {
                        new OracleParameter("user_login", OracleDbType.Varchar2, GetUserLogin(), ParameterDirection.Input )
             };
            return ManagerPlProc.ExecFunc("q_users_pack.get_role_name", OracleDbType.Varchar2, args);
        }       
    }
}