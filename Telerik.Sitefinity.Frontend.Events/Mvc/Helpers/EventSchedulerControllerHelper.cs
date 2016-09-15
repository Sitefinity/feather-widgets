using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
    internal class EventSchedulerControllerHelper
    {
        internal static EventSchedulerModel LoadModel(Guid widgetId, CultureInfo culture)
        {
            var pageManager = PageManager.GetManager();
            var objectData = pageManager.GetControls<ObjectData>().SingleOrDefault(p => p.Id == widgetId);

            if (objectData is PageDraftControl && ClaimsManager.IsBackendUser() == false)
                return null;

            EventSchedulerModel model = new EventSchedulerModel();

            if (objectData != null)
            {
                var mvcProxy = pageManager.LoadControl(objectData, culture) as MvcControllerProxy;

                if (mvcProxy != null)
                {
                    var schedullerController = mvcProxy.Controller as EventSchedulerController;
                    if (schedullerController != null)
                        model = schedullerController.Model as EventSchedulerModel;
                }
            }

            return model;
        }

        /// <summary>
        /// Get controller widget id
        /// </summary>
        /// <returns></returns>
        internal static Guid GetWidgetId(Controller controller)
        {
            Guid id = Guid.Empty;
            var pageManager = PageManager.GetManager();
            var pageId = SiteMapBase.GetCurrentNode().PageId;
            var page = pageManager.GetPageData(pageId);
            var viewBagControlId = controller.ViewData[ControllerKey];

            if (viewBagControlId != null)
            {
                var controlId = (string)viewBagControlId;
                if (IsEdit)
                {
                    var pageDraft = page.Drafts.FirstOrDefault(p => p.IsTempDraft);
                    var control = pageDraft.Controls.FirstOrDefault(p => p.Properties.FirstOrDefault(t => t.Name == "ID" && controlId.EndsWith(t.Value)) != null);
                    if (control != null)
                    {
                        id = control.Id;
                    }
                }
                else
                {
                    var control = page.Controls.FirstOrDefault(p => p.Properties.FirstOrDefault(t => t.Name == "ID" && controlId.EndsWith(t.Value)) != null);
                    if (control != null)
                    {
                        id = control.Id;
                    }
                }
            }

            return id;
        }

        /// <summary>
        /// Gets whether the page is in edit mode.
        /// </summary>
        /// <value>The is edit.</value>
        internal static bool IsEdit
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

        internal const string ControllerKey = "sf_cntrl_id";
    }
}
