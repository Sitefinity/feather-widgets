using DynamicContent.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace DynamicContent.Mvc.Models
{
    public static class DesignerModelHelper
    {
        public static Type DynamicContentType(this Control control)
        {
            var dynamicType = control.ResolveDynamicModuleType();
            return TypeResolutionService.ResolveType(dynamicType.GetFullTypeName());
        }

        public static string DynamicModuleName(this Control control)
        {
            var dynamicType = control.ResolveDynamicModuleType();
            return dynamicType.ModuleName;
        }

        private static DynamicModuleType ResolveDynamicModuleType(this Control control)
        {
            var mvcProxy = control as MvcWidgetProxy;
            if (mvcProxy == null)
                throw new ArgumentException("This method should be used for control designers of Dynamic content MVC widgets.");

            var dynamicContentController = mvcProxy.Controller as DynamicContentController;
            if (dynamicContentController == null)
                throw new ArgumentException("This method should be used for DynamicContentController's designer only.");

            return dynamicContentController.GetDynamicContentType(mvcProxy.WidgetName);
        }
    }
}
