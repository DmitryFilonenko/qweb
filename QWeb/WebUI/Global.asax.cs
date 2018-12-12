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
           // PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
        }

        // обработка события PostAuthenticateRequest
        protected void Application_PostAuthenticateRequest()
        {
            WindowsIdentity identity = HttpContext.Current.Request.LogonUserIdentity;
            string name = identity.Name;
            //QLoger.AddRecordToLog(name, "Autintification", "test", "0");            

            try
            {
                List<ProcParam> args = new List<ProcParam> {
                    new ProcParam { Name = "user_login", Type = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = name },
                    new ProcParam { Type = OracleDbType.Varchar2, Direction = ParameterDirection.ReturnValue }
                };
                string role = ManagerPlProc.ExecFunc("q_users_pack.get_role", args);
            }
            catch (Exception ex)
            {                
                throw;
            }
        }
    }
}
