using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Crud_application.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
            name: "",
            routeTemplate: "api/{controller}(foldername)/{Action}(APIName)",
            defaults: new
            {
                Controller = "(foldername)",
                Action = "(APIName)",
                id = RouteParameter.Optional
            }
          );
        }
    }
}