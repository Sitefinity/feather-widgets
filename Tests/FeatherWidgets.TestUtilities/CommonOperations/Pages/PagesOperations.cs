using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.TestIntegration.Data.Content;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// This class provides access to page operations
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class PagesOperations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "3#")]
        public Guid CreatePageWithControl(Control control, string pageNamePrefix, string pageTitlePrefix, string urlNamePrefix, int index)
        {
            var controls = new List<System.Web.UI.Control>();
            controls.Add(control);

            this.locationGenerator = new PageContentGenerator();
            var pageId = this.locationGenerator.CreatePage(
                                    string.Format(CultureInfo.InvariantCulture, "{0}{1}", pageNamePrefix, index.ToString(CultureInfo.InvariantCulture)),
                                    string.Format(CultureInfo.InvariantCulture, "{0}{1}", pageTitlePrefix, index.ToString(CultureInfo.InvariantCulture)),
                                    string.Format(CultureInfo.InvariantCulture, "{0}{1}", urlNamePrefix, index.ToString(CultureInfo.InvariantCulture)));

            PageContentGenerator.AddControlsToPage(pageId, controls);

            return pageId;
        }

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

            using (var mvcWidget = new Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy())
            {
                mvcWidget.ControllerName = "ContentBlock.Mvc.Controllers.ContentBlockController";

                mvcWidget.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(new ContentBlockController()
                {
                    Content = html
                });

                this.CreateControl(pageManager, page, mvcWidget, "ContentBlock");
            }
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

            using (var mvcWidget = new Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy())
            {
                mvcWidget.ControllerName = "ContentBlock.Mvc.Controllers.ContentBlockController";
                mvcWidget.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(new ContentBlockController()
                {
                    Content = content.Content.Value,
                    SharedContentID = content.Id
                });

                this.CreateControl(pageManager, page, mvcWidget, "ContentBlock");
            }
        }

        /// <summary>
        /// Adds news widget to existing page
        /// </summary>
        /// <param name="pageId">Page id value</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public void AddNewsWidgetToPage(Guid pageId, string placeholder = "Body")
        {
            PageManager pageManager = PageManager.GetManager();
            pageManager.Provider.SuppressSecurityChecks = true;
            var pageDataId = pageManager.GetPageNode(pageId).PageId;
            var page = pageManager.EditPage(pageDataId, CultureInfo.CurrentUICulture);

            using (var mvcWidget = new Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy())
            {
                mvcWidget.ControllerName = "News.Mvc.Controllers.NewsController";

                this.CreateControl(pageManager, page, mvcWidget, "News", placeholder);
            }
        }

        /// <summary>
        /// Deletes the pages.
        /// </summary>
        public void DeletePages()
        {
            this.locationGenerator.Dispose();
        }

        /// <summary>
        /// Denies a specific page permissions for selected role.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <param name="roleProvider">The role provider name.</param>
        /// <param name="pageTitle">The page title.</param>
        public void DenyPermissionsForRole(string roleName, string roleProvider, string pageTitle)
        {
            RoleManager roleManager = RoleManager.GetManager(roleProvider);
            var role = roleManager.GetRole(roleName);

            PageManager pagemanager = PageManager.GetManager();
            PageNode mypage = pagemanager.GetPageNodes().FirstOrDefault(pn => pn.Title == pageTitle);

            if (mypage != null)
            {
                pagemanager.BreakPermiossionsInheritance(mypage);
                pagemanager.SaveChanges();

                if (role != null)
                {
                    var perm = pagemanager.GetPermissions()
                                .Where(p =>
                                       p.SetName == SecurityConstants.Sets.Pages.SetName &&
                                       p.PrincipalId == role.Id &&
                                       p.ObjectId == mypage.Id)
                                .SingleOrDefault();

                    if (perm == null)
                    {
                        perm = pagemanager.CreatePermission(
                        SecurityConstants.Sets.Pages.SetName,
                        mypage.Id,
                        role.Id);

                        mypage.Permissions.Add(perm);
                    }

                    perm = pagemanager.GetPermission(
                        SecurityConstants.Sets.Pages.SetName,
                        mypage.Id,
                        role.Id);

                    perm.DenyActions(
                        false,
                        SecurityConstants.Sets.Pages.View,
                        SecurityConstants.Sets.Pages.Create,
                        SecurityConstants.Sets.Pages.Modify,
                        SecurityConstants.Sets.Pages.CreateChildControls,
                        SecurityConstants.Sets.Pages.EditContent,
                        SecurityConstants.Sets.Pages.ChangeOwner,
                        SecurityConstants.Sets.Pages.Delete);

                    pagemanager.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Republish a page.
        /// </summary>
        /// <param name="pageTitle">The page title.</param>
        public void RepublishPage(string pageTitle)
        {
            PageManager pm = PageManager.GetManager();

            PageNode page = pm.GetPageNodes().FirstOrDefault(pn => pn.Title == pageTitle);

            var pageData = page.GetPageData();
            var master = pm.PagesLifecycle.GetMaster(pageData);
            pm.PagesLifecycle.Publish(master);
            pm.SaveChanges();
        } 

        /// <summary>
        /// Creates the mvcWidget control.
        /// </summary>
        /// <param name="pageManager">The page manager.</param>
        /// <param name="page">The page.</param>
        /// <param name="mvcWidget">The MVC widget.</param>
        private void CreateControl(PageManager pageManager, PageDraft page, Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy mvcWidget, string widgetCaption, string placeholder = "Body")
        {
            var draftControlDefault = pageManager.CreateControl<PageDraftControl>(mvcWidget, placeholder);
            draftControlDefault.Caption = widgetCaption;
            pageManager.SetControlDefaultPermissions(draftControlDefault);
            page.Controls.Add(draftControlDefault);

            pageManager.PublishPageDraft(page, CultureInfo.CurrentUICulture);
            pageManager.SaveChanges();
        }

        private PageContentGenerator locationGenerator;
    }
}
