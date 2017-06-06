using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MediaLiveDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //  GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteTable.Routes.MapHttpRoute(
                 "DefaultVideo",
                  "api/{controller}/{ext}/{filename}"
              );
            RouteTable.Routes.MapHttpRoute(
                "DefaultDownload",
                "api/{controller}/{action}"
            );
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
