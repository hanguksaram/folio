using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ERP.Core.Logger;
using FluentValidation.Mvc;

namespace ERP.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //handle wrong ssl on practice compass jira
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            FluentValidationModelValidatorProvider.Configure();
        }

        private ILogger Logger;

        protected void Application_Error()
        {
            if (Logger == null)
            {
                Logger = new Log4NetLogger("Application_Error");
            }

            Exception exc = Server.GetLastError();
            Logger.Error("application Error", exc);
        }
    }
}
