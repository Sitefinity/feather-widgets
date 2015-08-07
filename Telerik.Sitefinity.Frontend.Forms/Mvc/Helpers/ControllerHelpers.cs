using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.Proxy;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Helpers
{
    public static class ControllerHelpers
    {
        /// <summary>
        /// Gets HTML markup for required HTML attributes for a submit button on the current request.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <returns>The required HTML attributes.</returns>
        public static MvcHtmlString SubmitButtonRequiredAttributes(this HtmlHelper htmlHelper)
        {
            if (Telerik.Sitefinity.Services.SystemManager.IsDesignMode || Telerik.Sitefinity.Services.SystemManager.IsInlineEditingMode)
            {
                return MvcHtmlString.Create("onclick=\"return false;\"");
            }
            else
            {
                return MvcHtmlString.Empty;
            }
        }

        public static MvcHtmlString FormController(this HtmlHelper htmlHelper, Guid id, FormViewMode viewMode, object routeValues)
        {
            var manager = FormsManager.GetManager();
            var controlData = manager.GetControl<FormControl>(id);
            var control = manager.LoadControl(controlData);

            var mvcProxy = control as MvcProxyBase;
            if (mvcProxy == null)
                throw new InvalidOperationException("Cannot render form controller with the given ID becaouse the control with this ID is not an MVC proxy.");

            var routeData = new RouteData();
            if (routeValues != null)
            {
                var routeDictionary = new RouteValueDictionary(routeValues);
                foreach (var kv in routeDictionary)
                {
                    routeData.Values.Add(kv.Key, kv.Value);
                }
            }

            var controller = mvcProxy.GetController();

            var controllerFactory = (ISitefinityControllerFactory)ControllerBuilder.Current.GetControllerFactory();
            routeData.Values["controller"] = controllerFactory.GetControllerName(controller.GetType());

            var action = controller is IFormFieldController ? viewMode.ToString() : "Index";
            routeData.Values["action"] = action;

            using (var writer = new StringWriter())
            {
                var context = new HttpContextWrapper(new HttpContext(HttpContext.Current.Request, new HttpResponse(writer)));
                context.Handler = HttpContext.Current.Handler;

                controller.ControllerContext = new ControllerContext(context, routeData, controller);
                controller.ActionInvoker.InvokeAction(controller.ControllerContext, action);

                var result = writer.ToString();
                return MvcHtmlString.Create(result);
            }
        }
    }
}