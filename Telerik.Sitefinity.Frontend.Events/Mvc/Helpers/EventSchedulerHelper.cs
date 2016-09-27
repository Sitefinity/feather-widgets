using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Events.Mvc.Models.EventScheduler;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Helpers
{
    /// <summary>
    /// This class contains functionality for working with event items.
    /// </summary>
    internal class EventSchedulerHelper
    {
        public static IEventSchedulerModel LoadModel(Guid widgetId, CultureInfo culture)
        {
            var pageManager = PageManager.GetManager();
            var objectData = pageManager.GetControls<ObjectData>().SingleOrDefault(p => p.Id == widgetId);

            if (objectData is PageDraftControl && ClaimsManager.IsBackendUser() == false)
                return null;

            IEventSchedulerModel model = null;

            if (objectData != null)
            {
                var mvcProxy = pageManager.LoadControl(objectData, culture) as MvcControllerProxy;

                if (mvcProxy != null)
                {
                    var schedullerController = mvcProxy.Controller as EventSchedulerController;
                    if (schedullerController != null)
                        model = schedullerController.Model;
                }
            }

            return model;
        }

        /// <summary>
        /// Get controller widget id
        /// </summary>
        /// <returns></returns>
        public static Guid GetWidgetId(Controller controller)
        {
            var pageManager = PageManager.GetManager();
            var viewBagControlId = controller.ViewData[ControllerKey];
            if (viewBagControlId == null)
            {
                return Guid.Empty;
            }

            string controlId = (string)viewBagControlId;

            // templates
            if (controller.HttpContext.Items[IsTemplate] != null && (bool)controller.HttpContext.Items[IsTemplate] == true)
            {
                // check if action is after save or cancel in template
                if (controller.HttpContext.Items[FormControlId] != null)
                {
                    return (Guid)controller.HttpContext.Items[FormControlId];
                }

                var templateId = GetTemplateIdKey(controller.ControllerContext.HttpContext);
                var versionId = GetVersionNumberKey(controller.ControllerContext.HttpContext);

                if (templateId != null && versionId != null)
                {
                    var template = pageManager.GetTemplate(new Guid(templateId));
                    if (template != null)
                    {
                        var versionManager = Telerik.Sitefinity.Versioning.VersionManager.GetManager();
                        TemplateDraft draft = new TemplateDraft();
                        versionManager.GetSpecificVersionByChangeId(draft, new Guid(versionId));

                        var control = GetControl(draft.Controls, controlId);
                        if (control != null)
                        {
                            return control.OriginalControlId;
                        }
                    }
                }

                // check if loaded in template
                var templateData = controller.HttpContext.Items[TemplateDraftProxy] as Telerik.Sitefinity.Modules.Pages.TemplateDraftProxy;
                if (templateData != null)
                {
                    var template = pageManager.GetTemplate(templateData.ParentItemId);
                    if (template != null)
                    {
                        if (IsEdit)
                        {
                            var control = GetControl(template.Drafts.FirstOrDefault(p => p.IsTempDraft).Controls, controlId);
                            if (control != null)
                            {
                                return control.Id;
                            }
                        }
                        else
                        {
                            var control = GetControl(template.Controls, controlId);
                            if (control != null)
                            {
                                return control.Id;
                            }
                        }
                    }
                }
            }
            else
            {
                // pages
                var pageId = SiteMapBase.GetCurrentNode().PageId;
                var page = pageManager.GetPageData(pageId);

                // if control is not overrided template control, if page is created page template is null
                if (page.Template != null)
                {
                    var templateControl = page.Template.Controls.FirstOrDefault(p => p.IsOverridedControl == false && p.Properties.FirstOrDefault(t => t.Name == "ID" && controlId.EndsWith(t.Value)) != null);
                    if (templateControl != null)
                    {
                        return templateControl.Id;
                    }
                }

                if (IsEdit)
                {
                    var pageDraft = page.Drafts.FirstOrDefault(p => p.IsTempDraft);

                    // Draft, if page is created page template is null, only draft is avalaible
                    if (page.Template == null && pageDraft.TemplateId != Guid.Empty)
                    {
                        var template = pageManager.GetTemplate(pageDraft.TemplateId);
                        if (template != null)
                        {
                            var templateControl = GetControl(template.Controls, controlId);
                            if (templateControl != null)
                            {
                                return templateControl.Id;
                            }
                        }
                    }

                    var control = GetControl(pageDraft.Controls, controlId);
                    if (control != null)
                    {
                        return control.Id;
                    }
                }
                else
                {
                    var control = GetControl(page.Controls, controlId);
                    if (control != null)
                    {
                        return control.Id;
                    }
                }
            }

            return Guid.Empty;
        }

        private static ControlData GetControl(IEnumerable<ControlData> controls, string controlId)
        {
            return controls.FirstOrDefault(p => p.Properties.FirstOrDefault(t => t.Name == "ID" && controlId.EndsWith(t.Value)) != null);
        }

        private static string GetTemplateIdKey(HttpContextBase context)
        {
            var requestContext = context.Items[RouteHandler.RequestContextKey] as RequestContext ?? context.Request.RequestContext;
            if (requestContext.RouteData.Values.ContainsKey("itemId"))
            {
                return requestContext.RouteData.Values["itemId"] as string;
            }
            else
            {
                return null;
            }
        }

        private static string GetVersionNumberKey(HttpContextBase context)
        {
            var requestContext = context.Items[RouteHandler.RequestContextKey] as RequestContext ?? context.Request.RequestContext;
            var keys = requestContext.RouteData.Values["Params"] as string[];
            if (requestContext.RouteData.Values.ContainsKey("VersionNumber"))
            {
                return requestContext.RouteData.Values["VersionNumber"] as string;
            }
            else
            {
                return null;
            }
        }

        private static bool IsEdit
        {
            get
            {
                var isEdit = false;
                if (SystemManager.IsDesignMode && !SystemManager.IsPreviewMode)
                {
                    isEdit = true;
                }

                return isEdit;
            }
        }

        private const string TemplateDraftProxy = "TemplateDraftProxy";
        private const string IsTemplate = "IsTemplate";
        private const string FormControlId = "FormControlId";
        private const string ControllerKey = "sf_cntrl_id";
    }
}