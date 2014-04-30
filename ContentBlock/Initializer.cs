using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;

namespace ContentBlock
{
    public static class Initializer
    {
        public static void Initialize()
        {
            RouteTable.Routes.MapRoute("MvcContentBlock", "ContentBlock/{controller}/{action}", new { controller = "ContentBlock", action = "Index" });
        }
    }
}
