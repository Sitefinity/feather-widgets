using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// This class provides access to page operations
    /// </summary>
    public class PagesOperations
    {
        /// <summary>
        /// Adds content block widget to existing page
        /// </summary>
        /// <param name="pageId">Page id value</param>
        /// <param name="html">Html value</param>
        public void AddContentBlockWidgetToPage(Guid pageId, string html)
        {
            PageManager pageManager = PageManager.GetManager();
            pageManager.Provider.SuppressSecurityChecks = true;
            var pageDataId = pageManager.GetPageNode(pageId).PageId;
            var page = pageManager.EditPage(pageDataId, CultureInfo.CurrentUICulture);

            var mvcWidget = new Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy();

            mvcWidget.ControllerName = "ContentBlock.Mvc.Controllers.ContentBlockController";
            mvcWidget.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(new ContentBlockController()
            {
                Content = html
            });
            var draftControlDefault = pageManager.CreateControl<PageDraftControl>(mvcWidget, "Body");
            draftControlDefault.Caption = "ContentBlock";
            pageManager.SetControlDefaultPermissions(draftControlDefault);
            page.Controls.Add(draftControlDefault);

            pageManager.PublishPageDraft(page, CultureInfo.CurrentUICulture);
            pageManager.SaveChanges();
        }

        /// <summary>
        /// Add shared content block widget to the page
        /// </summary>
        /// <param name="pageId">Page id value</param>
        /// <param name="contentBlockTitle">Content block title</param>
        public void AddSharedContentBlockWidgetToPage(Guid pageId, string contentBlockTitle)
        {
            PageManager pageManager = PageManager.GetManager();
            pageManager.Provider.SuppressSecurityChecks = true;
            var pageDataId = pageManager.GetPageNode(pageId).PageId;
            var page = pageManager.EditPage(pageDataId, CultureInfo.CurrentUICulture);

            var content = App.WorkWith().ContentItems()
            .Published()
            .Where(c => c.Title == contentBlockTitle)
            .Get().Single();

            var mvcWidget = new Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy();

            mvcWidget.ControllerName = "ContentBlock.Mvc.Controllers.ContentBlockController";
            mvcWidget.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(new ContentBlockController()
            {
                Content = content.Content.Value,
                SharedContentID = content.Id
            });
            var draftControlDefault = pageManager.CreateControl<PageDraftControl>(mvcWidget, "Body");
            draftControlDefault.Caption = "ContentBlock";
            pageManager.SetControlDefaultPermissions(draftControlDefault);
            page.Controls.Add(draftControlDefault);

            pageManager.PublishPageDraft(page, CultureInfo.CurrentUICulture);
            pageManager.SaveChanges();
        }
    }
}
