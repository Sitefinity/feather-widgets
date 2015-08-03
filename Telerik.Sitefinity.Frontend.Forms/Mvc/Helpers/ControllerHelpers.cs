using System;
using System.Web.Mvc;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.Proxy;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Helper
{
    public static class ControllerHelpers
    {
        public static MvcHtmlString FormController(this HtmlHelper htmlHelper, Guid id, string action, object routeValues)
        {
            var manager = FormsManager.GetManager();
            var controlData = manager.GetControl<FormControl>(id);
            var control = manager.LoadControl(controlData);
            var mvcProxy = control as MvcProxyBase;
            if (mvcProxy == null)
                throw new InvalidOperationException("Cannot render form controller with the given ID becaouse the control with this ID is not an MVC proxy.");

            string result;
            var actionInvoker = ObjectFactory.Resolve<IControllerActionInvoker>();
            
            var requestContext = new RequestContext(htmlHelper.ViewContext.HttpContext, new RouteData());
            if (routeValues != null)
            {
                var routeDictionary = new RouteValueDictionary(routeValues);
                foreach (var kv in routeDictionary)
                {
                    requestContext.RouteData.Values.Add(kv.Key, kv.Value);
                }
            }

            requestContext.RouteData.Values["action"] = action;

            var page = (System.Web.UI.Page)htmlHelper.ViewContext.HttpContext.CurrentHandler;
            var prevRequest = page.Request.RequestContext;
            try
            {
                page.Request.RequestContext = requestContext;
                actionInvoker.TryInvokeAction(mvcProxy, out result);
            }
            finally
            {
                page.Request.RequestContext = prevRequest;
            }

            return MvcHtmlString.Create(result);
        }
    }
}