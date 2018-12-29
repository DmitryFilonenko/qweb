using DbLayer.Infrsrt;
using DbLayer.Managers;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebUI.Infrastructure;

namespace WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // обработка события PostAuthenticateRequest
        //protected void Application_PostAuthenticateRequest()
        //{
        //    //WindowsIdentity identity = HttpContext.Current.Request.LogonUserIdentity;
        //    //string userName = identity.Name.Substring(identity.Name.LastIndexOf('\\') + 1);

        //    try
        //    {
        //        List<OracleParameter> args = new List<OracleParameter> {
        //            new OracleParameter("user_login", OracleDbType.Varchar2, userName, ParameterDirection.Input ) 
        //        };

        //        string userRole = ManagerPlProc.ExecFunc("q_users_pack.get_role_name", OracleDbType.Varchar2, args);
        //        //identity.Label = userRole;
        //    }
        //    catch (Exception ex)
        //    {
        //        QLoger.AddRecordToLog(userName, "Get_usrer_role", ex.Message, "1");
        //    }
        //}
    }
}
