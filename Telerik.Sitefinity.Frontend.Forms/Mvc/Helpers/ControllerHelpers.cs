﻿using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Web.UI;

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
            control.Page = HttpContext.Current.Handler as Page;

            var mvcProxy = control as MvcProxyBase;
            if (mvcProxy == null)
                throw new InvalidOperationException("Cannot render form controller with the given ID becaouse the control with this ID is not an MVC proxy.");

            var actionInvoker = ObjectFactory.Resolve<IControllerActionInvoker>() as Telerik.Sitefinity.Mvc.ControllerActionInvoker;
            if (actionInvoker != null)
                actionInvoker.DeserializeControllerProperties(mvcProxy);

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

            if (mvcProxy.IsIndexingMode() && controller.GetIndexRenderMode() == IndexRenderModes.NoOutput)
                return MvcHtmlString.Empty;

            var controllerFactory = (ISitefinityControllerFactory)ControllerBuilder.Current.GetControllerFactory();
            var controllerType = controller.GetType();
            var explicitMode = ControllerHelpers.ExplicitMode(controllerType);
            if ((explicitMode == FormControlDisplayMode.Read && viewMode != FormViewMode.Read) || (explicitMode == FormControlDisplayMode.Write && viewMode != FormViewMode.Write))
                return MvcHtmlString.Empty;

            routeData.Values["controller"] = controllerFactory.GetControllerName(controllerType);

            string action = typeof(IFormElementController<IFormElementModel>).IsAssignableFrom(controllerType) ? action = viewMode.ToString() : action = "Index";
            routeData.Values["action"] = action;

            using (var writer = new StringWriter())
            {
                var context = new HttpContextWrapper(new HttpContext(HttpContext.Current.Request, new HttpResponse(writer)));
                ControllerHelpers.PopulateHttpContext(context);

                controller.ControllerContext = new ControllerContext(context, routeData, controller);
                controller.ActionInvoker.InvokeAction(controller.ControllerContext, action);

                ControllerHelpers.RestoreHttpContext(controller);

                var result = writer.ToString();
                return MvcHtmlString.Create(result);
            }
        }

        private static void PopulateHttpContext(HttpContextWrapper context)
        {
            context.Handler = HttpContext.Current.Handler;
            context.User = HttpContext.Current.User;

            foreach (var key in HttpContext.Current.Items.Keys)
            {
                context.Items[key] = HttpContext.Current.Items[key];
            }
        }

        private static void RestoreHttpContext(Controller controller)
        {
            foreach (var key in controller.ControllerContext.HttpContext.Items.Keys)
            {
                HttpContext.Current.Items[key] = controller.ControllerContext.HttpContext.Items[key];
            }

            HttpContext.Current.User = controller.ControllerContext.HttpContext.User;
        }

        private static FormControlDisplayMode ExplicitMode(Type controllerType)
        {
            var attributes = controllerType.GetCustomAttributes(typeof(FormControlDisplayModeAttribute), true);
            if (attributes.Length > 0)
            {
                var attr = (FormControlDisplayModeAttribute)attributes[0];
                return attr.FormControlDisplayMode;
            }
            else
            {
                return FormControlDisplayMode.ReadAndWrite;
            }
        }
    }
}