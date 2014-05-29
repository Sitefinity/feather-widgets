using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;

namespace ContentBlock
{
    /// <summary>
    /// This class presents initialzier for the ContentBlock.
    /// </summary>
    public static class Initializer
    {
        /// <summary>
        /// Registers routes for the ContentBlock controller. 
        /// </summary>
        public static void Initialize()
        {
             Bootstrapper.MVC.MapRoute("MvcContentBlock", "ContentBlock/{controller}/{action}",
                    new { controller = "ContentBlock", action = "Index" });
        }
    }
}
